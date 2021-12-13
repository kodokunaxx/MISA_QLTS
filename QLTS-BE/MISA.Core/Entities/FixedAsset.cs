using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class FixedAsset : BaseEntity
    {
        #region Declare

        #endregion

        #region Property
        /// <summary>
        /// Tài sản Id
        /// </summary>
        [Key]
        public Guid FixedAssetId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required]
        [Duplication]
        public string FixedAssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        [Required]
        public string FixedAssetName { get; set; }
        /// <summary>
        /// Phòng ban Id
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Loại tài sản Id
        /// </summary>
        [Required]
        public Guid FixedAssetCategoryId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        [Required]
        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        [Required]
        public decimal? Cost { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        [Required]
        public int? Quantity { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [Required]
        public float? DepreciationRate { get; set; }
        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int? TrackedYear { get; set; }
        /// <summary>
        /// Số năm sử dụng 
        /// </summary>
        [Required]
        public int? LifeTime { get; set; }
        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int? ProductionYear { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [Required]
        public DateTime? UseDate { get; set; }
        /// <summary>
        /// Sử dụng: Đã - Chưa
        /// </summary>
        public bool? Active { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region Method

        #endregion
    }
}
