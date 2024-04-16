using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.RoomsShared;

public enum UsingOrNot
{
    DefaultValue = 0,
    [Description("未使用")] IsNotUsing = 2,
    [Description("正在使用")] IsUsing = 1,
}