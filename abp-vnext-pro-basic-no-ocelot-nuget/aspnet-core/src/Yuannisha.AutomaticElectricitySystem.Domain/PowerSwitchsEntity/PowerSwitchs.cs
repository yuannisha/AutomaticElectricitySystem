using Abp.Domain.Entities;
using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities.Auditing;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity
{
    /// <summary>
    /// 智能空开
    /// </summary>
    public class PowerSwitchs : FullAuditedAggregateRoot<Guid>, IEntity<Guid>
    {
        private PowerSwitchs()
        {
        }


        public PowerSwitchs(
            Guid id,
            Guid roomId,
            string serialNumber,
            string controlledMachineName,
            IsOnline isOnline,
            Status status,
            IsAbnormalOrNot isAbnormal = IsAbnormalOrNot.IsNotAbnormal,
            double energyConsumption= 0
        ) 
        {
            SetId(id);
            SetRoomId(roomId);
            SetSerialNumber(serialNumber);
            SetControlledMachineName(controlledMachineName);
            SetIsOnline(isOnline);
            SetStatus(status);
            SetIsAbnormal(isAbnormal);
            SetEnergyConsumption(energyConsumption);
        }

        private void SetId(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 教室
        /// </summary>
        public Guid RoomId { get; private set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SerialNumber { get; private set; }
        /// <summary>
        /// 控制器械名
        /// </summary>
        public string ControlledMachineName { get; private set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public IsOnline IsOnline { get; private set; }
        /// <summary>
        /// 开关开合闸状态
        /// </summary>
        public Status Status { get; private set; }
        /// <summary>
        /// 空开是否异常，0为正常1为异常
        /// </summary>
        public IsAbnormalOrNot IsAbnormal { get; set; }
        /// <summary>
        /// 空开开闸时间总计,单位为秒
        /// </summary>
        public double EnergyConsumption { get; set; }


        private void SetEnergyConsumption(double energyConsumption)
        {
            EnergyConsumption = energyConsumption;
        }

        /// <summary>
        /// 设置教室
        /// </summary>        
        private void SetRoomId(Guid roomId)
        {
            RoomId = roomId;
        }

        /// <summary>
        /// 设置设备序列号
        /// </summary>        
        private void SetSerialNumber(string serialNumber)
        {
            Guard.NotNullOrWhiteSpace(serialNumber, nameof(serialNumber),  14, 0);
            SerialNumber = serialNumber;
        }

        /// <summary>
        /// 设置控制器械名
        /// </summary>        
        private void SetControlledMachineName(string controlledMachineName)
        {
            Guard.Length(controlledMachineName, nameof(controlledMachineName), 50, 0);
            ControlledMachineName = controlledMachineName;
        }

        /// <summary>
        /// 设置是否在线
        /// </summary>
        private void SetIsOnline(IsOnline isOnline)
        {
            IsOnline = isOnline;
        }

        /// <summary>
        /// 设置开关开合闸状态
        /// </summary>
        private void SetStatus(Status status)
        {
            Status = status;
        }

        private void SetIsAbnormal(IsAbnormalOrNot isAbnormal)
        {
            IsAbnormal = isAbnormal;
        }
        
        /// <summary>
        /// 更新智能空开
        /// </summary> 
        public void Update(
            Guid roomId,
            string serialNumber,
            string controlledMachineName,
            IsOnline isOnline,
            Status status,
            IsAbnormalOrNot isAbnormal,
            double energyConsumption
        )
        {
            SetRoomId(roomId);
            SetSerialNumber(serialNumber);
            SetControlledMachineName(controlledMachineName);
            SetIsOnline(isOnline);
            SetStatus(status);
            SetIsAbnormal(isAbnormal);
            SetEnergyConsumption(energyConsumption);
        }
        #region Foreign Keys

        /// <summary>
        /// 所属 
        /// </summary>
        public virtual RoomsEntity.Rooms Room { get; set; } // FK_PowerSwitch_Room


        #endregion Foreign Keys

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
    }
}

