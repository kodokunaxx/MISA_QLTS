using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Interface Employee Service
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public interface IFixedAssetService : IBaseService<FixedAsset>
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
        public ServiceResult GetAll(int pageIndex, int pageSize);
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
        public ServiceResult GetFilter(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize);

        /// <summary>
        /// Lấy mã TS mới
        /// </summary>
        /// <returns>Mã TS mới</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetNewCode();

        /// <summary>
        /// Export file Excel
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <param name="departmentId">Phòng ban Id</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản Id</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>file</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public Stream ExportExcel(string keyword, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize);
        #endregion
    }
}
