using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice
{
    /// <summary>
    /// 删除教学楼
    /// </summary>
    public class UpdateBuildingsInput
    {
        /// <summary>
        /// 教学楼Id
        /// </summary>
        public Guid Id { get; set; }

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

