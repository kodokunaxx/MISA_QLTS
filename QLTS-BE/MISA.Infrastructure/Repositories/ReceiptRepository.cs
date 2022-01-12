using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure.Repositories
{
    /// <summary>
    /// ReceiptRepository
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        #region Declare
        #endregion

        #region Property
        #endregion

        #region Constructor
        public ReceiptRepository()
        {
        }
        #endregion

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
        public IEnumerable<Receipt> GetFilter(string receiptCode, string note, int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetReceiptFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$ReceiptCode", receiptCode);
            dynamicParameters.Add("@$Note", note);
            dynamicParameters.Add("@$PageIndex", pageIndex);
            dynamicParameters.Add("@$PageSize", pageSize);
            dynamicParameters.Add("@$IsCount", null);

            return _dbConnection.Query<Receipt>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Count Filter chứng từ
        /// </summary>
        /// <param name="receiptCode">mã</param>
        /// <param name="note">nội dung</param>
        /// <returns></returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int GetFilterCount(string receiptCode, string note)
        {
            string sqlCommand = "Proc_GetReceiptFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$ReceiptCode", receiptCode);
            dynamicParameters.Add("@$Note", note);
            dynamicParameters.Add("@$PageIndex", null);
            dynamicParameters.Add("@$PageSize", null);
            dynamicParameters.Add("@$IsCount", 1);

            return _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }
        #endregion

    }
}
