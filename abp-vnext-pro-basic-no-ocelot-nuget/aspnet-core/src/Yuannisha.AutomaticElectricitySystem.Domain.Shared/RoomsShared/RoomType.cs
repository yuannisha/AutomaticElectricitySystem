using System.ComponentModel;

namespace Yuannisha.AutomaticElectricitySystem.RoomsShared
{
    /// <summary>
    /// 教室类型
    /// </summary>
    public enum RoomTypeEnum
    {
        DefaultValue = 0,
        [Description("普通")] NormalRoom = 3,
        [Description("多媒体")] MultiMediaRoom = 1,
        [Description("机房")] ComputerRoom = 2,
    }
}

