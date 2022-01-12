using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IReceiptRepository: IBaseRepository<Receipt>
    {
        #region Method
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
        public IEnumerable<Receipt> GetFilter(string receiptCode, string note, int pageIndex, int pageSize);

        /// <summary>
        /// Count Filter chứng từ
        /// </summary>
        /// <param name="receiptCode">mã</param>
        /// <param name="note">nội dung</param>
        /// <returns></returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int GetFilterCount(string receiptCode, string note);
        #endregion
    }
}
