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
    /// AssetResourceRepository
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class AssetResourceRepository : BaseRepository<AssetResource>, IAssetResourceRepository
    {
        #region Declare
        #endregion

        #region Property
        #endregion

        #region Constructor
        public AssetResourceRepository()
        {
        }
        /// <summary>
        /// Xóa các bản ghi theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int DeleteByFixedAssetId(string fixedAssetId)
        {
            int affectedRow = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                string sql = $"DELETE FROM AssetResource WHERE FixedAssetId = @fixedAssetId;";

                dynamicParameters.Add($"@fixedAssetId", fixedAssetId);

                affectedRow =  _dbConnection.Execute(sql, param: dynamicParameters, transaction: transaction);
                transaction.Commit();
            }
            return affectedRow;
        }

        /// <summary>
        /// Lấy danh sách tài sản nguồn theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<AssetResource> GetByFixedAssetId(Guid fixedAssetId)
        {
            string sqlCommand = $"Proc_GetAssetResourceByFixedAssetId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@$FixedAssetId", fixedAssetId.ToString());

            return _dbConnection.Query<AssetResource>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Thêm nhiều bản ghi 
        /// </summary>
        /// <param name="assetResources"></param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int InsertByFixedAssetId(AssetResource[] assetResources)
        {

            int affectedRow = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                string sql = $"INSERT INTO AssetResource VALUES ";
                for(int i=0; i< assetResources.Length; i++)
                {
                    sql += $"(UUID(), @FixedAssetId{i}, @ResourceId{i}, @Budget{i}, 'Đỗ Mạnh Hà', NOW(), NULL, NULL), ";

                    dynamicParameters.Add($"@FixedAssetId{i}", assetResources[i].FixedAssetId);
                    dynamicParameters.Add($"@ResourceId{i}", assetResources[i].ResourceId);
                    dynamicParameters.Add($"@Budget{i}", assetResources[i].Budget);
                }

                sql = sql.Substring(0, sql.Length - 2) + ";";

                affectedRow = _dbConnection.Execute(sql, param: dynamicParameters, transaction: transaction);
                transaction.Commit();
            }
            return affectedRow;
        }
        #endregion

        #region Method
        #endregion

    }
}
