using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AmisAPI.Controllers
{
    /// <summary>  
    /// Chứng từ Controller
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class ReceiptController : BaseController<Receipt>
    {
        #region Delclare
        IReceiptService _receiptService;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public ReceiptController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Filter paging chứng từ
        /// </summary>
        /// <param name="keyword">mã, nội dung</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("filter")]
        public IActionResult GetFilter(string keyword, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _receiptService.GetFilter(keyword, keyword, pageIndex, pageSize);
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Lấy mã chứng từ mới nhất
        /// </summary>
        /// <returns>new code</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("new-code")]
        public IActionResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _receiptService.GetNewCode();
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        #endregion
    }
}
