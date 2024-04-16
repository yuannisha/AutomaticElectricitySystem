using System.ComponentModel.DataAnnotations;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsIAppservice
{
    /// <summary>
    /// 删除教室
    /// </summary>
    public class UpdateRoomsInput
    {
        /// <summary>
        /// 教室Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 楼栋标识
        /// </summary>
        [Required(ErrorMessage = "楼栋标识不能为空")]
        public Guid BuildingId { get; set; }
        /// <summary>
        /// 楼层标识
        /// </summary>
        [Required(ErrorMessage = "楼层标识不能为空")]
        public int Floor { get; set; }
        /// <summary>
        /// 教室号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 是否在用
        /// </summary>
        [Required(ErrorMessage = "是否在用不能为空")]
        public UsingOrNot UsingOrNot { get; set; }
        /// <summary>
        /// 教室类型
        /// </summary>
        public RoomTypeEnum RoomType { get; set; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public ControlTypeEnum ControlType { get; set; }
    } 
}

