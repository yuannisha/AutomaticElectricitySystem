using System.ComponentModel.DataAnnotations;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice
{
    /// <summary>
    /// 创建智能空开
    /// </summary>
    public class CreatePowerSwitchsInput
    {
        /// <summary>
        /// 教室
        /// </summary>
        [Required(ErrorMessage = "教室不能为空")]
        public Guid RoomId { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        [Required(ErrorMessage = "设备序列号不能为空")]
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
        
    }
}

