using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Services
{
    public class ReceiptService : BaseService<Receipt>, IReceiptService
    {
        #region Declare
        IReceiptRepository _receiptRepository;
        IFixedAssetService _fixedAssetService;
        #endregion

        #region Property

        #endregion
        #region Constructor
        public ReceiptService(IReceiptRepository receiptRepository, IFixedAssetService fixedAssetService) : base(receiptRepository)
        {
            _receiptRepository = receiptRepository;
            _fixedAssetService = fixedAssetService;
        }

        public ServiceResult GetFilter(string receiptCode, string note, int pageIndex, int pageSize)
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;
            var data = _receiptRepository.GetFilter(receiptCode, note, pageIndex, pageSize);
            serviceResult.SetSuccess(serviceResult, data);
            serviceResult.Total = _receiptRepository.GetFilterCount(receiptCode, note);

            return serviceResult;
        }

        public ServiceResult GetNewCode()
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var lastestCode = _receiptRepository.GetLastCode();
                if (lastestCode == null)
                {
                    serviceResult.SetSuccess(serviceResult, "GT00001");
                    return serviceResult;
                }

                int lastCharIndex = 0;
                for (int i = lastestCode.Length - 1; i >= 0; i--)
                {
                    if (lastestCode[i] < '0' || lastestCode[i] > '9')
                    {
                        lastCharIndex = i + 1;
                        break;
                    }
                }

                if (lastCharIndex == lastestCode.Length)
                {
                    serviceResult.SetSuccess(serviceResult, lastestCode + 0);
                }
                else
                {
                    string number = lastestCode.Substring(lastCharIndex);
                    int firstIndex = 0;

                    while (firstIndex < number.Length && number[firstIndex] == '0')
                    {
                        firstIndex++;
                    }

                    int currentNumberLength = number.Length - firstIndex;
                    if (((Int32.Parse(number) + 1) + "").Length > currentNumberLength && firstIndex > 0)
                    {
                        firstIndex--;
                    }

                    string data = lastestCode.Substring(0, lastCharIndex + firstIndex) + (Int32.Parse(number) + 1);
                    serviceResult.SetSuccess(serviceResult, data);
                }
            }
            catch (Exception e)
            {
                serviceResult.ResultCode = (int)CodeResult.NotFound;
                serviceResult.DevMessage.Add(e.Message);
            }
            return serviceResult;
        }


        public override ServiceResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;

            var data = _receiptRepository.GetById(entityId);
            if (data != null)
            {
                data.FixedAssets = (IEnumerable<FixedAsset>) _fixedAssetService.GetByReceiptId(entityId).Data;
                serviceResult.SetSuccess(serviceResult, data);
            }
            else
            {
                serviceResult.SetBadRequest(serviceResult);
                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorGet_Msg, entityId));
            }
            return serviceResult;
        }
        #endregion
    }
}
