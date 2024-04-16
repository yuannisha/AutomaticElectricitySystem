using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity
{
    /// <summary>
    /// 预约限制信息
    /// </summary>
    public class BookingLimited : FullAuditedAggregateRoot<Guid>
    {
        private BookingLimited()
        {
        }


        public BookingLimited(
            Guid id,
            string studentId,
            string studentName,
            string date,
            int bookedHours
        ) : base(id)
        {
            SetStudentId(studentId);
            SetStudentName(studentName);
            SetDate(date);
            SetBookedHours(bookedHours);
        }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get; private set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; private set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string Date { get; private set; }
        /// <summary>
        /// 已预约小时数
        /// </summary>
        public int BookedHours { get; private set; }



        /// <summary>
        /// 设置学号
        /// </summary>        
        private void SetStudentId(string studentId)
        {
            Guard.NotNullOrWhiteSpace(studentId, nameof(studentId),  15, 0);
            StudentId = studentId;
        }

        /// <summary>
        /// 设置学生姓名
        /// </summary>        
        private void SetStudentName(string studentName)
        {
            Guard.NotNullOrWhiteSpace(studentName, nameof(studentName),  50, 0);
            StudentName = studentName;
        }

        /// <summary>
        /// 设置预约日期
        /// </summary>        
        private void SetDate(string date)
        {
            Guard.NotNullOrWhiteSpace(date, nameof(date),  50, 0);
            Date = date;
        }

        /// <summary>
        /// 设置已预约小时数
        /// </summary>        
        private void SetBookedHours(int bookedHours)
        {
            BookedHours = bookedHours;
        }

        /// <summary>
        /// 更新预约限制信息
        /// </summary> 
        public void Update(
            string studentId,
            string studentName,
            string date,
            int bookedHours
        )
        {
            SetStudentId(studentId);
            SetStudentName(studentName);
            SetDate(date);
            SetBookedHours(bookedHours);
        }
    }
}

