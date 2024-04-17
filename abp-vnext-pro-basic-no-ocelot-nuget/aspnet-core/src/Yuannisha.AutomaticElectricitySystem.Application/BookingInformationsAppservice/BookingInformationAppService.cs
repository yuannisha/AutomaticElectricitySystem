using System.Data;
using Abp.Application.Services.Dto;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NUglify.Helpers;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;
using Yuannisha.AutomaticElectricitySystem.SchoolClassTable;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsAppservice;

/// <summary>
/// 预约信息
/// </summary>

public class BookingInformationAppService : ApplicationService, IBookingInformationAppService
{
    private readonly BookingInformationManager _bookingInformationManager;
    private readonly RoomsManager _roomsManager;

    private readonly IGuidGenerator _guidGenerator;

    protected static Dictionary<string, List<TimespansInWeekend>> PianoRoomsBookingTimespanInWeekend = new Dictionary<string, List<TimespansInWeekend>>();

    protected static Dictionary<string, List<string>> PianoRoomsBookingTimespan = new Dictionary<string, List<string>>();

    protected static List<string> Timespans = new List<string>() { "08:00-09:00","09:00-10:00",
            "10:00-11:00", "11:00-12:00", "12:00-13:00", "13:00-14:00", "14:00-15:00", "15:00-16:00",
            "16:00-17:00", "17:00-18:00", "18:00-19:00", "19:00-20:00", "20:00-21:00" };

    protected static List<string> ReferenceTimespans = new List<string>() { "08:00-09:00","09:00-10:00",
            "10:00-11:00", "11:00-12:00", "12:00-13:00", "13:00-14:00", "14:00-15:00", "15:00-16:00",
            "16:00-17:00", "17:00-18:00", "18:00-19:00", "19:00-20:00", "20:00-21:00" };

    protected static List<string> PianoRooms = new List<string>() { "创512-琴房1", "创512-琴房2", "创512-琴房3", "创512-琴房4",
            "创512-琴房5","创512-琴房6","创607-琴房7","创607-琴房8","创607-琴房9","创608-琴房10","创608-琴房11"
            ,"创608-琴房12"};
    
    protected static List<string> PianoroomNames = new List<string>() { "创512","创607","创608"};
    
    protected static Dictionary<string, List<string>>  PianoRoomNamesWithTimespans = new Dictionary<string, List<string>>();
    
    // private static Abp.Domain.Repositories.IRepository<BookingInformation, Guid> _repositoryForbookingInformation;

    protected static string timeReference(int whichClass)
    {
        string timeStamp = "";
        switch (whichClass)
        {
            case 1:
                timeStamp = "08:00-09:00";
                break;
            case 2:
                timeStamp = "09:00-10:00";
                break;
            case 3:
                timeStamp = "10:00-11:00";
                break;
            case 4:
                timeStamp = "11:00-12:00";
                break;
            case 5:
                timeStamp = "14:00-15:00";
                break;
            case 6:
                timeStamp = "15:00-16:00";
                break;
            case 7:
                timeStamp = "16:00-17:00";
                break;
            case 8:
                timeStamp = "17:00-18:00";
                break;
            case 9:
                timeStamp = "19:00-20:00";
                break;
            case 10:
                timeStamp = "20:00-21:00";
                break;
            case 11:
                timeStamp = "21:00-22:00";
                break;
        }
        return timeStamp;
    }
    
    private readonly IRepository<BookingLimited, Guid> _bookingLimitedRepository;

    public BookingInformationAppService(BookingInformationManager bookingInformationManager, IGuidGenerator 
            guidGenerator,IRepository<BookingLimited, Guid> bookingLimitedRepository,
        RoomsManager roomsManager
        // Abp.Domain.Repositories.IRepository<BookingInformation, Guid> repositoryForbookingInformation
        )
    {
        _bookingInformationManager = bookingInformationManager;
        _guidGenerator = guidGenerator;
        _bookingLimitedRepository = bookingLimitedRepository;
        // _repositoryForbookingInformation = repositoryForbookingInformation;
        _roomsManager = roomsManager;
    }
    
