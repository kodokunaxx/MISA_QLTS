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
    /// FixedAssetRepository
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        #region Declare
        #endregion

        #region Property
        #endregion

        #region Constructor
        public FixedAssetRepository()
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetAll(int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetFixedAssetPaging";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$PageIndex", pageIndex);
            dynamicParameters.Add("@$PageSize", pageSize);
            dynamicParameters.Add("@$IsCount", null);

            return _dbConnection.Query<FixedAsset>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public int GetCount()
        {
            string sqlCommand = "Proc_GetFixedAssetPaging";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$PageIndex", null);
            dynamicParameters.Add("@$PageSize", null);
            dynamicParameters.Add("@$IsCount", 1);

            return _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy bản ghi qua keyword
        /// </summary>
        /// <param name="fixedAssetCode">Mã tài sản</param>
        /// <param name="fixedAssetName">Tên tài sản</param>
        /// <param name="departmentId">Phòng ban Id</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản Id</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetFilter(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize)
        {
            string sqlCommand = "Proc_GetFixedAssetFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$FixedAssetCode", fixedAssetCode);
            dynamicParameters.Add("@$FixedAssetName", fixedAssetName);
            dynamicParameters.Add("@$DepartmentId", departmentId);
            dynamicParameters.Add("@$FixedAssetCategoryId", fixedAssetCategoryId);
            dynamicParameters.Add("@$PageIndex", pageIndex);
            dynamicParameters.Add("@$PageSize", pageSize);
            dynamicParameters.Add("@$IsCount", null);

            return _dbConnection.Query<FixedAsset>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Đếm số bản ghi qua keyword
        /// </summary>
        /// <param name="fixedAssetCode">Mã tài sản</param>
        /// <param name="fixedAssetName">Tên tài sản</param>
        /// <param name="departmentId">Phòng ban Id</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản Id</param>
        /// <returns>Số bản ghi</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int GetFilterCount(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId)
        {
            string sqlCommand = "Proc_GetFixedAssetFilter";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$FixedAssetCode", fixedAssetCode);
            dynamicParameters.Add("@$FixedAssetName", fixedAssetName);
            dynamicParameters.Add("@$DepartmentId", departmentId);
            dynamicParameters.Add("@$FixedAssetCategoryId", fixedAssetCategoryId);
            dynamicParameters.Add("@$PageIndex", null);
            dynamicParameters.Add("@$PageSize", null);
            dynamicParameters.Add("@$IsCount", 1);

            return _dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo chứng từ Id
        /// </summary>
        /// <param name="receiptId">chứng từ id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetByReceiptId(Guid receiptId)
        {
            string sqlCommand = $"Proc_GetFixedAssetByReceiptId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@$ReceiptId", receiptId.ToString());

            return _dbConnection.Query<FixedAsset>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Cập nhật các bản ghi dựa theo chứng từ Id và tài sản id
        /// </summary>
        /// <param name="receiptId">chứng từ Id</param>
        /// <param name="listId">danh sách tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int UpdateByReceiptId(Guid receiptId, string[] listId)
        {
            string sqlCommand = "UPDATE FixedAsset fa SET fa.ReceiptId = NULL, fa.Active = FALSE WHERE fa.ReceiptId = @receiptId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@receiptId", receiptId);
            _dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.Text);

            if (listId.Length == 0) return 0;

            sqlCommand = "UPDATE FixedAsset fa SET fa.ReceiptId = @receiptId, fa.Active = TRUE WHERE fa.FixedAssetId IN @listId";
            dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@receiptId", receiptId);
            dynamicParameters.Add($"@listId", listId);
            var rowEffect = _dbConnection.Execute(sqlCommand, param: dynamicParameters, commandType: CommandType.Text);

            return rowEffect;
        }

        /// <summary>
        /// Filter paging tài sản không có chứng từ
        /// </summary>
        /// <param name="fixedAssetCode">mã</param>
        /// <param name="fixedAssetName">tên</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetByReceiptIdNull(string fixedAssetCode, string fixedAssetName)
        {
            string sqlCommand = "Proc_GetFixedAssetByReceiptIdNull";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@$FixedAssetCode", fixedAssetCode);
            dynamicParameters.Add("@$FixedAssetName", fixedAssetName);

            return _dbConnection.Query<FixedAsset>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        #endregion

    }
}
