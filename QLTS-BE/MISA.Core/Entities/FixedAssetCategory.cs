using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class FixedAssetCategory : BaseEntity
    {
        public Guid FixedAssetCategoryId { get; set; }
        public string FixedAssetCategoryCode { get; set; }
        public string FixedAssetCategoryName { get; set; }
        public float? DepreciationRate { get; set; }
        public int? LifeTime { get; set; }
        public string Description { get; set; }

    }
}
