using Abp.Domain.Entities.Auditing;
using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsEntity
{
    /// <summary>
    /// 教室
    /// </summary>
    public class Rooms : FullAuditedAggregateRoot<Guid>,IEntity<Guid>
    {
        private Rooms()
        {
        }


        public Rooms(
            Guid id,
            Guid buildingId,
            int floor,
            string no,
            UsingOrNot usingOrNot, 
            RoomTypeEnum roomType,
            ControlTypeEnum controlType
        )
        {
            SetId(id);
            SetBuildingId(buildingId);
            SetFloor(floor);
            SetNo(no);
            SetUsingOrNot(usingOrNot);
            SetRoomType(roomType);
            SetControlType(controlType);
        }

        public bool IsTransient()
        {
            return Id == default(Guid);
        }

        /// <summary>
        /// 楼栋标识
        /// </summary>
        public Guid BuildingId { get; private set; }
        /// <summary>
        /// 楼层标识
        /// </summary>
        public int Floor { get; private set; }
        /// <summary>
        /// 教室号
        /// </summary>
        public string No { get; private set; }
        /// <summary>
        /// 是否在用
        /// </summary>
        public UsingOrNot IsUsingOrNot { get; private set; }
        /// <summary>
        /// 教室类型
        /// </summary>
        public RoomTypeEnum RoomType { get; private set; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public ControlTypeEnum ControlType { get; private set; }


        /// <summary>
        /// 设置主键
        /// </summary>        
        private void SetId(Guid Id)
        {
            BuildingId = Id;
        }
        
        /// <summary>
        /// 设置楼栋标识
        /// </summary>        
        private void SetBuildingId(Guid buildingId)
        {
            BuildingId = buildingId;
        }

        /// <summary>
        /// 设置楼层标识
        /// </summary>        
        private void SetFloor(int floor)
        {
            Floor = floor;
        }

        /// <summary>
        /// 设置教室号
        /// </summary>        
        private void SetNo(string no)
        {
            Guard.Length(no, nameof(no), 50, 0);
            No = no;
        }

        /// <summary>
        /// 设置是否在用
        /// </summary>        
        private void SetUsingOrNot(UsingOrNot usingOrNot)
        {
            IsUsingOrNot = usingOrNot;
        }

        /// <summary>
        /// 设置教室类型
        /// </summary>
        private void SetRoomType(RoomTypeEnum roomType)
        {
            RoomType = roomType;
        }

        /// <summary>
        /// 设置控制类型
        /// </summary>
        private void SetControlType(ControlTypeEnum controlType)
        {
            ControlType = controlType;
        }

        /// <summary>
        /// 更新教室
        /// </summary> 
        public void Update(
            Guid buildingId,
            int floor,
            string no,
            UsingOrNot usingOrNot,
            RoomTypeEnum roomType,
            ControlTypeEnum controlType
        )
        {
            SetBuildingId(buildingId);
            SetFloor(floor);
            SetNo(no);
            SetUsingOrNot(usingOrNot);
            SetRoomType(roomType);
            SetControlType(controlType);
        }
        #region Reverse navigation      

        /// <summary>
        ///  子项
        /// </summary>
        public virtual ICollection<PowerSwitchs> PowerSwitches { get; set; } = new List<PowerSwitchs>(); // Basics_PowerSwitch.FK_PowerSwitch_Room



        #endregion Reverse navigation      



        #region Foreign Keys

        /// <summary>
        /// 所属 
        /// </summary>
        public virtual BuildingsEntity.Buildings Building { get; set; } // FK_Room_Building

        #endregion


        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}

