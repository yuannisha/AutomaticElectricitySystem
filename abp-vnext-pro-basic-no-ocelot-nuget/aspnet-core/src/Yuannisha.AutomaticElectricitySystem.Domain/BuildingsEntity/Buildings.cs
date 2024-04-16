using Abp.Domain.Entities;
using Lion.AbpPro.Core;
using Volo.Abp.Domain.Entities.Auditing;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsEntity
{
    /// <summary>
    /// 教学楼
    /// </summary>
    public class Buildings : FullAuditedAggregateRoot<Guid>, IEntity<Guid>
    {
        private Buildings()
        {
        }


        public Buildings(
            Guid id,
            string name,
            int displayOrder
        ) : base(id)
        {
            SetId(id);
            SetName(name);
            SetDisplayOrder(displayOrder);
        }

        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; private set; }


        /// <summary>
        /// 设置楼栋名称
        /// </summary>        
        private void SetId(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 设置楼栋名称
        /// </summary>        
        private void SetName(string name)
        {
            Guard.NotNullOrWhiteSpace(name, nameof(name),  50, 0);
            Name = name;
        }

        /// <summary>
        /// 设置排序
        /// </summary>        
        private void SetDisplayOrder(int displayOrder)
        {
            DisplayOrder = displayOrder;
        }

        /// <summary>
        /// 更新教学楼
        /// </summary> 
        public void Update(
            string name,
            int displayOrder
        )
        {
            SetName(name);
            SetDisplayOrder(displayOrder);
        }

        #region Reverse navigation      

        /// <summary>
        ///  子项
        /// </summary>
        public virtual ICollection<Rooms> Rooms { get; set; } = new List<Rooms>(); // Basics_Room.FK_Room_Building



        #endregion Reverse navigation

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
    }
}

