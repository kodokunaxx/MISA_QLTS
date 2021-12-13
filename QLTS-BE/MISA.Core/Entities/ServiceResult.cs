﻿using MISA.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Lớp kết quả trả về
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class ServiceResult
    {
        #region Declare

        #endregion

        #region Property
        /// <summary>
        /// Mã kết quả 
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Message trả về cho dev
        /// </summary>
        public List<string> DevMessage { get; set; }

        /// <summary>
        /// Message trả về cho user
        /// </summary>
        public List<string> UserMessage { get; set; }

        /// <summary>
        /// Thông tin khác
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Data trả về cho Client
        /// </summary>
        public object Data { get; set; }

        public List<object> ValidateInfo { get; set; }

        /// <summary>
        /// Tổng số hàng
        /// </summary>
        public int Total { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult()
        {
            ResultCode = (int)CodeResult.Success;
            DevMessage = new List<string>();
            UserMessage = new List<string>();
            MoreInfo = null;
            Data = null;
            Total = -1;
            ValidateInfo = new List<object>();
        }
        #endregion

        #region Method
        /// <summary>
        /// Gán log thực hiện thành công
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <param name="data"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetSuccess(ServiceResult serviceResult, object data)
        {
            serviceResult.ResultCode = (int)CodeResult.Success;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Success_Msg);
            serviceResult.UserMessage.Add(Properties.Resource.User_Success_Msg);
            serviceResult.Data = data;
        }

        /// <summary>
        /// Gán log thực hiện thất bại
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetNoContent(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)CodeResult.NoContent;
            serviceResult.DevMessage.Add(Properties.Resource.Dev_Error_Msg);
            serviceResult.UserMessage.Add(Properties.Resource.User_Info_Msg);
        }

        /// <summary>
        /// Gán log lỗi máy chủ
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetInternalServerError(Exception ex)
        {
            ResultCode = (int)System.Net.HttpStatusCode.InternalServerError;
            DevMessage.Add(ex.Message);
            UserMessage.Add(Properties.Resource.User_Error_Msg);
        }

        /// <summary>
        /// Gán log lỗi thông tin
        /// </summary>
        /// <param name="serviceResult"></param>
        /// CreatedBy: hadm (18/8/2021)
        /// ModifiedBy: null
        public void SetBadRequest(ServiceResult serviceResult)
        {
            serviceResult.ResultCode = (int)CodeResult.BadRequest;
            serviceResult.UserMessage.Add(Properties.Resource.User_Info_Msg);
        }

        #endregion
    }
}
