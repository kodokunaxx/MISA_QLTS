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
    /// EmployeeController
    /// </summary>
    public class FixedAssetController : BaseController<FixedAsset>
    {
        IFixedAssetService _fixedAssetService;
        public FixedAssetController(IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>danh sách bản ghi phân trang</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("paging")]
        public IActionResult GetAll([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {

            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _fixedAssetService.GetAll(pageIndex, pageSize);
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentId">Phòng ban Id</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản Id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>dang sách bản ghi theo filter</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("filter")]
        public IActionResult GetFilter(string keyword, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _fixedAssetService.GetFilter(keyword, keyword, departmentId, fixedAssetCategoryId, pageIndex, pageSize);
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }

        }

        /// <summary>
        /// Lấy mã TS mới
        /// </summary>
        /// <returns>new  code</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("new-code")]
        public IActionResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _fixedAssetService.GetNewCode();
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Xuất Excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>excel file</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("Export")]
        public IActionResult ExportExcel(string keyword, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize)
        {
            var stream = _fixedAssetService.ExportExcel(keyword, departmentId, fixedAssetCategoryId, pageIndex, pageSize);
            string excelName = $"Danh-sach-tai-san-{DateTime.Now.ToString("yyyy_MM_dd")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
