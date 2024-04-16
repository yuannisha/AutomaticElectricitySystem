using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;
    /// <summary>
    /// 是否异常
    /// </summary>
    public enum IsAbnormalOrNot
    {
        DefaultValue = 0,
        [Description("异常")] IsAbnormal = 1,
        [Description("正常")] IsNotAbnormal = 2,
    }