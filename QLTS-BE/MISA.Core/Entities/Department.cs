using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Phòng ban id
        /// </summary>
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
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

    }
}
