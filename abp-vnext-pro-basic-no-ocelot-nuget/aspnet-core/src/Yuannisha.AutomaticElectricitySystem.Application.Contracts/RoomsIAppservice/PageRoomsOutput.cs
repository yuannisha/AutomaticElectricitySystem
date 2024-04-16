namespace Yuannisha.AutomaticElectricitySystem.RoomsIAppservice
{
    /// <summary>
    /// 创建教室
    /// </summary>
    public class PageRoomsOutput
    {
        // /// <summary>
        // /// 教室Id
        // /// </summary>
        // public Guid Id { get; set; }
        //
        // /// <summary>
        // /// 楼栋标识
        // /// </summary>
        // public Guid BuildingId { get; set; }
        // /// <summary>
        // /// 楼层标识
        // /// </summary>
        // public int Floor { get; set; }
        // /// <summary>
        // /// 教室号
        // /// </summary>
        // public string No { get; set; }
        // /// <summary>
        // /// 是否在用
        // /// </summary>
        // public UsingOrNot UsingOrNot { get; set; }
        // /// <summary>
        // /// 教室类型
        // /// </summary>
        // public RoomTypeEnum RoomType { get; set; }
        // /// <summary>
        // /// 控制类型
        // /// </summary>
        // public ControlTypeEnum ControlType { get; set; }
        //
        // /// <summary>
        // /// 创建时间
        // /// </summary>       
        // public DateTime CreationTime { get; set; }
        
        public List<RoomList> RoomList { get; set; }
        
        public int TotalCount { get; set; }
    }
}

