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
    /// Tài sản - nguồn Controller
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class AssetResourceController : BaseController<AssetResource>
    {
        #region Declare
        IAssetResourceService _assetResourceService;
        #endregion

        #region Property

        #endregion
        #region Constructor
        public AssetResourceController(IAssetResourceService assetResourceService) : base(assetResourceService)
        {
            _assetResourceService = assetResourceService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách tài sản nguồn theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>list</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("FixedAsset/{fixedAssetId}")]
        public IActionResult GetByFixedAssetId(Guid fixedAssetId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _assetResourceService.GetByFixedAssetId(fixedAssetId);
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Xóa các bản ghi theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpDelete("FixedAsset/{fixedAssetId}")]
        public IActionResult DeleteByFixedAssetId(string fixedAssetId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _assetResourceService.DeleteByFixedAssetId(fixedAssetId);
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        /// <summary>
        /// Thêm nhiều bản ghi 
        /// </summary>
        /// <param name="assetResources"></param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpPost("FixedAsset/insert-multi")]
        public IActionResult InsertByFixedAssetId(AssetResource[] assetResources)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _assetResourceService.InsertByFixedAssetId(assetResources);
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
