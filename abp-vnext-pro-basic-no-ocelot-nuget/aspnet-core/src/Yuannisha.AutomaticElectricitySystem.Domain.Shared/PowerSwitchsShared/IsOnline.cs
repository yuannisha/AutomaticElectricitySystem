using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared
{
    /// <summary>
    /// 是否在线
    /// </summary>
    public enum IsOnline
    {
        DefaultValue = 0,
        [Description("在线")] On = 1,
        [Description("离线")] Off = 2,
    }
}

