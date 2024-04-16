using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;

public class RoomList
{
    
    public Guid Id { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 楼栋标识
    /// </summary>
    public Guid BuildingId { get; set; }
    /// <summary>
    /// 楼层标识
    /// </summary>
    public int Floor { get; set; }
    /// <summary>
    /// 教室号
    /// </summary>
    public string No { get; set; }
    /// <summary>
    /// 是否在用
    /// </summary>
    public UsingOrNot IsUsingOrNot { get; set; }
    /// <summary>
    /// 教室类型
    /// </summary>
    public RoomTypeEnum RoomType { get; set; }
    /// <summary>
    /// 控制类型
    /// </summary>
    public ControlTypeEnum ControlType { get; set; }
}