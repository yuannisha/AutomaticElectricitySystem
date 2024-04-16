using System.Text.RegularExpressions;

namespace Yuannisha.AutomaticElectricitySystem.SchoolClassTable;

public class ClassInformationInClassRoom
{

    public List<int> Week { get; set; }
    public string WeekDay { get; set; }
    public List<int> When { get; set; }
    public string ClassRoom { get; set; }
    public ClassInformationInClassRoom() { }

    public bool JudgeWeeksOrWeekdays(List<int> outterList, List<int> innerList)
    {
        bool flag = false;
        foreach (var u in innerList)
        {
            if (outterList.Contains(u))
            {
                flag = true;
                break;
            }
        }
        return flag;
    }
    public void AddNums(List<int> outterList, List<int> innerList)
    {
        foreach (var u in innerList)
        {
            outterList.Add(u);
        }
    }
    private string[] ExtractNums(string Str, int Pos1, int Pos2)//提取纯数字
    {
        var result1 = Regex.Replace(Str, @"[^0-9]+", ",");
        var result2 = result1.Substring(Pos1, result1.Length-Pos2);
        var result3 = result2.Split(",");
        return result3;
    }
    private List<int> SupportFunc3(string Str)//this.Week赋值方法
    {
        List<int> list = new List<int>();
        if (Str.Contains(","))
        {
            string[] temp = Str.Split(",");
            foreach (string u in temp)
            {
                //Console.WriteLine(u);
                if (u.Contains("双"))
                {
                    var res1 = ExtractNums(u, 0, 1);
                    for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                    {
                        if (i % 2 ==1)
                            continue;
                        else
                            list.Add(i);
                    }
                }
                else if (u.Contains("单"))
                {
                    var res1 = ExtractNums(u, 0, 1);
                    for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                    {
                        if (i % 2 ==1)
                            list.Add(i);
                        else
                            continue;
                    }
                }
                else
                {
                    if (!u.Contains("-"))
                    {
                        var res1 = ExtractNums(u, 0, 1);
                        list.Add(Convert.ToInt32(res1[0]));
                    }
                    else
                    {
                        var res1 = ExtractNums(u, 0, 1);
                        for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                        {
                            list.Add(i);
                        }
                    }
                }
            }
        }
        else
        {
            if (!Str.Contains("-"))
            {
                var res1 = ExtractNums(Str, 0, 1);
                list.Add(Convert.ToInt32(res1[0]));
            }
            else
            {
                if (Str.Contains("双"))
                {
                    var res1 = ExtractNums(Str, 0, 1);
                    for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                    {
                        if (i % 2 ==1)
                            continue;
                        else
                            list.Add(i);
                    }
                }
                else if (Str.Contains("单"))
                {
                    var res1 = ExtractNums(Str, 0, 1);
                    for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                    {
                        if (i % 2 ==1)
                            list.Add(i);
                        else
                            continue;
                    }
                }
                else
                {
                    if (!Str.Contains("-"))
                    {
                        var res1 = ExtractNums(Str, 0, 1);
                        list.Add(Convert.ToInt32(res1[0]));
                    }
                    else
                    {
                        var res1 = ExtractNums(Str, 0, 1);
                        for (int i = Convert.ToInt32(res1[0]); i <= Convert.ToInt32(res1[1]); i++)
                        {
                            list.Add(i);
                        }
                    }
                }
            }
        }
        return list;
    }
    private void SupportFunc2(List<int> listname, int startNuM, int endNum)//辅助函数-1
    {
        for (int i = startNuM; i<=endNum; i++)
        {
            listname.Add(i);
        }
    }
    private List<int> SupportFunc(List<int> listname, string[] strArray)//辅助函数-2
    {
        if (strArray.Length == 4)
        {
            string startStr1 = strArray[0];
            string endStr1 = strArray[1];
            string startStr2 = strArray[2];
            string endStr2 = strArray[3];
            SupportFunc2(listname, Convert.ToInt32(startStr1), Convert.ToInt32(endStr1));
            SupportFunc2(listname, Convert.ToInt32(startStr2), Convert.ToInt32(endStr2));
        }
        else
        {
            SupportFunc2(listname, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
        }
        return listname;
    }
    private List<int> SupportFunc1(string Str)//this.When赋值方法
    {
        List<int> list = new List<int>();
        var result = ExtractNums(Str, 1, 2);
        list = SupportFunc(list, result);
        return list;
    }
    public void SetDatas(string ClassRoom, string TimeInformation)
    {
        this.ClassRoom = ClassRoom;//this.ClassRoom赋值

        string[] temp = TimeInformation.Split("{");
        string weekDay = temp[0][..3];
        this.WeekDay = weekDay;//this.WeekDay赋值
        //Console.WriteLine(weekDay);

        this.When = SupportFunc1(temp[0]);//this.When赋值
        this.Week = SupportFunc3(temp[1]);//this.Week赋值
    }

}