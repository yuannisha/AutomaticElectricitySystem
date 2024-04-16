using Yuannisha.AutomaticElectricitySystem.BuildingsShared;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice
{
    /// <summary>
    /// 分页查询教学楼输出结果
    /// </summary>
    public class PageBuildingsOutput
    {
        // /// <summary>
        // /// 教学楼Id
        // /// </summary>
        // public Guid Id { get; set; }
        //
        // /// <summary>
        // /// 楼栋名称
        // /// </summary>
        // public string Name { get; set; }
        // /// <summary>
        // /// 排序
        // /// </summary>
        // public int DisplayOrder { get; set; }
        //
        // /// <summary>
        // /// 创建时间
        // /// </summary>       
        // public DateTime CreationTime { get; set; }
        
        public List<BuildingsDto> BuildingsDto { get; set; }
        
        public int TotalCount { get; set; }
    }
}

