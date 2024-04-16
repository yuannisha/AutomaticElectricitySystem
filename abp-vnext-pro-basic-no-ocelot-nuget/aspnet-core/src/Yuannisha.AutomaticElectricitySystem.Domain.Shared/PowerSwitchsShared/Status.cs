using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared
{
    /// <summary>
    /// 开关开合闸状态
    /// </summary>
    public enum Status
    {

        DefaultValue = 0,
        [Description("开闸")] Open = 1,
        [Description("关闸")] Close = 2,
    }
}

