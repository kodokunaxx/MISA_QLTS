using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Enumerations
{
    /// <summary>
    /// Mã trả về
    /// </summary>
    /// CreatedBy: hadm (27/8/2021)
    /// ModifiedBy: null
    public enum CodeResult
    {
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,
        /// <summary>
        /// Tạo tành công
        /// </summary>
        Created = 201,
        /// <summary>
        /// Không có nội dung
        /// </summary>
        NoContent = 204,
        /// <summary>
        /// Yêu cầu k hợp lệ
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// không được phép truy cập
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// Cấm
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// Không tìm thấy 
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Lỗi phía server
        /// </summary>
        InternalServerError = 500
    }
}
