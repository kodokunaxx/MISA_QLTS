using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Resource: BaseEntity
    {
        /// <summary>
        /// Nguồn id
        /// </summary>
        public Guid ResourceId { get; set; }
        /// <summary>
        /// Mã nguồn
        /// </summary>
        public string ResourceCode { get; set; }
        /// <summary>
        /// Tên nguồn
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Note { get; set; }
    }
}
