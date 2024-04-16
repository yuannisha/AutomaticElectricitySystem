using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.RoomsShared
{
    /// <summary>
    /// 控制类型
    /// </summary>
    public enum ControlTypeEnum
    {
        DefaultValue = 0,
        [Description("自动")] Auto = 2,
        [Description("手动")] Manual = 1,
    }
}

