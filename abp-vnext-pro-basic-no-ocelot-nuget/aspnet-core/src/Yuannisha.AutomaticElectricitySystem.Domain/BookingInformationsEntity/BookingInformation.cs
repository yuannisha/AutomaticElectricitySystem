using Abp.Domain.Entities;
using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity
{
    /// <summary>
    /// 预约信息
    /// </summary>
    public class BookingInformation : FullAuditedAggregateRoot<Guid> , IEntity<Guid>
    {
        public BookingInformation()
        {
        }


        public BookingInformation(
            Guid id,
            string studentId,
            string studentName,
            string studentClass,
            string telephoneNumber,
            string usingClassroom,
            string usingPurpose,
            string bookingTimespan
        ) : base(id)
        {
            SetStudentId(studentId);
            SetStudentName(studentName);
            SetStudentClass(studentClass);
            SetTelephoneNumber(telephoneNumber);
            SetUsingClassroom(usingClassroom);
            SetUsingPurpose(usingPurpose);
            SetBookingTimespan(bookingTimespan);
        }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get; private set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentName { get; private set; }
        /// <summary>
        /// 学生班级
        /// </summary>
        public string StudentClass { get; private set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelephoneNumber { get; private set; }
        /// <summary>
        /// 使用教室
        /// </summary>
        public string UsingClassroom { get; private set; }
        /// <summary>
        /// 使用用途
        /// </summary>
        public string UsingPurpose { get; private set; }
        /// <summary>
        /// 预约时间段
        /// </summary>
        public string BookingTimespan { get; private set; }



        /// <summary>
        /// 设置学号
        /// </summary>        
        private void SetStudentId(string studentId)
        {
            Guard.NotNullOrWhiteSpace(studentId, nameof(studentId), 15, 0);
            StudentId = studentId;
        }

        /// <summary>
        /// 设置学生名字
        /// </summary>        
        private void SetStudentName(string studentName)
        {
            Guard.NotNullOrWhiteSpace(studentName, nameof(studentName), 50, 0);
            StudentName = studentName;
        }

        /// <summary>
        /// 设置学生班级
        /// </summary>        
        private void SetStudentClass(string studentClass)
        {
            Guard.NotNullOrWhiteSpace(studentClass, nameof(studentClass), 50, 0);
            StudentClass = studentClass;
        }

        /// <summary>
        /// 设置电话号码
        /// </summary>        
        private void SetTelephoneNumber(string telephoneNumber)
        {
            Guard.NotNullOrWhiteSpace(telephoneNumber, nameof(telephoneNumber), 11, 0);
            TelephoneNumber = telephoneNumber;
        }

        /// <summary>
        /// 设置使用教室
        /// </summary>        
        private void SetUsingClassroom(string usingClassroom)
        {
            Guard.NotNullOrWhiteSpace(usingClassroom, nameof(usingClassroom), 50, 0);
            UsingClassroom = usingClassroom;
        }

        /// <summary>
        /// 设置使用用途
        /// </summary>        
        private void SetUsingPurpose(string usingPurpose)
        {
            Guard.NotNullOrWhiteSpace(usingPurpose, nameof(usingPurpose), 50, 0);
            UsingPurpose = usingPurpose;
        }

        /// <summary>
        /// 设置预约时间段
        /// </summary>        
        private void SetBookingTimespan(string bookingTimespan)
        {
            Guard.NotNullOrWhiteSpace(bookingTimespan, nameof(bookingTimespan), 80, 0);
            BookingTimespan = bookingTimespan;
        }

        /// <summary>
        /// 更新预约信息
        /// </summary> 
        public void Update(
            string studentId,
            string studentName,
            string studentClass,
            string telephoneNumber,
            string usingClassroom,
            string usingPurpose,
            string bookingTimespan
        )
        {
            SetStudentId(studentId);
            SetStudentName(studentName);
            SetStudentClass(studentClass);
            SetTelephoneNumber(telephoneNumber);
            SetUsingClassroom(usingClassroom);
            SetUsingPurpose(usingPurpose);
            SetBookingTimespan(bookingTimespan);
        }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
    }
}

