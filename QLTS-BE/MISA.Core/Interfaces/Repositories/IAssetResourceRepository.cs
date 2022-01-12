using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface Tài sản nguồn
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public interface IAssetResourceRepository : IBaseRepository<AssetResource>
    {
        #region Method
        /// <summary>
        /// Lấy danh sách tài sản nguồn theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<AssetResource> GetByFixedAssetId(Guid fixedAssetId);

        /// <summary>
        /// Xóa các bản ghi theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int DeleteByFixedAssetId(string fixedAssetId);

        /// <summary>
        /// Thêm nhiều bản ghi 
        /// </summary>
        /// <param name="assetResources"></param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int InsertByFixedAssetId(AssetResource[] assetResources);
        #endregion
    }
}
