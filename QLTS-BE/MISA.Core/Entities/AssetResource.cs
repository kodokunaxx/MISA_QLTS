using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class AssetResource : BaseEntity
    {
        /// <summary>
        /// Tài sản nguồn Id
        /// </summary>
        public Guid? AssetResourceId { get; set; }
        /// <summary>
        /// Tài sản id
        /// </summary>
        public Guid FixedAssetId { get; set; }
        /// <summary>
        /// Nguồn Id
        /// </summary>
        public Guid ResourceId { get; set; }
        /// <summary>
        /// Nguồn kinh phí
        /// </summary>
        public decimal? Budget { get; set; }
        /// <summary>
        /// Nguồn
        /// </summary>
        [Obj]
        public Resource Resource { get; set; }
    }
}