            public static void InitPianoRoomsBookingTimespan()
{

    PianoRoomsBookingTimespan.Clear();//清空字典
    PianoRoomsBookingTimespanInWeekend.Clear();//清空字典
    PianoRoomNamesWithTimespans.Clear();//清空字典

    // string date1 = DateTime.Now.ToString("yyyy/MM/dd");
    // string date2 = DateTime.Now.ToString("MM/dd/yyyy");
    // var bookingInfor = _repositoryForbookingInformation.GetAllIncluding(x=>x.BookingTimespan.Contains(date1) || x
    //     .BookingTimespan.Contains(date2));

    string dt;
    string weekday = "";
    dt = DateTime.Today.DayOfWeek.ToString();
    switch (dt)
    {
        case "Monday":
            weekday = "星期一";
            break;
        case "Tuesday":
            weekday = "星期二";
            break;
        case "Wednesday":
            weekday = "星期三";
            break;
        case "Thursday":
            weekday = "星期四";
            break;
        case "Friday":
            weekday = "星期五";
            break;
        case "Saturday":
            weekday = "星期六";
            break;
        case "Sunday":
            weekday = "星期日";
            break;
    }

    var OccupiedClassroom = "";
    var keys = DatasHandle.RoomParaClassInfor.Keys.ToArray();
    var classes = new List<int>();



    foreach (var pr in PianoroomNames)
    {
        foreach (var key in keys)
        {
            if (key.Contains(pr))
            {
                OccupiedClassroom = key;
                classes = DatasHandle.AutoGetTodayClassByRoom(OccupiedClassroom);
                //classes = DatasHandle.getClassesByGivendateAndClassRoom(OccupiedClassroom, new DateTime(2023, 11, 1));

                HandleTimespans(classes, OccupiedClassroom);

                break;
            }

        }
    }
    SetTimeSpans(weekday);
}
//         public static async void InitPianoRoomsBookingTimespan()
// {
//
//     PianoRoomsBookingTimespan.Clear();//清空字典
//     PianoRoomsBookingTimespanInWeekend.Clear();//清空字典
//     PianoRoomNamesWithTimespans.Clear();//清空字典
//
//     string date1 = DateTime.Now.ToString("yyyy/MM/dd");
//     string date2 = DateTime.Now.ToString("MM/dd/yyyy");
//     var bookingInfor = _repositoryForbookingInformation.GetAllIncluding(x=>x.BookingTimespan.Contains(date1) || x
//         .BookingTimespan.Contains(date2));
//
//     string dt;
//     string weekday = "";
//     dt = DateTime.Today.DayOfWeek.ToString();
//     switch (dt)
//     {
//         case "Monday":
//             weekday = "星期一";
//             break;
//         case "Tuesday":
//             weekday = "星期二";
//             break;
//         case "Wednesday":
//             weekday = "星期三";
//             break;
//         case "Thursday":
//             weekday = "星期四";
//             break;
//         case "Friday":
//             weekday = "星期五";
//             break;
//         case "Saturday":
//             weekday = "星期六";
//             break;
//         case "Sunday":
//             weekday = "星期日";
//             break;
//     }
//
//     var OccupiedClassroom = "";
//     var keys = DatasHandle.RoomParaClassInfor.Keys.ToArray();
//     var classes = new List<int>();
//
//
//
//     foreach (var pr in PianoroomNames)
//     {
//         foreach (var key in keys)
//         {
//             if (key.Contains(pr))
//             {
//                 OccupiedClassroom = key;
//                 classes = DatasHandle.AutoGetTodayClassByRoom(OccupiedClassroom);
//                 //classes = DatasHandle.getClassesByGivendateAndClassRoom(OccupiedClassroom, new DateTime(2023, 11, 1));
//
//                 HandleTimespans(classes, OccupiedClassroom);
//
//                 break;
//             }
//
//         }
//     }
//     SetTimeSpans(weekday);
//
//     //weekday = "星期一";
//
//     //if (weekday.Equals("星期一"))
//     //{
//     //    var NewTimeSpans = new List<string>();
//
//     //    Timespans.ForEach(ts =>
//     //    {
//     //        NewTimeSpans.Add(ts);
//     //    });
//     //    //NewTimeSpans.Remove("15:00-16:00");
//     //    var IndexVal = NewTimeSpans.IndexOf("15:00-16:00");
//     //    NewTimeSpans[IndexVal] = "";
//
//     //    PianoRooms.ForEach(x => {
//     //        if (x.Contains("创608"))
//     //        {
//     //            PianoRoomsBookingTimespan.Add(x, NewTimeSpans);
//     //        }
//     //        else
//     //        {
//     //            PianoRoomsBookingTimespan.Add(x, Timespans);
//     //        }
//     //    });
//     //}
//     //else if (weekday.Equals("星期五"))
//     //{
//     //    var timespansInweekendList = new List<TimespansInWeekend>();
//     //    var timespansInweekend1 = new TimespansInWeekend { Date = DateTime.Today.AddDays(1).ToString().Replace(" 0:00:00", "") ,Timespans = Timespans ,IsEmpty = false};
//     //    var timespansInweekend2 = new TimespansInWeekend { Date = DateTime.Today.AddDays(2).ToString().Replace(" 0:00:00", ""), Timespans = Timespans ,IsEmpty = false };
//     //    timespansInweekendList.Add(timespansInweekend1);
//     //    timespansInweekendList.Add(timespansInweekend2);
//     //    PianoRooms.ForEach(x =>
//     //    {
//     //        PianoRoomsBookingTimespanInWeekend.Add(x, timespansInweekendList);
//     //    });
//     //}
//     //else
//     //{
//     //    PianoRooms.ForEach(x => {
//     //        PianoRoomsBookingTimespan.Add(x, Timespans);
//     //    });
//     //}
// }
   
//     public static void InitPianoRoomsBookingTimespan()
// {
//
//     PianoRoomsBookingTimespan.Clear();//清空字典
//     PianoRoomsBookingTimespanInWeekend.Clear();//清空字典
//     PianoRoomNamesWithTimespans.Clear();//清空字典
//
//     string dt;
//     string weekday = "";
//     dt = DateTime.Today.DayOfWeek.ToString();
//     switch (dt)
//     {
//         case "Monday":
//             weekday = "星期一";
//             break;
//         case "Tuesday":
//             weekday = "星期二";
//             break;
//         case "Wednesday":
//             weekday = "星期三";
//             break;
//         case "Thursday":
//             weekday = "星期四";
//             break;
//         case "Friday":
//             weekday = "星期五";
//             break;
//         case "Saturday":
//             weekday = "星期六";
//             break;
//         case "Sunday":
//             weekday = "星期日";
//             break;
//     }
//
//     var OccupiedClassroom = "";
//     var keys = DatasHandle.RoomParaClassInfor.Keys.ToArray();
//     var classes = new List<int>();
//
//
//
//     foreach (var pr in PianoroomNames)
//     {
//         foreach (var key in keys)
//         {
//             if (key.Contains(pr))
//             {
//                 OccupiedClassroom = key;
//                 classes = DatasHandle.AutoGetTodayClassByRoom(OccupiedClassroom);
//                 //classes = DatasHandle.getClassesByGivendateAndClassRoom(OccupiedClassroom, new DateTime(2023, 11, 1));
//
//                 HandleTimespans(classes, OccupiedClassroom);
//
//                 break;
//             }
//
//         }
//     }
//     SetTimeSpans(weekday);
//
//     //weekday = "星期一";
//
//     //if (weekday.Equals("星期一"))
//     //{
//     //    var NewTimeSpans = new List<string>();
//
//     //    Timespans.ForEach(ts =>
//     //    {
//     //        NewTimeSpans.Add(ts);
//     //    });
//     //    //NewTimeSpans.Remove("15:00-16:00");
//     //    var IndexVal = NewTimeSpans.IndexOf("15:00-16:00");
//     //    NewTimeSpans[IndexVal] = "";
//
//     //    PianoRooms.ForEach(x => {
//     //        if (x.Contains("创608"))
//     //        {
//     //            PianoRoomsBookingTimespan.Add(x, NewTimeSpans);
//     //        }
//     //        else
//     //        {
//     //            PianoRoomsBookingTimespan.Add(x, Timespans);
//     //        }
//     //    });
//     //}
//     //else if (weekday.Equals("星期五"))
//     //{
//     //    var timespansInweekendList = new List<TimespansInWeekend>();
//     //    var timespansInweekend1 = new TimespansInWeekend { Date = DateTime.Today.AddDays(1).ToString().Replace(" 0:00:00", "") ,Timespans = Timespans ,IsEmpty = false};
//     //    var timespansInweekend2 = new TimespansInWeekend { Date = DateTime.Today.AddDays(2).ToString().Replace(" 0:00:00", ""), Timespans = Timespans ,IsEmpty = false };
//     //    timespansInweekendList.Add(timespansInweekend1);
//     //    timespansInweekendList.Add(timespansInweekend2);
//     //    PianoRooms.ForEach(x =>
//     //    {
//     //        PianoRoomsBookingTimespanInWeekend.Add(x, timespansInweekendList);
//     //    });
//     //}
//     //else
//     //{
//     //    PianoRooms.ForEach(x => {
//     //        PianoRoomsBookingTimespan.Add(x, Timespans);
//     //    });
//     //}
// }
    
