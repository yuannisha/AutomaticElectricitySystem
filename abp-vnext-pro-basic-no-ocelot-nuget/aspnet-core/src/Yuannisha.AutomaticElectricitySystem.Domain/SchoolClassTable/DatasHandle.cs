using System.Data;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace Yuannisha.AutomaticElectricitySystem.SchoolClassTable;

public class DatasHandle
{
    protected static DateTime startDate = new DateTime(2023, 8, 28);//自定义开学日期

    public static Dictionary<string, List<ClassInformationInClassRoom>> RoomParaClassInfor = new Dictionary<string, List<ClassInformationInClassRoom>>();
    protected List<ClassInformationInClassRoom> ClassInforInClassRooms = new List<ClassInformationInClassRoom>();
    protected static string timeReference(int whichClass)
    {
        string timeStamp = "";
        switch (whichClass)
        {
            case 1:
                timeStamp = "7:50";
                break;
            case 2:
                timeStamp = "10:10";
                break;
            case 3:
                timeStamp = "10:10";
                break;
            case 4:
                timeStamp = "12:10";
                break;
            case 5:
                timeStamp = "13:50";
                break;
            case 6:
                timeStamp = "16:00";
                break;
            case 7:
                timeStamp = "16:00";
                break;
            case 8:
                timeStamp = "18:00";
                break;
            case 9:
                timeStamp = "18:50";
                break;
            case 10:
                timeStamp = "21:00";
                break;
            case 11:
                timeStamp = "21:00";
                break;
            case 12:
                timeStamp = "23:00";
                break;
        }
        return timeStamp;
    }
    protected static int AutoCalWeek()//根据当天日期计算其在本学期第几周
    {
        DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        int days = nowDate.Subtract(startDate).Days;
        int week = days / 7 + 1;
        return week;
    }
    protected static int CalWeekByDate(DateTime date)
    {
        int days = date.Subtract(startDate).Days;
        int week = days / 7 + 1;
        return week;
    }//根据输入的日期计算其在本学期第几周
    public static List<int> AutoGetTodayClassByRoom(string ClassRoom)//自动根据当天日期与输入的教室号获得当天教室的课程信息
    {
        int thisWeek = AutoCalWeek();
        string weekDay = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
        List<int> list = new List<int>();
        list = SurpportFuncF(list, ClassRoom, weekDay, thisWeek);
        return list;
    }
    public Dictionary<string, List<int>> AutoGetToweekClassesByRoom(string ClassRoom)//根据教室号自动获得此教室在当天所在周次的课程情况
    {
        int thisWeek = AutoCalWeek();
        Dictionary<string, List<int>> weekdayToClasses = new Dictionary<string, List<int>>();
        foreach (var item in RoomParaClassInfor)
        {
            if (item.Key.Contains(ClassRoom))
            {
                foreach (var o in item.Value)
                {
                    if (o.Week.Contains(thisWeek))
                    {
                        List<int> list = new List<int>();
                        foreach (int u in o.When)
                        {
                            list.Add(u);
                        }
                        if (weekdayToClasses.ContainsKey(o.WeekDay))
                        {
                            if (list.Count != 0)
                            {
                                foreach (var nn in list)
                                {
                                    //if(judgeRepeatedInDayOfWeek(weekdayToClasses,nn) == false)
                                    weekdayToClasses[o.WeekDay].Add(nn);
                                }
                            }
                        }
                        else
                        {
                            weekdayToClasses.Add(o.WeekDay, list);
                        }
                    }
                }
            }
        }
        return weekdayToClasses;
    }
    public static List<int> GetClassesByGivendateAndClassRoom(string ClassRoom, DateTime dateTime)//根据教室号、日期获得当天的教室课程情况
    {
        List<int> list = new List<int>();
        string weekDay = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);
        int thisWeek = CalWeekByDate(dateTime);
        list = SurpportFuncF(list, ClassRoom, weekDay, thisWeek);
        return list;
    }
    protected static List<int> SurpportFuncF(List<int> list, string ClassRoom, string weekDay, int thisWeek)//辅助函数
    {
        foreach (var item in RoomParaClassInfor)
        {
            if (item.Key.Contains(ClassRoom))
            {
                foreach (var o in item.Value)
                {
                    //Console.WriteLine($"教室等于：{item.Key}，有哪几个星期有课为：{o.WeekDay}");
                    if (o.Week.Contains(thisWeek) && o.WeekDay == weekDay)
                    {
                        if (list.Count == 0)
                            list = o.When;
                        else
                        {
                            if (!o.JudgeWeeksOrWeekdays(list, o.When))
                                o.AddNums(list, o.When);
                        }
                    }
                }
            }
        }
        return list;
    }
    public static int JudgeTime(string ClassRoom)//根据教室号自动判断当前时间是否在上课时间内
    {
        int State = 0;
        List<int> todayClasses = AutoGetTodayClassByRoom(ClassRoom);
        int times = todayClasses.Count() / 2;
        List<List<int>> list = new List<List<int>>();
        int startPos = 0;
        for (int i = 0; i < times; i++)
        {
            startPos = SupportFuncT(list, startPos, todayClasses);
        }

        bool flag = GetTimeSpan(todayClasses, list);
        if (flag)
        {
            //此处为当当前时间在上课时间时，需要做的处理过程，可外写函数引用进来
            //Console.WriteLine("现在在上课时间内！");
            State = 1;
        }
        else
        {
            //此处为当当前时间不在上课时间时，需要做的处理过程，可外写函数引用进来
            //Console.WriteLine("现在不在上课时间内！");
            State = 0;
        }
        return State;
    }
    protected static int SupportFuncT(List<List<int>> list, int startPos, List<int> todayClasses)//辅助函数
    {
        List<int> listIn = new List<int>();
        int flag = 0;
        for (int i = startPos; i < todayClasses.Count; i++)
        {
            flag++;
            if (flag <= 2)
            {
                startPos++;
                listIn.Add(todayClasses[i]);
            }
            else
                break;
        }
        list.Add(listIn);
        return startPos;
    }
    protected static bool GetTimeSpan(List<int> todayClasses, List<List<int>> list)//判断当前时间是否在某一个区间内
    {
        bool dataToReturn = false;

        foreach (var items in list)
        {
            if (items[0].Equals(items[1]))
            {
                string Time = timeReference(items[0]);
                TimeSpan time = DateTime.Parse(Time).TimeOfDay;

                var endTime = time.Add(new TimeSpan(1, 10, 0));

                string timeOfNow = DateTime.Now.ToString("t");
                TimeSpan TimeOfNow = DateTime.Parse(timeOfNow).TimeOfDay;

                if (TimeOfNow > time && TimeOfNow < endTime)
                {
                    dataToReturn = true;
                }
            }
            else
            {
                string startTime = timeReference(items[0]);
                string endTime = timeReference(items[1]);

                TimeSpan StartTime = DateTime.Parse(startTime).TimeOfDay;
                TimeSpan EndTime = DateTime.Parse(endTime).TimeOfDay;

                string timeOfNow = DateTime.Now.ToString("t");
                TimeSpan TimeOfNow = DateTime.Parse(timeOfNow).TimeOfDay;

                if (TimeOfNow > StartTime && TimeOfNow < EndTime)
                {
                    dataToReturn = true;
                }
            }
        }
        return dataToReturn;
    }
    protected static void Step1DataStorage(DataTable tablename, string[] temp1, string[] temp2, int flag, int times)
    {
        tablename.Rows.Add();
        tablename.Rows[flag]["id"] = flag + 1;
        tablename.Rows[flag]["上课地点"] = temp1[times];
        tablename.Rows[flag]["上课时间"] = temp2[times];
    }
    protected static void Step11DataStorage(DataTable tablename, string timeStamp, string classRoom, int flag)
    {
        tablename.Rows.Add();
        tablename.Rows[flag]["id"] = flag + 1;
        tablename.Rows[flag]["上课地点"] = classRoom;
        tablename.Rows[flag]["上课时间"] = timeStamp;
    }
    protected static void Step111DataStorage(DataTable tablename, string[] temp1, string[] temp2, int flag, int times)
    {
        tablename.Rows.Add();
        tablename.Rows[flag]["id"] = flag + 1;
        tablename.Rows[flag]["上课地点"] = temp1[0];
        tablename.Rows[flag]["上课时间"] = temp2[times];
    }
    protected static void FreeTables(DataTable tablename)//用完datatable后释放其空间
    {
        tablename.Clear();
        tablename.Dispose();
        tablename = null;
    }

    public static void GetInformationOfClasses()
    {
        try
        {
            RoomParaClassInfor.Clear();//清空字典

            DataTable allDatas = new DataTable("allDatas");
            allDatas.Columns.Add("教室总信息", typeof(String));
            allDatas.Columns.Add("时间总信息", typeof(String));
            allDatas.Columns.Add("教师", typeof(String));

            DataTable datasStep1 = new DataTable("datasStep1");
            datasStep1.Columns.Add("id", typeof(int));
            datasStep1.Columns.Add("上课地点", typeof(String));
            datasStep1.Columns.Add("上课时间", typeof(String));

            string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.102.1.91)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=JWTORZ_VIEW_JXBXX;Password=JWTORZ_VIEW_JXBXX;";
            OracleConnection con = new OracleConnection(connString);

            try
            {
                con.Open();
                Console.WriteLine($"课表连接成功----{DateTime.Now.ToString()}");
                //Debug.WriteLine("连接成功");
                OracleCommand com = con.CreateCommand();
                com.CommandText = "SELECT VIEW_JXBXX.\"上课地点\",VIEW_JXBXX.\"上课时间\" FROM VIEW_JXBXX WHERE VIEW_JXBXX.\"学年\"='2023-2024' and VIEW_JXBXX.\"上课地点\" IS NOT NULL AND VIEW_JXBXX.\"上课地点\" NOT LIKE '%运动场%' AND VIEW_JXBXX.\"上课地点\" NOT LIKE '%实习%' AND VIEW_JXBXX.\"上课地点\" NOT LIKE '%设计%' AND VIEW_JXBXX.\"学期\"=1";
                OracleDataReader odr = com.ExecuteReader();

                int flag = 0;
                int sum = 0;
                int flag1 = 0;
                bool flag2;
                int flag3 = 0;
                int sumofFalse = 0;
                int flag4 = 0;
                int flag5 = 0;
                int flag6 = 0;
                while (odr.Read())//将最初始状态上课时间相对应的上课地点存入allDatas表中
                {
                    string classRoom = odr["上课地点"].ToString();
                    string timeOfClass = odr["上课时间"].ToString();
                    allDatas.Rows.Add();
                    allDatas.Rows[flag]["教室总信息"] = classRoom;
                    allDatas.Rows[flag]["时间总信息"] = timeOfClass;
                    flag++;
                }

                for (int sp = 0; sp < allDatas.Rows.Count; sp++)//将初始课表数据根据自身情况解析
                {

                    flag2 = true;
                    //Console.WriteLine("{0} ----  {1}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString());
                    string[] classes = allDatas.Rows[sp]["教室总信息"].ToString().Split(";");
                    string[] times = allDatas.Rows[sp]["时间总信息"].ToString().Split(";");

                    if (classes.Length != times.Length || allDatas.Rows[sp]["教室总信息"].ToString().Contains("B401-402"))//筛选出不符合规范的课表数据
                    {
                        if (classes.Length > 1)
                        {
                            for (int p = 0; p < classes.Length; p++)
                            {
                                //Console.WriteLine("{0}-------{1}----------{2}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString(), allDatas.Rows[sp]["教师"].ToString());
                                if (classes[0] != classes[p])//此步骤为筛选出不可解析的数据
                                {
                                    sum++;
                                    sumofFalse++;
                                    flag2 = false;
                                    //Console.WriteLine("{0}-------{1}----------{2}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString(), allDatas.Rows[sp]["教师"].ToString());
                                    break;
                                }
                            }
                        }
                        if (flag2)////存入原始数据中不符合规范中课程信息中可以解析出来的数据
                        {
                            if (times.Length == 1 || classes[0].Contains("B401-402"))//存入特殊的B401-402教室的课程信息
                            {
                                sum++;
                                flag4++;
                                flag6++;
                                //Console.WriteLine("开始{0}-------{1}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString());
                                string[] temp1 = allDatas.Rows[sp]["教室总信息"].ToString().Split("-");
                                temp1[1] = "B" + temp1[1];

                                for (int j = 0; j < temp1.Length; j++)
                                {
                                    string temp2 = temp1[j];
                                    Step11DataStorage(datasStep1, allDatas.Rows[sp]["时间总信息"].ToString(), temp2, flag1);
                                    flag1++;
                                }
                            }
                            else//存入类似于F206D-------星期五第3-4节{1-16周};星期日第9-12节{8周}的课程信息或者上课地点为以下特点的数据：F206D;F206D
                            {
                                sum++;
                                flag3++;
                                flag4++;
                                //Console.WriteLine("开始{0}-------{1}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString());
                                for (int j = 0; j < times.Length; j++)
                                {
                                    Step111DataStorage(datasStep1, classes, times, flag1, j);
                                    flag1++;
                                }

                            }
                            //Console.WriteLine("{0}-------{1}", allDatas.Rows[sp]["教室总信息"].ToString(), allDatas.Rows[sp]["时间总信息"].ToString());
                        }
                    }
                    else//将大部分符合规范的课表数据存入到初步数据表中
                    {
                        sum++;
                        for (int j = 0; j < classes.Length; j++)
                        {
                            Step1DataStorage(datasStep1, classes, times, flag1, j);
                            flag1++;
                        }
                        flag5++;
                    }
                }
                for (int i = 0; i < datasStep1.Rows.Count; i++)
                {
                    ClassInformationInClassRoom classInforInClass = new ClassInformationInClassRoom();
                    classInforInClass.SetDatas(datasStep1.Rows[i]["上课地点"].ToString(), datasStep1.Rows[i]["上课时间"].ToString());

                    if (RoomParaClassInfor.ContainsKey(classInforInClass.ClassRoom))
                    {
                        RoomParaClassInfor[classInforInClass.ClassRoom].Add(classInforInClass);
                    }
                    else
                    {
                        RoomParaClassInfor.Add(classInforInClass.ClassRoom, new List<ClassInformationInClassRoom>() { classInforInClass });
                    }
                }

                FreeTables(allDatas);
                FreeTables(datasStep1);
            }
            finally
            {
                con.Close();
                Console.WriteLine($"课表连接已关闭！----{DateTime.Now.ToString()}");
            } 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}