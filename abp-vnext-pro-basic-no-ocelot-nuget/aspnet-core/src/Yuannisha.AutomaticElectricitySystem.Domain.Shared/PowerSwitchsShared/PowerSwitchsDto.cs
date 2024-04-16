namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared
{
    /// <summary>
    /// 智能空开
    /// </summary>
    public class PowerSwitchsDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 教室
        /// </summary>
        public Guid RoomId { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 控制器械名
        /// </summary>
        public string ControlledMachineName { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public IsOnline IsOnline { get; set; }
        /// <summary>
        /// 开关开合闸状态
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 空开是否异常，0为正常1为异常
        /// </summary>
        public IsAbnormalOrNot IsAbnormal { get; set; }
        /// <summary>
        /// 空开开闸时间总计,单位为秒
        /// </summary>
        public double EnergyConsumption { get; set; }
    }
}

