using System.Net.Sockets;
using System.Text.RegularExpressions;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.DeviceManagement;

public class SwitchEvents
{
    private const string frontCode = "68 10 10 68";
    private const string controlCode = "03";
    private const string orderCode = "2D";
    private const string userDataForOpen = "81 06 00 00 00 01 60 00";
    private const string userDataForClose = "81 06 00 00 00 01 60 01";
    private const string endCode = "16";
    private const string codeForTelemetering = "FF 06 00 00 00 01 40 2D";
    private const string controlCodeForTelemetering = "0A";
    private const string orderCodeForTelemetering = "64";

    public int TimesOfDisconnceted { get; set; } = 0;
    public bool isOpen { get; set; }
    public  Socket socket { get; set; }
    public IsOnline isOnline { get; set; }
    
    public IsAbnormalOrNot isAbnormal { get; set; }

    public void Telemetering(string DSN)//根据序列号遥测
    {
        try
        {
            string telemeteringCode = TelemeteringCode(DSN);
            byte[] data = ToBytesFromHexString(telemeteringCode);
            socket.Send(data);
            Console.WriteLine(string.Format("已发送指令：{0}\n", telemeteringCode));
        
        }
        catch (Exception ex)
        {

        }
    }
    public static string TelemeteringCode(string DSN)//得到最终遥测命令
    {
        string telemeteringCode = controlCodeForTelemetering + " " + DSN + " " + orderCodeForTelemetering + " " + codeForTelemetering;
        string cosc = checkOutSumCalc(telemeteringCode);
        // if (cosc.Length == 1)
        //     cosc += "0";
        if (Regex.IsMatch(cosc, "[a-zA-Z]"))
        {
            cosc = cosc.ToUpper();
        }
        string result = frontCode + " " + controlCodeForTelemetering + " " + DSN + " " +orderCodeForTelemetering+" "+ codeForTelemetering  + " " + cosc + " " + endCode;
        return result;
    }
    
    public void OpenSwitch(string DSN)//根据序列号开闸
    {
        try
        {
            string openCode = OpenCode(DSN);
            byte[] data = ToBytesFromHexString(openCode);
            socket.Send(data);
            Console.WriteLine(string.Format("已发送指令：{0}\n", openCode));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void CloseSwitch(string DSN)//根据序列号合闸
    {
        try
        {
            string closeCode = CloseCode(DSN);
            byte[] data = ToBytesFromHexString(closeCode);
            socket.Send(data);
            Console.WriteLine(string.Format("已发送指令：{0}\n", closeCode));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static string OpenCode(string terminalAddress)//得到最终开闸命令
    {
        string opencode = OpenCodeForCOSC(terminalAddress);
        string cosc = checkOutSumCalc(opencode);
        string result = frontCode + " " + controlCode + " " + terminalAddress + " " + orderCode + " " + userDataForOpen + " " + cosc + " " + endCode;
        return result;
    }
    private static string CloseCode(string terminalAddress)//得到最终关闸命令
    {
        string opencode = CloseCodeForCOSC(terminalAddress);
        string cosc = checkOutSumCalc(opencode);
        string result = frontCode + " " + controlCode + " " + terminalAddress + " " + orderCode + " " + userDataForClose + " " + cosc + " " + endCode;
        return result;
    }
    private  static string OpenCodeForCOSC(string terminalAddress)//根据设备编号组合开闸命令
    {
        string result = controlCode + " " + terminalAddress + " " + orderCode + " " + userDataForOpen;
        return result;
    }
    private static string CloseCodeForCOSC(string terminalAddress)//根据设备编号组合合闸命令
    {
        string result = controlCode + " " + terminalAddress + " " + orderCode + " " + userDataForClose;
        return result;
    }

    private static string checkOutSumCalc(string OpenOrClose)//校验和计算
    {
        byte[] res = ToBytesFromHexString(OpenOrClose);
        int sum = 0;
        for (var b = 0; b < res.Length; b++)
        {
            sum += res[b];
        }
        int res1 = sum % 256;
        string nnres1 = Convert.ToString(res1, 16);
        return nnres1;
    }

    private static byte[] ToBytesFromHexString(string hexString)//字符串转换为byte数组
    {
        //以 ' ' 分割字符串，并去掉空字符
        string[] chars = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        byte[] returnBytes = new byte[chars.Length];
        //逐个字符变为16进制字节数据
        for (int i = 0; i < chars.Length; i++)
        {
            returnBytes[i] = Convert.ToByte(chars[i], 16);
        }
        return returnBytes;
    }
}