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

        /// <summary>
        /// Lấy danh sách bản ghi theo chứng từ Id
        /// </summary>
        /// <param name="receiptId">chứng từ id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetByReceiptId(Guid receiptId);

        /// <summary>
        /// Cập nhật các bản ghi dựa theo chứng từ Id và tài sản id
        /// </summary>
        /// <param name="receiptId">chứng từ Id</param>
        /// <param name="listId">danh sách tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult UpdateByReceiptId(Guid receiptId, string[] listId);

        /// <summary>
        /// Filter paging tài sản không có chứng từ
        /// </summary>
        /// <param name="fixedAssetCode">mã</param>
        /// <param name="fixedAssetName">tên</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetByReceiptIdNull(string fixedAssetCode, string fixedAssetName);
        #endregion
    }
}
