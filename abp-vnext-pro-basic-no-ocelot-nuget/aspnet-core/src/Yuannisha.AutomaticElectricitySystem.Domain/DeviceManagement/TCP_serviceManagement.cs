using System.Net;
using System.Net.Sockets;
using System.Text;
using NUglify.Helpers;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.DeviceManagement;

public class TCP_serviceManagement
{
    public static Dictionary<string, SwitchEvents> DSNwithInfor = new Dictionary<string, SwitchEvents>();

    private static bool ServiceIsOnlineOrNot = false;

    private static readonly object Dic1_Obj = new object();

    private static readonly object Dic2_Obj = new object();

    private static Socket socket;

    // public static GetBuildingOutPutDto Buildings = null;

    public static List<PowerSwitchsDto> PowerSwitchs = new List<PowerSwitchsDto>();

    // public static GetAllClassroomsOutPutDto Rooms = null;
    
    private static readonly IPowerSwitchsRepository _powerSwitchsRepository;
    private static readonly IObjectMapper _objectMapper;
    private static readonly IRepository<PowerSwitchs,Guid> _repositoryOfPowerSwitchs;
    private static PowerSwitchsManager powerSwitchsManager = new PowerSwitchsManager(_powerSwitchsRepository,
        _objectMapper,_repositoryOfPowerSwitchs);
    
    private const string UpdateTimeSpan = "00:00";
    private static double YesyterdayConsumption = 0;