    protected static void HandleTimespans(List<int> classes,string className)
    {
        var IndexVal = 0;
        var NewTimeSpans = new List<string>();

        Timespans.ForEach(ts =>
        {
            NewTimeSpans.Add(ts);
        });

        if(classes.Count > 0)
        {
            classes.ForEach(x =>
            {
                IndexVal = NewTimeSpans.IndexOf(timeReference(x));
                NewTimeSpans[IndexVal] = "";

            });
        }

        PianoRoomNamesWithTimespans.Add(className, NewTimeSpans); 
    }
    
    protected static void SetTimeSpans(string weekday)
    {
        if (weekday.Equals("星期五"))
        {
            var timespansInweekendList = new List<TimespansInWeekend>();
            var timespansInweekend1 = new TimespansInWeekend { Date = DateTime.Today.AddDays(1).ToString().Replace(" 0:00:00", ""), Timespans = Timespans, IsEmpty = false };
            var timespansInweekend2 = new TimespansInWeekend { Date = DateTime.Today.AddDays(2).ToString().Replace(" 0:00:00", ""), Timespans = Timespans, IsEmpty = false };
            timespansInweekendList.Add(timespansInweekend1);
            timespansInweekendList.Add(timespansInweekend2);
            PianoRooms.ForEach(x =>
            {
                PianoRoomsBookingTimespanInWeekend.Add(x, timespansInweekendList);
            });
        }
        else
        {
                   

            //NewTimeSpans.Remove("15:00-16:00");
            //IndexVal = NewTimeSpans.IndexOf("15:00-16:00");

            PianoRooms.ForEach(x => {
                //if (x.Contains(classroom))
                //{
                //    PianoRoomsBookingTimespan.Add(x, NewTimeSpans);
                //}
                //else
                //{
                PianoRoomsBookingTimespan.Add(x, Timespans);
                //}
            });

        }
        var rooms = PianoRoomsBookingTimespan.Keys.ToArray();

        PianoRoomNamesWithTimespans.Keys.ToArray().ForEach(x =>
        {
            foreach(var room in rooms)
            {
                if (room.Contains(x))
                {
                    PianoRoomsBookingTimespan[room] = PianoRoomNamesWithTimespans[x];
                }
            }
        });

    }

    [Authorize(AutomaticElectricitySystemPermissions.BookingInformationManagement.BookingInformation.View)]
    public async Task<Volo.Abp.Application.Dtos.PagedResultDto<BookingInformationDto>> GetAllBookingInfor(GetAllBookingInforInput input)
    {
        var result = await _bookingInformationManager.GetListAsync(input);
        // var listDto = new List<BookingInforDto>();
        // foreach (var item in result)
        // {
        //     var dto = ObjectMapper.Map<BookingInformationDto, BookingInforDto>(item);
        //     listDto.Add(dto);
        // }
        // var count =result.Count;
                
            // var page = new Volo.Abp.Application.Dtos.PagedResultDto<BookingInforDto>(count, listDto);
            // var bookingInfoDtoList = new List<BookingInforDto> { result };
            
            return new Volo.Abp.Application.Dtos.PagedResultDto<BookingInformationDto>(result.TotalCount,result.BookingInformationDtoList);
    }

