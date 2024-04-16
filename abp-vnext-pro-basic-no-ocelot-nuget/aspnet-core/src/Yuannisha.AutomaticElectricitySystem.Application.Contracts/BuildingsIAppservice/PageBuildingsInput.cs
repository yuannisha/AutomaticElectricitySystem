using Lion.AbpPro.Core;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice
{
    /// <summary>
    /// 创建教学楼
    /// </summary>
    public class PageBuildingsInput : PagingBase
    {
        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string Name { get; set; }
    }
}

