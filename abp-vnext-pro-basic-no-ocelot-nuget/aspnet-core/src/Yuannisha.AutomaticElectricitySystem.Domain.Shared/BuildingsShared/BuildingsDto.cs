namespace Yuannisha.AutomaticElectricitySystem.BuildingsShared
{
    /// <summary>
    /// 教学楼
    /// </summary>
    public class BuildingsDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}

