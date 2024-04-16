using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice
{
    /// <summary>
    /// 创建教学楼
    /// </summary>
    public class CreateBuildingsInput
    {
        /// <summary>
        /// 楼栋名称
        /// </summary>
        [Required(ErrorMessage = "楼栋名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        public int DisplayOrder { get; set; }
    }
}

