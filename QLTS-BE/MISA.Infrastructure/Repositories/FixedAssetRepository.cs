using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
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

        // <summary>
        /// Lấy mã TS mới nhất
        /// </summary>
        /// <returns>Mã TS mới nhất</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public string GetLastCode()
        {
            string sqlCommand = "Proc_GetLastestFixedAssetCode";
            return _dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
        }
        #endregion

    }
}
