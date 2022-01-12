using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class Receipt : BaseEntity
    {
        /// <summary>
        /// Chứng từ Id
        /// </summary>
        [Key]
        public Guid ReceiptId { get; set; }
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        [Required]
        [Duplication]
        public string ReceiptCode { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [Required]
        public DateTime? ReceiptDate { get; set; }
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        [Required]
        public DateTime? IncreaseDate { get; set; }
        /// <summary>
        /// Tổng nguồn kinh phí
        /// </summary>
        public decimal TotalCost { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// List tài sản
        /// </summary>
        [Obj]
        public IEnumerable<FixedAsset> FixedAssets { get; set; }
    }
}