    public async Task<StoreAndInsertNewInforOutput> StoreAndInsertNewInfor(CreateOrUpdateClassroomBookingInput input)
    {
        var bookingRes = "";
        var result = true;
        if (input != null)
        {
            try
            {
                if (PianoRoomsBookingTimespan[input.ClassroomBooking.UsingClassroom].Count == 0)
                {
                    result = false;
                    bookingRes = "抱歉，此教室已被预约满，请换一个教室！";
                }
                else
                {
                    var bookingLimitedHours = 0;
                    var tomorrow = DateTime.Today.AddDays(1).ToString().Replace(" 0:00:00", "");
                    //var dt = DateTime.Now.Date.ToString().Replace(" 0:00:00", "");

                    var bookingLimitedList =new List<BookingLimited>();
                    try
                    {
                        bookingLimitedList =await _bookingLimitedRepository.GetListAsync(x => x.StudentId.Equals(input.ClassroomBooking.StudentId) && x.Date.Equals(tomorrow));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message+"这是错误的地方!!!!");
                        throw;
                    }
                    

                    Console.WriteLine(bookingLimitedList.Count());
                    
                    //var bookingLimitedList = _bookingLimitedRepository.GetAll().Where(x => x.StudentId.Equals(input.ClassroomBooking.StudentId) && x.Date.Equals(tomorrow)).ToList();

                    if (bookingLimitedList.Count != 0)
                    {
                        bookingLimitedList.ForEach(x =>
                        {
                            if (x.Date.Equals(tomorrow))
                            {
                                bookingLimitedHours = x.BookedHours;
                            }
                            else
                            {
                                bookingLimitedHours = 0;
                            }
                        });
                    }

                    if (bookingLimitedHours >= 2)
                    {
                        bookingLimitedHours = 0;
                        result = false;
                        bookingRes = "抱歉，每人每天最多只能预约两个小时！";
                    }
                    else
                    {
                        if (input.ClassroomBooking.BookingTimespan.Contains(";"))
                        {
                            bookingLimitedHours += 2;
                            if (bookingLimitedHours > 2)
                            {
                                bookingLimitedHours = 0;
                                result = false;
                                bookingRes = "抱歉，每人每天最多只能预约两个小时！你已经预约了1个小时，最多只能再预约一个小时！";
                            }
                            else
                            {
                                RidTimespan(input.ClassroomBooking.UsingClassroom, input.ClassroomBooking.BookingTimespan);

                                input.ClassroomBooking.Id = _guidGenerator.Create();
                                input.ClassroomBooking.BookingTimespan = tomorrow + "：" + input.ClassroomBooking.BookingTimespan;
                                var bookingInfor = ObjectMapper.Map<ClassroomBookingEditDto, BookingInformation>(input.ClassroomBooking);
                                await _bookingInformationManager.CreateAsync(bookingInfor.Id, bookingInfor.StudentId, bookingInfor.StudentName, bookingInfor.StudentClass, bookingInfor.TelephoneNumber, bookingInfor.UsingClassroom, bookingInfor.UsingPurpose, bookingInfor.BookingTimespan);

                                var InsertBookingLimited = ObjectMapper.Map<ClassroomBookingEditDto, BookingLimitedEditDto>(input.ClassroomBooking);
                                //var InsertBookingLimited = new BookingLimitedEditDto();
                                //InsertBookingLimited.BookingLimited.Id = SequentialGuidGenerator.Instance.Create();
                                InsertBookingLimited.BookedHours = bookingLimitedHours;
                                InsertBookingLimited.Date = tomorrow;
                                //InsertBookingLimited.StudentId = input.ClassroomBooking.StudentId;
                                //InsertBookingLimited.StudentName = input.ClassroomBooking.StudentName;

                                var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };
                                await CreateBookingLimited(InsertInfor);

                                bookingLimitedHours = 0;
                                bookingRes = "预约成功！";
                            }
                        }
                        else
                        {
                            bookingLimitedHours += 1;
                            if (bookingLimitedHours > 2)
                            {
                                bookingLimitedHours = 0;
                                result = false;
                                bookingRes = "抱歉，每人每天最多只能预约两个小时！";
                            }
                            else
                            {
                                RidTimespan(input.ClassroomBooking.UsingClassroom, input.ClassroomBooking.BookingTimespan);

                                input.ClassroomBooking.Id = _guidGenerator.Create();
                                input.ClassroomBooking.BookingTimespan = tomorrow + "：" + input.ClassroomBooking.BookingTimespan;
                                var bookingInfor = new BookingInformation();
                                try
                                {
                                    bookingInfor = ObjectMapper.Map<ClassroomBookingEditDto, BookingInformation>(input.ClassroomBooking);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message+"这是错误的地方？？？？？？？");
                                    throw;
                                }
                                // var bookingInfor = ObjectMapper.Map<ClassroomBookingEditDto, BookingInformation>(input.ClassroomBooking);
                                await _bookingInformationManager.CreateAsync(bookingInfor.Id, bookingInfor.StudentId, bookingInfor.StudentName, bookingInfor.StudentClass, bookingInfor.TelephoneNumber, bookingInfor.UsingClassroom, bookingInfor.UsingPurpose, bookingInfor.BookingTimespan);

                                var InsertBookingLimited = ObjectMapper.Map<ClassroomBookingEditDto, BookingLimitedEditDto>(input.ClassroomBooking);
                                //var InsertBookingLimited = new BookingLimitedEditDto();
                                //InsertBookingLimited.BookingLimited.Id = SequentialGuidGenerator.Instance.Create();
                                InsertBookingLimited.BookedHours = bookingLimitedHours;
                                InsertBookingLimited.Date = tomorrow;
                                //InsertBookingLimited.StudentId = input.ClassroomBooking.StudentId;
                                //InsertBookingLimited.StudentName = input.ClassroomBooking.StudentName;

                                if (bookingLimitedHours == 1)
                                {
                                    var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };
                                    await CreateBookingLimited(InsertInfor);
                                }
                                else
                                {
                                    var Id = new Guid();

                                    bookingLimitedList.ForEach(x => Id = x.Id);
                                    InsertBookingLimited.Id = Id;
                                    var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };

                                    await UpdateBookingLimited(InsertInfor);
                                }

                                bookingLimitedHours = 0;
                                bookingRes = "预约成功！";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
        }
        return new StoreAndInsertNewInforOutput { SuccessfullyOrNot = result, NewTimeSpansss = PianoRoomsBookingTimespan[input.ClassroomBooking.UsingClassroom], BookingResult = bookingRes };
    }

    public AvailableTimespanOutput GetAvailableTimespanInfor(AvailableTimespanInput input)
    {
        var result = new List<string>();
        if (input != null)
        {
            if (PianoRoomsBookingTimespan.ContainsKey(input.classRoomName))
            {
                result = PianoRoomsBookingTimespan[input.classRoomName];
            }
        }
        else
        {
            result.Add("错误，请选择教室号！");
        }
        return new AvailableTimespanOutput { AvailableTimespan = result };
    }

