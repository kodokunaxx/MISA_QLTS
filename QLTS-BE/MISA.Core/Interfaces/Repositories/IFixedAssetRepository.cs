using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface FixedAsset
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        #region Method
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetAll(int pageIndex, int pageSize);


        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int GetCount();

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
        public IEnumerable<FixedAsset> GetFilter(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy mã TS mới nhất
        /// </summary>
        /// <returns>Mã TS mới nhất</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public string GetLastCode();

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
        public int GetFilterCount(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId);
        #endregion
    }
}
