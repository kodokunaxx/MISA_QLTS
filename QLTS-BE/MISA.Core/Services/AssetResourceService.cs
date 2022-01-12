using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Services
{
    /// <summary>
    /// Resource Service
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class AssetResourceService : BaseService<AssetResource>, IAssetResourceService
    {
        #region Declare
        IAssetResourceRepository _assetResourceRepository;
        #endregion

        #region Property

        #endregion
        #region Constructor
        public AssetResourceService(IAssetResourceRepository assetResourceRepository) : base(assetResourceRepository)
        {
            _assetResourceRepository = assetResourceRepository;
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
        public ServiceResult DeleteByFixedAssetId(string fixedAssetId)
        {
            ServiceResult serviceResult = new ServiceResult();
            var data = _assetResourceRepository.DeleteByFixedAssetId(fixedAssetId);
            serviceResult.MoreInfo = Properties.Resource.DELETE;
            serviceResult.SetSuccess(serviceResult, data);
            return serviceResult;
        }

        /// <summary>
        /// Xóa các bản ghi theo tài sản Id
        /// </summary>
        /// <param name="fixedAssetId">tài sản Id</param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetByFixedAssetId(Guid fixedAssetId)
        {
            ServiceResult serviceResult = new ServiceResult();
            var data = _assetResourceRepository.GetByFixedAssetId(fixedAssetId);
            serviceResult.MoreInfo = Properties.Resource.GET;
            serviceResult.SetSuccess(serviceResult, data);
            return serviceResult;
        }

        /// <summary>
        /// Thêm nhiều bản ghi 
        /// </summary>
        /// <param name="assetResources"></param>
        /// <returns>rowEffect</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult InsertByFixedAssetId(AssetResource[] assetResources)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.POST;
            var rowEffects = _assetResourceRepository.InsertByFixedAssetId(assetResources);

            if (rowEffects == assetResources.Length)
            {
                serviceResult.SetSuccess(serviceResult, rowEffects);
            }
            else
            {
                serviceResult.SetBadRequest(serviceResult);
                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_Info_Msg, ""));
            }
            return serviceResult;
        }
        #endregion
    }
}