    public async Task<BookingInWeekendOutput> BookingInWeekend(BookingInWeekendInput input)
    {
        var bookingRes = "";
        var result = true;
        var date = "";
        if (!input.Equals(null))
        {
            try
            {

                foreach (var x in input.BookingInfor.BookingDatas)
                {
                    if (PianoRoomsBookingTimespanInWeekend[x.UsingClassroom].Count == 0)
                    {
                        result = false;
                        bookingRes = $"抱歉，教室{x.UsingClassroom}已被预约满，请换一个教室！";
                    }
                    else
                    {
                        date = x.Date;
                        var bookingLimitedHours = 0;

                        var bookingLimitedList = await _bookingLimitedRepository.GetListAsync(x => x.StudentId.Equals
                            (input.BookingInfor.StudentId) && x.Date.Contains(date));


                        if (bookingLimitedList.Count != 0)
                        {
                            bookingLimitedList.ForEach(x =>
                            {
                                if (x.Date.Equals(date))
                                {
                                    bookingLimitedHours = x.BookedHours;
                                }
                                else
                                {
                                    bookingLimitedHours = 0;
                                }
                            });
                        }

                        if (bookingLimitedHours >= 2)
                        {
                            bookingLimitedHours = 0;
                            result = false;
                            bookingRes = $"抱歉，你在{x.Date}的预约时间已经满了两个小时！";
                        }
                        else
                        {
                            if (x.BookingTimespan.Contains(";"))
                            {
                                bookingLimitedHours += 2;
                                if (bookingLimitedHours > 2)
                                {
                                    bookingLimitedHours = 0;
                                    result = false;
                                    bookingRes = $"抱歉，每人每天最多只能预约两个小时！你在{x.Date}已经预约了1个小时，最多只能再预约一个小时！";
                                }
                                else
                                {
                                    RidTimespanInWeekend(x);

                                    var BookingInforma = ObjectMapper.Map<BookingInWeekendInputDto,ClassroomBookingEditDto>(input.BookingInfor);
                                    BookingInforma.Id = _guidGenerator.Create();

                                    //var BookingInforma = new ClassroomBookingEditDto
                                    //{
                                    //    Id = _guidGenerator.Create(),
                                    //    StudentId = input.BookingInfor.StudentId,
                                    //    UsingPurpose = input.BookingInfor.UsingPurpose,
                                    //    TelephoneNumber = input.BookingInfor.TelephoneNumber,
                                    //    StudentName = input.BookingInfor.StudentName,
                                    //    StudentClass = input.BookingInfor.StudentClass
                                    //};

                                    x.BookingTimespan = x.Date + "：" + x.BookingTimespan;
                                    BookingInforma.UsingClassroom = x.UsingClassroom;
                                    BookingInforma.BookingTimespan = x.BookingTimespan;
                                    var bookingInfor = ObjectMapper.Map<ClassroomBookingEditDto,BookingInformation>(BookingInforma);

                                    await _bookingInformationManager.CreateAsync(bookingInfor.Id,bookingInfor.StudentId,bookingInfor.StudentName,bookingInfor.StudentClass,bookingInfor.TelephoneNumber,bookingInfor.UsingClassroom,bookingInfor.UsingPurpose,bookingInfor.BookingTimespan);
                                    //await _bookingInfoRepository.InsertAsync(bookingInfor);


                                    var InsertBookingLimited = ObjectMapper.Map<BookingInWeekendInputDto, BookingLimitedEditDto>(input.BookingInfor);
                                    //var InsertBookingLimited = new BookingLimitedEditDto();
                                    //InsertBookingLimited.BookingLimited.Id = SequentialGuidGenerator.Instance.Create();
                                    InsertBookingLimited.BookedHours = bookingLimitedHours;
                                    InsertBookingLimited.Date = x.Date;
                                    //InsertBookingLimited.StudentId = input.BookingInfor.StudentId;
                                    //InsertBookingLimited.StudentName = input.BookingInfor.StudentName;

                                    var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };
                                    await CreateBookingLimited(InsertInfor);

                                    bookingLimitedHours = 0;
                                    bookingRes = "预约成功！";
                                }
                            }
                            else
                            {
                                bookingLimitedHours += 1;
                                if (bookingLimitedHours > 2)
                                {
                                    bookingLimitedHours = 0;
                                    result = false;
                                    bookingRes = $"抱歉，你在{x.Date}的预约时间已经满了两个小时！";
                                }
                                else
                                {
                                    RidTimespanInWeekend(x);

                                    var BookingInforma = new ClassroomBookingEditDto();
                                    BookingInforma.Id = _guidGenerator.Create();
                                    BookingInforma.StudentId = input.BookingInfor.StudentId;
                                    BookingInforma.UsingPurpose = input.BookingInfor.UsingPurpose; BookingInforma.TelephoneNumber = input.BookingInfor.TelephoneNumber; BookingInforma.StudentName = input.BookingInfor.StudentName; BookingInforma.StudentClass = input.BookingInfor.StudentClass;

                                    x.BookingTimespan = x.Date + "：" + x.BookingTimespan;
                                    BookingInforma.UsingClassroom = x.UsingClassroom;
                                    BookingInforma.BookingTimespan = x.BookingTimespan;
                                    var bookingInfor = ObjectMapper.Map<ClassroomBookingEditDto,BookingInformation>(BookingInforma);
                                    await _bookingInformationManager.CreateAsync(bookingInfor.Id, bookingInfor.StudentId, bookingInfor.StudentName, bookingInfor.StudentClass, bookingInfor.TelephoneNumber, bookingInfor.UsingClassroom, bookingInfor.UsingPurpose, bookingInfor.BookingTimespan);


                                    var InsertBookingLimited = ObjectMapper.Map<BookingInWeekendInputDto, BookingLimitedEditDto>(input.BookingInfor);
                                    //var InsertBookingLimited = new BookingLimitedEditDto();
                                    //InsertBookingLimited.BookingLimited.Id = SequentialGuidGenerator.Instance.Create();
                                    InsertBookingLimited.BookedHours = bookingLimitedHours;
                                    InsertBookingLimited.Date = x.Date;
                                    //InsertBookingLimited.StudentId = input.BookingInfor.StudentId;
                                    //InsertBookingLimited.StudentName = input.BookingInfor.StudentName;

                                    if (bookingLimitedHours == 1)
                                    {
                                        var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };
                                        await CreateBookingLimited(InsertInfor);
                                    }
                                    else
                                    {
                                        var Id = new Guid();

                                        bookingLimitedList.ForEach(x => Id = x.Id);
                                        InsertBookingLimited.Id = Id;
                                        var InsertInfor = new CreateOrUpdateBookingLimitedInput() { BookingLimited = InsertBookingLimited };

                                        await UpdateBookingLimited(InsertInfor);
                                    }
                                    bookingLimitedHours = 0;
                                    bookingRes = "预约成功！";
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.ToString());
                result = false;
            }
        }

        var ATI = new AvailableTimespanInput { classRoomName = input.BookingInfor.BookingDatas[0].UsingClassroom };
        var newSpans = GetAvailableTimespanInforInWeekend(ATI);

        return new BookingInWeekendOutput { BookingResult = bookingRes, SuccessfullyOrNot = result, NewTimeSpansss = newSpans };
    }
  
    public List<TimespansInWeekend> GetAvailableTimespanInforInWeekend(AvailableTimespanInput input)
    {
        var result = new List<TimespansInWeekend>();
        if (input != null)
        {
            if (PianoRoomsBookingTimespanInWeekend.ContainsKey(input.classRoomName))
            {
                result = PianoRoomsBookingTimespanInWeekend[input.classRoomName];
            }
        }
        return result;
    }

    public async Task<GetClassroomBookingEditOutput> GetClassroomBookingEdit(NullableIdDto<Guid> input)
    {
        var output = new GetClassroomBookingEditOutput();

        if (input.Id != null)
        {
            var booking = await _bookingInformationManager.GetAsync(input.Id.Value);
            output.ClassroomBooking = ObjectMapper.Map<BookingInformation,ClassroomBookingEditDto>(booking);
        }
        else
        {
            output.ClassroomBooking = new ClassroomBookingEditDto();
        }
        return output;
    }

    public async Task CreateClassroomBooking(CreateOrUpdateClassroomBookingInput input)
    {
        //RidTimespan(input.ClassroomBooking.UsingClassroom, input.ClassroomBooking.BookingTimespan);

        //input.ClassroomBooking.Id = SequentialGuidGenerator.Instance.Create();
        //var booking = ObjectMapper.Map<BookingInformation>(input.ClassroomBooking);
        //await _bookingInfoRepository.InsertAsync(booking);


        var identityRes = IdentityVerify(input.ClassroomBooking.StudentId, input.ClassroomBooking.StudentName, input.ClassroomBooking.StudentClass);

        if (identityRes)
        {
            if (PianoRooms.Contains(input.ClassroomBooking.UsingClassroom))
            {
                if (input.ClassroomBooking.BookingTimespan.Contains(";"))
                {
                    var flag = true;
                    var arr = input.ClassroomBooking.BookingTimespan.Split(";");
                    arr.ForEach(x =>
                    {
                        if (!Timespans.Contains(x))
                        {
                            flag = false;
                        }
                    });

                    if (flag)
                    {
                        await StoreAndInsertNewInfor(input);
                    }
                    else
                    {
                        throw new Exception("请正确填写时间段，例如：\"08:00-09:00\"或者\"08:00-09:00;09:00-10:00\"");
                    }
                }
                else
                {
                    if (Timespans.Contains(input.ClassroomBooking.BookingTimespan))
                    {
                        await StoreAndInsertNewInfor(input);
                    }
                    else
                    {
                        throw new Exception("请正确填写时间段，例如：\"08:00-09:00\"或者\"08:00-09:00;09:00-10:00\"");
                    }
                }

            }
            else
            {
                throw new Exception("请正确填写教室,如：\"创512-琴房1\"！");
            }
        }
        else
        {
            throw new Exception("抱歉，您的身份不能预约教室！");
        }
    }

    public async Task UpdateClassroomBooking(CreateOrUpdateClassroomBookingInput input)
    {
        var booking = await _bookingInformationManager.GetAsync(input.ClassroomBooking.Id.Value);

        var identityRes = IdentityVerify(input.ClassroomBooking.StudentId, input.ClassroomBooking.StudentName, input.ClassroomBooking.StudentClass);

        if (identityRes)
        {
            if (PianoRooms.Contains(input.ClassroomBooking.UsingClassroom))
            {
                if (input.ClassroomBooking.BookingTimespan.Contains(";"))
                {
                    var flag = true;
                    var arr = input.ClassroomBooking.BookingTimespan.Split(";");
                    arr.ForEach(x =>
                    {
                        if (!Timespans.Contains(x))
                        {
                            flag = false;
                        }
                    });

                    if (flag)
                    {
                        var IndexLists = new List<int>();

                        arr.ForEach(x => IndexLists.Add(ReferenceTimespans.IndexOf(x)));

                        for (int i = 0; i < arr.Length; i++)
                        {
                            PianoRoomsBookingTimespan[booking.UsingClassroom][IndexLists[i]] = arr[i];
                            //.Insert(IndexLists[i], val[i]);
                        }

                        RidTimespan(input.ClassroomBooking.UsingClassroom, input.ClassroomBooking.BookingTimespan);

                        ObjectMapper.Map(input.ClassroomBooking, booking);
                        await _bookingInformationManager.UpdateAsync(booking);
                    }
                    else
                    {
                        throw new Exception("请正确填写时间段，例如：\"08:00-09:00\"或者\"08:00-09:00;09:00-10:00\"");
                    }
                }
                else
                {
                    if (Timespans.Contains(input.ClassroomBooking.BookingTimespan))
                    {
                        var indexVal = ReferenceTimespans.IndexOf(booking.BookingTimespan);
                        PianoRoomsBookingTimespan[booking.UsingClassroom][indexVal] = booking.BookingTimespan;
                        //.Insert(indexVal, booking.BookingTimespan);

                        RidTimespan(input.ClassroomBooking.UsingClassroom, input.ClassroomBooking.BookingTimespan);

                        ObjectMapper.Map(input.ClassroomBooking, booking);
                        await _bookingInformationManager.UpdateAsync(booking);
                    }
                    else
                    {
                        throw new Exception("请正确填写时间段，例如：\"08:00-09:00\"或者\"08:00-09:00;09:00-10:00\"");
                    }
                }
            }
            else
            {
                throw new Exception("请正确填写教室,如：\"创512-琴房1\"！");
            }
        }
        else
        {
            throw new Exception("抱歉，您的身份不能预约教室！");
        }
    }

    public async Task DeleteClassroomBooking(Volo.Abp.Application.Dtos.EntityDto<List<Guid>> input)
    {
        var booking = new BookingInformation();
        input.Id.ForEach(async x =>
        {
            booking = await _bookingInformationManager.GetAsync(x);

            if (booking.BookingTimespan.Contains(";"))
            {
                var val = booking.BookingTimespan.Split(";");
                var IndexLists = new List<int>();

                val.ForEach(x => IndexLists.Add(ReferenceTimespans.IndexOf(x)));

                for (int i = 0; i < val.Length; i++)
                {
                    PianoRoomsBookingTimespan[booking.UsingClassroom][IndexLists[i]] = val[i];
                }
            }
            else
            {
                var indexVal = ReferenceTimespans.IndexOf(booking.BookingTimespan);
                PianoRoomsBookingTimespan[booking.UsingClassroom][indexVal] = booking.BookingTimespan;
            }
        });

        await _bookingInformationManager.BatchDeleteAsync(input);
    }

    protected void RidTimespanInWeekend(BookingDatas bookingDatas)
    {
        var timepsaninweekend = new TimespansInWeekend();
        var index = 0;

        var temp = new List<TimespansInWeekend>();
        PianoRoomsBookingTimespanInWeekend[bookingDatas.UsingClassroom].ForEach(ii => temp.Add(ii));

        foreach (var p in temp)
        {
            if (p.Date.Equals(bookingDatas.Date))
            {
                timepsaninweekend.Date = p.Date;
                //timepsaninweekend.IsEmpty = p.IsEmpty;
                timepsaninweekend.Timespans = new List<string>();
                p.Timespans.ForEach(x => timepsaninweekend.Timespans.Add(x));
                index = temp.IndexOf(p);

                if (bookingDatas.BookingTimespan.Contains(";"))
                {
                    var val = bookingDatas.BookingTimespan.Split(";");
                    var IndexLists = new List<int>();

                    val.ForEach(p => IndexLists.Add(timepsaninweekend.Timespans.IndexOf(p)));
                    IndexLists.ForEach(h =>
                    {
                        timepsaninweekend.Timespans.RemoveRange(h, 1);
                        timepsaninweekend.Timespans.Insert(h, " ");
                    });

                    var isEmpty = IsEmptyList(timepsaninweekend.Timespans);
                    timepsaninweekend.IsEmpty = isEmpty;
                }
                else
                {
                    var indexVal = timepsaninweekend.Timespans.IndexOf(bookingDatas.BookingTimespan);
                    timepsaninweekend.Timespans.RemoveRange(indexVal, 1);
                    timepsaninweekend.Timespans.Insert(indexVal, " ");
                    var isEmpty = IsEmptyList(timepsaninweekend.Timespans);
                    timepsaninweekend.IsEmpty = isEmpty;
                }
            }
        }

        temp.RemoveRange(index, 1);
        temp.Insert(index, timepsaninweekend);

        PianoRoomsBookingTimespanInWeekend.Remove(bookingDatas.UsingClassroom);
        PianoRoomsBookingTimespanInWeekend.Add(bookingDatas.UsingClassroom, temp);
    }

    public async Task CreateBookingLimited(CreateOrUpdateBookingLimitedInput input)
    {
        input.BookingLimited.Id = _guidGenerator.Create();
        var bookingLimited = ObjectMapper.Map<BookingLimitedEditDto, BookingLimited>(input.BookingLimited);
        await _bookingLimitedRepository.InsertAsync(bookingLimited);
    }
    public async Task UpdateBookingLimited(CreateOrUpdateBookingLimitedInput input)
    {
        var bookingLimited = await _bookingLimitedRepository.GetAsync(input.BookingLimited.Id.Value);
        ObjectMapper.Map(input.BookingLimited, bookingLimited);
        await _bookingLimitedRepository.UpdateAsync(bookingLimited);
    }

    protected bool IsEmptyList(List<string> list)
    {
        bool res = false;
        var flag = 0;

        list.ForEach(x =>
        {
            if (x.Equals(" "))
                flag++;
        });

        if (flag.Equals(13))
            res = true;

        return res;
    }

    protected void RidTimespan(string Classroom, string Timespan)
    {
        var TempList = new List<string>();

        PianoRoomsBookingTimespan[Classroom].ForEach(x => TempList.Add(x));

        if (Timespan.Contains(";"))
        {
            var val = Timespan.Split(";");
            var IndexLists = new List<int>();

            val.ForEach(x => IndexLists.Add(TempList.IndexOf(x)));
            IndexLists.ForEach(x =>
            {
                TempList.RemoveRange(x, 1);
                TempList.Insert(x, " ");
            });
        }
        else
        {
            var indexVal = TempList.IndexOf(Timespan);
            TempList.RemoveRange(indexVal, 1);
            TempList.Insert(indexVal, " ");
        }

        PianoRoomsBookingTimespan.Remove(Classroom);

        PianoRoomsBookingTimespan.Add(Classroom, TempList);
    }
    public bool IdentityVerify(string StudentId, string Name, string ClassName)
    {
        var result = false;
        DataTable dt = ExcelToDatatable(@"D:\Project\AutomaticElectricitySystem\StudentsList.xlsx", "Sheet1", true);
        //将excel表格数据存入list集合中
        //EachdayTX定义的类，字段值对应excel表中的每一列
        List<EachdayTX> eachdayTX = new List<EachdayTX>();
        foreach (DataRow dr in dt.Rows)
        {
            EachdayTX model = new EachdayTX
            {
                StudentId = dr[0].ToString(),//excel表中第一列的值，依次类推
                Name = dr[1].ToString(),
                ClassName = dr[2].ToString(),
            };
            eachdayTX.Add(model);
        }

        eachdayTX.ForEach(x =>
        {
            if (x.StudentId.Equals(StudentId) && x.Name.Equals(Name) && x.ClassName.Equals(ClassName))
            {
                result = true;
            }
        });

        return result;
    }

    public async Task<List<BookingInformationDto>> GetAllInformation()
    {
        return await _bookingInformationManager.GetAll();
    }

    public async Task<GetRoomsUsedConditionOutput> GetRoomsUsedCondition()
    {
        var currentDate1 = DateTime.Now.ToString("yyyy/M/d");
        var currentDate2 = DateTime.Now.ToString("M/d/yyyy");
        var bookingInfor =await GetAllInformation();
        // var lastTwelveHours = GetLastTwelveHours();
        Dictionary<string, double> useRatesOfRooms = new Dictionary<string, double>();

        Dictionary<string, int> timespanCounts = new Dictionary<string, int>();
        DateTime currentTime = DateTime.Now;
        
        var uniqueBookingsToday = bookingInfor
            .Where(x => x.BookingTimespan.Contains(currentDate1) || x.BookingTimespan.Contains(currentDate2))
            .ToList();
        for (int i = 0; i < 12; i++)
        {
            string timespan = currentTime.AddHours(-i - 1).ToString("HH:00") + "-" + currentTime.AddHours(-i).ToString("HH:00");
            timespanCounts[timespan] = 0;  // 初始化计数
        }
        foreach (var booking in uniqueBookingsToday)
        {
            var array = booking.BookingTimespan.Split("：");
            var span = array[1];
                    if (span.Contains(";"))
                    {
                        var timespans = span.Split(';');
                        foreach (var timespan in timespans)
                        {
                            // if (!timespanCounts.ContainsKey(timespan))
                            // {
                            //     timespanCounts[timespan] = 0;
                            // }
                            if(timespanCounts.ContainsKey(span))
                                    timespanCounts[timespan]++;
                        }
                    }
                    else
                    {
                        // if (!timespanCounts.ContainsKey(booking.BookingTimespan))
                        // {
                        //     timespanCounts[booking.BookingTimespan] = 0;
                        // }

                            if(timespanCounts.ContainsKey(span))
                                timespanCounts[span]++;
                    }
                }
        var rooms =await _roomsManager.GetAllClassrooms();
        var count = rooms.Count;
        foreach (var kkk in timespanCounts)
        {
            var re = (double) kkk.Value / count;
            // useRatesOfRooms[kkk.Key] = string.Format("{0:0.00}%", re * 100);
            useRatesOfRooms[kkk.Key] = Math.Round(re, 4) * 100;
        }

        var list = useRatesOfRooms.Values.ToList();
        list.Reverse();
        var list1 = useRatesOfRooms.Keys.ToList();
        list1.Reverse();
        var list2 = timespanCounts.Values.ToList();
        list2.Reverse();
        return new GetRoomsUsedConditionOutput() { UseRatesOfRooms = list, UsedCountAmount = 
            list2,LastTwelveHours = list1};
    }
    
    public List<string> GetLastTwelveHours()
    {
        var now = DateTime.Now;
        var hourRanges = new List<string>();

        for (int i = 12; i > 0; i--)
        {
            string startHour = now.AddHours(-i).ToString("H:00");
            string endHour = now.AddHours(-i + 1).ToString("H:00");
            hourRanges.Add($"{startHour}-{endHour}");
        }

        return hourRanges;
    }

    #region 读取Excel数据
    /// <summary>
    /// 将excel中的数据导入到DataTable中
    /// </summary>
    /// <param name="fileName">文件路径</param>
    /// <param name="sheetName">excel工作薄sheet的名称</param>
    /// <param name="isFirstRowColumn">第一行是否是DataTable的列名，true是</param>
    /// <returns>返回的DataTable</returns>
    public static DataTable ExcelToDatatable(string fileName, string sheetName, bool isFirstRowColumn)
    {
        ISheet sheet = null;
        DataTable data = new DataTable();
        int startRow = 0;
        FileStream fs;
        IWorkbook workbook = null;
        int cellCount = 0;//列数
        int rowCount = 0;//行数
        try
        {
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
            {
                workbook = new XSSFWorkbook(fs);
            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook(fs);
            }
            if (sheetName != null)
            {
                sheet = workbook.GetSheet(sheetName);//根据给定的sheet名称获取数据
            }
            else
            {
                //也可以根据sheet编号来获取数据
                sheet = workbook.GetSheetAt(0);//获取第几个sheet表（此处表示如果没有给定sheet名称，默认是第一个sheet表）  
            }
            if (sheet != null)
            {
                IRow firstRow = sheet.GetRow(0);
                cellCount = firstRow.LastCellNum; //第一行最后一个cell的编号 即总的列数
                if (isFirstRowColumn)//如果第一行是标题行
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)//第一行列数循环
                    {
                        DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);//获取标题
                        data.Columns.Add(column);//添加列
                    }
                    startRow = sheet.FirstRowNum + 1;//1（即第二行，第一行0从开始）
                }
                else
                {
                    startRow = sheet.FirstRowNum;//0
                }
                //最后一行的标号
                rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)//循环遍历所有行
                {
                    IRow row = sheet.GetRow(i);//第几行
                    if (row == null)
                    {
                        continue; //没有数据的行默认是null;
                    }
                    //将excel表每一行的数据添加到datatable的行中
                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                    data.Rows.Add(dataRow);
                }
            }
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return null;
        }
    }
    #endregion

    class EachdayTX
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
    }
}