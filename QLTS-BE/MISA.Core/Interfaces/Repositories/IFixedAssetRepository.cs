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

        /// <summary>
        /// Lấy danh sách bản ghi theo chứng từ Id
        /// </summary>
        /// <param name="receiptId">chứng từ id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetByReceiptId(Guid receiptId);

        /// <summary>
        /// Cập nhật các bản ghi dựa theo chứng từ Id và tài sản id
        /// </summary>
        /// <param name="receiptId">chứng từ Id</param>
        /// <param name="listId">danh sách tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public int UpdateByReceiptId(Guid receiptId, string[] listId);

        /// <summary>
        /// Filter paging tài sản không có chứng từ
        /// </summary>
        /// <param name="fixedAssetCode">mã</param>
        /// <param name="fixedAssetName">tên</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public IEnumerable<FixedAsset> GetByReceiptIdNull(string fixedAssetCode, string fixedAssetName);
        #endregion
    }
}
