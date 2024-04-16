using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice
{
    /// <summary>
    /// 创建智能空开
    /// </summary>
    public class PagePowerSwitchsOutput
    {
        // /// <summary>
        // /// 智能空开Id
        // /// </summary>
        // public Guid Id { get; set; }
        //
        // /// <summary>
        // /// 教室
        // /// </summary>
        // public Guid RoomId { get; set; }
        // /// <summary>
        // /// 设备序列号
        // /// </summary>
        // public string SerialNumber { get; set; }
        // /// <summary>
        // /// 控制器械名
        // /// </summary>
        // public string ControlledMachineName { get; set; }
        // /// <summary>
        // /// 是否在线
        // /// </summary>
        // public IsOnline IsOnline { get; set; }
        // /// <summary>
        // /// 开关开合闸状态
        // /// </summary>
        // public Status Status { get; set; }
        //
        // /// <summary>
        // /// 创建时间
        // /// </summary>       
        
        public List<PowerSwitchsDto> PowerSwitchsDto { get; set; }
        
        public int TotalCount { get; set; }
    }
}