    public static void benginService(bool restart=false)//开启TCP服务端 
    {
        //1、创建Socket对象
        //参数：寻址方式，当前为Ivp4  指定套接字类型   指定传输协议Tcp；
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //2、绑定端口、IP
        //IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(GetCurrentLoginIP()), int.Parse(txtPort.Text));
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8999);
        socket.Bind(iPEndPoint);
        if (restart)
        {
            //先关闭服务后以便重启服务
            socket.Close();
            Console.WriteLine("服务已关闭！");
            //3、开启侦听   10为队列最多接收的数量
            socket.Listen(100);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
            //4、开始接受客户端的连接  ，连接会阻塞主线程，故使用线程池。
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
            ServiceIsOnlineOrNot = true;
            Console.WriteLine("服务已重新启动！");
        }
        else
        {
            if (!ServiceIsOnlineOrNot)
            {
                //3、开启侦听   10为队列最多接收的数量
                socket.Listen(100);//如果同时来了100个连接请求，只能处理一个,队列中10个在等待连接的客户端，其他的则返回错误消息。
                //4、开始接受客户端的连接  ，连接会阻塞主线程，故使用线程池。
                ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
                ServiceIsOnlineOrNot = true;
                Console.WriteLine("服务已启动！");
            }
            else
            {
                Console.WriteLine("服务正在使用，请勿重复开启！");
            }
        }
    }

    /// <summary>
    /// 线程池线程执行的接受客户端连接方法
    /// </summary>
    /// <param name="obj">传入的Socket</param>
    private static void AcceptClientConnect(object obj)
    {
        //转换Socket
        var serverSocket = obj as Socket;
        Console.WriteLine("服务端开始接收客户端连接！\n");
        //不断接受客户端的连接
        while (true)
        {
            try
            {
                //5、创建一个负责通信的Socket
                Socket proxSocket = serverSocket.Accept();
                Console.WriteLine(string.Format("客户端：{0}连接上了！\n", proxSocket.RemoteEndPoint.ToString()));//上线事件
                //IPorDSwithSocket.Add(proxSocket.RemoteEndPoint.ToString(), proxSocket);
                //6、不断接收客户端发送来的消息

                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("异常："+ex.Message);
                break;
            }
        }
    }

    /// <summary>
    /// 接受客户端信息方法
    /// </summary>
    /// <param name="obj">传入的Socket</param>
    private static void ReceiveClientMsg(object obj)
    {
        var proxSocket = obj as Socket;
        //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
        byte[] data = new byte[1024];
        while (true)
        {
            int len;
            try
            {
                //接收消息,返回字节长度
                len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                //7、关闭Socket
                //异常退出
                try
                {
                    ClientExit(string.Format("客户端：{0}非正常退出", proxSocket.RemoteEndPoint.ToString()), proxSocket);
                }
                catch (Exception e)
                {
                    //Console.WriteLine("异常2:"+e.Message);
                }
                return;//让方法结束，终结当前客户端数据的异步线程，方法退出，即线程结束
            }
            if(len == 0 )
            {
                return;
            }
            byte[] validStr = data.Take(len).ToArray();
            string msgStr = ToHexStrFromByte(validStr);
            Console.WriteLine(string.Format("接收到客户端：{0}的消息：{1}", proxSocket.RemoteEndPoint.ToString(), msgStr));
            if (msgStr.StartsWith("68 5F 5F 68 88") && msgStr.Length > 60)
            {
                string DSN = GetDSN(msgStr);
                string StatusNum = msgStr.Substring(60, 2);
                Console.WriteLine($"空开状态代码：{StatusNum}");
                SpecialAddSwitchEvents(DSN, StatusNum, proxSocket);
                CheckDisDev(DSN);
                
                // string exceptionStatusCheck = msgStr.Substring(63, 228);
                // OpeningtimeAndExceptionStatusWatch(DSN,exceptionStatusCheck);
            }

            // if (msgStr.StartsWith("68 6A 6A 68 88") && msgStr.Length > 60)
            // {
            //     string DSN = GetDSN(msgStr);
            //     string energyConsumption = msgStr.Substring(102, 5);
            //     MonitorEnergyConsumption(DSN,energyConsumption);
            // }
        }
    }

    public static async Task AutoAddTestDatasScript()
    {
        var newPowerSwitchsDto = new PowerSwitchsDto();
        var list = DSNwithInfor.Keys.ToList();
        string DSN = "";
        foreach (var powerSwitch in PowerSwitchs)
        {
            var time = DateTime.Now.ToString("HH:mm");
            double totalSumForUpdate = 0;
            if (time.Equals(UpdateTimeSpan))
            {
                totalSumForUpdate = 0;
            }
            else
            {
                // 创建 Random 类的实例
                Random random = new Random();

                // 生成一个介于 2200 到 20500 之间的随机整数
                int randomInt = random.Next(150, 1351); // 20501 是上限的独占值

                // 转换为 double 类型并除以 100.0，以生成两位小数的浮点数
                double randomValue = randomInt / 100.0;

                totalSumForUpdate = powerSwitch.EnergyConsumption + randomValue;
            }
            list.ForEach(x =>
            {
                if (x.Replace(" ", "").Equals(powerSwitch.SerialNumber))
                    DSN = x;
            });
            newPowerSwitchsDto = await powerSwitchsManager.UpdateAsync(powerSwitch.Id, powerSwitch.RoomId, 
                powerSwitch
                    .SerialNumber, powerSwitch.ControlledMachineName, powerSwitch.IsOnline, powerSwitch
                    .Status, DSNwithInfor[DSN].isAbnormal,totalSumForUpdate);
        }
        if (newPowerSwitchsDto.Id != Guid.Empty)
        {
            int index = PowerSwitchs.FindIndex(p => p.Id == newPowerSwitchsDto.Id);
            if(index != -1)
            {
                PowerSwitchs[index] = newPowerSwitchsDto;
            }
        }
    }

    public static async Task MonitorEnergyConsumption(string DSN , string energyConsumption)
    {
        var parts = energyConsumption.Split(" ");
        Array.Reverse(parts);
        string reversed = string.Join("", parts);
        var totalSum = Convert.ToInt32(reversed, 16) * 0.01;
        
        var newPowerSwitchsDto = new PowerSwitchsDto();
        foreach (var powerSwitch in PowerSwitchs)
        {
            if (powerSwitch.SerialNumber.Equals(DSN.Replace(" ","")))
            {
                var time = DateTime.Now.ToString("HH:mm");
                double totalSumForUpdate = 0;
                if (time.Equals(UpdateTimeSpan))
                {
                    YesyterdayConsumption = totalSum;
                    var data = totalSum - powerSwitch.EnergyConsumption;
                    totalSumForUpdate = powerSwitch.EnergyConsumption + data;
                }
                else
                {
                    totalSumForUpdate = totalSum - YesyterdayConsumption;
                }
                newPowerSwitchsDto = await powerSwitchsManager.UpdateAsync(powerSwitch.Id, powerSwitch.RoomId, 
                    powerSwitch
                        .SerialNumber, powerSwitch.ControlledMachineName, powerSwitch.IsOnline, powerSwitch
                        .Status, DSNwithInfor[DSN].isAbnormal,totalSumForUpdate);
            }
        }
        if (newPowerSwitchsDto.Id != Guid.Empty)
        {
            int index = PowerSwitchs.FindIndex(p => p.Id == newPowerSwitchsDto.Id);
            if(index != -1)
            {
                PowerSwitchs[index] = newPowerSwitchsDto;
            }
        }
    }
    public static async Task OpeningtimeAndExceptionStatusWatch(string DSN,string exceptionStatusCheck)
    {
        var newPowerSwitchsDto = new PowerSwitchsDto();
        foreach (var powerSwitch in PowerSwitchs)
        {
            if (powerSwitch.SerialNumber.Equals(DSN.Replace(" ","")))
            {
                if (exceptionStatusCheck.Contains("1"))
                {
                    DSNwithInfor[DSN].isAbnormal = IsAbnormalOrNot.IsAbnormal;

                    if (!powerSwitch.IsAbnormal.Equals(DSNwithInfor[DSN].isAbnormal))
                    {
                        newPowerSwitchsDto = await powerSwitchsManager.UpdateAsync(powerSwitch.Id, powerSwitch.RoomId, 
                            powerSwitch
                                .SerialNumber, powerSwitch.ControlledMachineName, powerSwitch.IsOnline, powerSwitch
                                .Status, DSNwithInfor[DSN].isAbnormal, powerSwitch.EnergyConsumption);
                    }
                }
            }
        }
        if (newPowerSwitchsDto.Id != Guid.Empty)
        {
            int index = PowerSwitchs.FindIndex(p => p.Id == newPowerSwitchsDto.Id);
            if(index != -1)
            {
                PowerSwitchs[index] = newPowerSwitchsDto;
            }
        }
    }
    private static void CheckDisDev(string DSN)
    {
        lock (Dic1_Obj)
        {
            var newPowerSwitchsDto = new PowerSwitchsDto();
            DSNwithInfor.ForEach(async item =>
            {
                if (item.Key != DSN)
                {
                    if (DSNwithInfor.ContainsKey(item.Key))
                        DSNwithInfor[item.Key].TimesOfDisconnceted++;
                }
                else if (item.Key == DSN)
                {
                    if (DSNwithInfor.ContainsKey(item.Key))
                    {
                        foreach (var powerSwitch in PowerSwitchs)
                        {
                            if (powerSwitch.SerialNumber.Equals(item.Key))
                            {
                                if (!powerSwitch.IsOnline.Equals(DSNwithInfor[item.Key].isOnline))
                                {
                                    newPowerSwitchsDto = await powerSwitchsManager.UpdateAsync(powerSwitch.Id, powerSwitch.RoomId, 
                                        powerSwitch
                                            .SerialNumber, powerSwitch.ControlledMachineName, DSNwithInfor[item.Key].isOnline, powerSwitch
                                            .Status, powerSwitch.IsAbnormal, powerSwitch.EnergyConsumption);
                                    break;
                                }
                            }
                        }
                        DSNwithInfor[item.Key].TimesOfDisconnceted = 0;
                    }
                        
                }
                if (DSNwithInfor[item.Key].TimesOfDisconnceted > 30)
                {
                    if (DSNwithInfor.ContainsKey(item.Key))
                    {
                        DSNwithInfor[item.Key].isOnline = IsOnline.Off;
                        foreach (var powerSwitch in PowerSwitchs)
                        {
                            if (powerSwitch.SerialNumber.Equals(item.Key))
                            {
                                if (!powerSwitch.IsOnline.Equals(DSNwithInfor[item.Key].isOnline))
                                {
                                    newPowerSwitchsDto = await powerSwitchsManager.UpdateAsync(powerSwitch.Id, powerSwitch.RoomId, 
                                        powerSwitch
                                            .SerialNumber, powerSwitch.ControlledMachineName, DSNwithInfor[item.Key].isOnline, powerSwitch
                                            .Status, powerSwitch.IsAbnormal, powerSwitch.EnergyConsumption);
                                    break;
                                }
                            }
                        }
                        DSNwithInfor[item.Key].TimesOfDisconnceted = 0;
                    }
                }
            });
        }
    }

    /// <summary>
    /// 客户端退出调用
    /// </summary>
    /// <param name="msg"></param>
    private static void ClientExit(string msg, Socket proxSocket)
    {
        Console.WriteLine("{0}\n", msg);
        try
        {
            if (proxSocket.Connected && proxSocket != null)//如果是连接状态
            {
                proxSocket.Shutdown(SocketShutdown.Both);//关闭连接
                proxSocket.Close();//100秒超时间
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    public static List<string> OnlineDeviceList()
    {
        List<string> list = new List<string>();
        lock (Dic2_Obj)
        {
            if (DSNwithInfor.Count != 0)
            {
                for (int i = 0; i < DSNwithInfor.Count; i++)
                {
                    try
                    {
                        KeyValuePair<string, SwitchEvents> kv = DSNwithInfor.ElementAt(i);
                        if (DSNwithInfor[kv.Key].isOnline == IsOnline.On)
                        {
                            string res = kv.Key.Replace(" ", "");
                            list.Add(res);
                        }
                        else
                            continue;
                    }
                    catch (Exception e)
                    {

                    }
                }
                //list   = DSNwithInfor.Keys.ToList<string>();
            }
        }
        return list;

    }//得到在线设备列表

    public static int GetDeviceStatus(string DSN)
    {
        int StateNum = 0;
        try
        {
            if (DSNwithInfor[DSN].isOpen == true)
            {
                //Console.WriteLine("设备{0}目前处于开闸状态", DSN);
                StateNum = 0;
            }
            else if (DSNwithInfor[DSN].isOpen == false)
            {
                //Console.WriteLine("设备{0}目前处于合闸状态", DSN);
                StateNum = 1;
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine(e.Message);
        }
        return StateNum;
    }//得到设备开合闸状态
    private static void SpecialAddSwitchEvents(string DSN, string StatusNum, Socket proxSocket)
    {
        lock (Dic1_Obj)
        {
            if (DSNwithInfor.Count == 0 || !DSNwithInfor.ContainsKey(DSN))
            {
                AddSwitchEvents(DSN, StatusNum, proxSocket);
            }
            else if (DSNwithInfor.ContainsKey(DSN))
            {
                DSNwithInfor.Remove(DSN);
                AddSwitchEvents(DSN, StatusNum, proxSocket);
            }
            else
            {
                return;
            }
        }
    }

    private static void AddSwitchEvents(string DSN, string StatusNum, Socket proxSocket)
    {
        DSNwithInfor.Add(DSN, new SwitchEvents());
        DSNwithInfor[DSN].socket = proxSocket;
        if (StatusNum == "00") DSNwithInfor[DSN].isOpen = true;
        else if (StatusNum == "01") DSNwithInfor[DSN].isOpen = false;
        DSNwithInfor[DSN].isOnline = IsOnline.On;

    }//将序列号与相应的socket等存入字典中

    private static string ToHexStrFromByte(byte[] byteDatas)//byte数组转十六进制
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < byteDatas.Length; i++)
        {
            builder.Append(string.Format("{0:X2} ", byteDatas[i]));
        }
        return builder.ToString().Trim();
    }

    private static string GetDSN(string msg)//根据收到的空开消息得到空开序列号
    {
        string res1 = "";
        if (msg.Length > 78)
        {
            res1 = msg.Substring(15, 17);
        }
        return res1;
    }

    public static string AddSpaceInSeriesNum(string SeriesNum)
    {
        int time = 0;
        for (int i = 1; i <= 5; i++)
        {
            SeriesNum = SeriesNum.Insert(2 * i + time, " ");
            time++;
        }
        return SeriesNum;
    }//为输入的序列号做格式编辑

}