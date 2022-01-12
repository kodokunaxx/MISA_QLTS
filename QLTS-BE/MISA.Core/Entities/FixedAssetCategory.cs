using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class FixedAssetCategory : BaseEntity
    {
        /// <summary>
        /// Loại tài sản Id
        /// </summary>
        public Guid FixedAssetCategoryId { get; set; }
        /// <summary>
        /// mã loại tài sản
        /// </summary>
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// tên loại tài sản
        /// </summary>
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public float? DepreciationRate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? LifeTime { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

    }
}
