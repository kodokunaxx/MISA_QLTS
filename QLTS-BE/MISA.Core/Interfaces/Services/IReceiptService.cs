using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    public interface IReceiptService : IBaseService<Receipt>
    {
        /// <summary>
        /// Filter paging chứng từ
        /// </summary>
        /// <param name="receiptCode">mã</param>
        /// <param name="note">nội dung</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetFilter(string receiptCode, string note, int pageIndex, int pageSize);


        /// <summary>
        /// Lấy mã chứng từ mới nhất
        /// </summary>
        /// <returns>new code</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetNewCode();
    }
}
