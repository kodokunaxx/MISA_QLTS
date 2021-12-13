using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    /// <summary>
    /// Base Service
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class BaseService<T> : IBaseService<T>
    {
        #region Declare
        IBaseRepository<T> _baseRepository;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.DELETE;

            var rowEffects = _baseRepository.Delete(entityId);
            if (rowEffects > 0)
            {
                serviceResult.SetSuccess(serviceResult, rowEffects);
            }
            else
            {
                serviceResult.SetBadRequest(serviceResult);
                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorDelete_Msg, entityId));
            }
            return serviceResult;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            var data = _baseRepository.GetAll();
            serviceResult.SetSuccess(serviceResult, data);

            return serviceResult;
        }

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Entity</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;

            var data = _baseRepository.GetById(entityId);
            if (data != null)
            {
                serviceResult.SetSuccess(serviceResult, data);
            }
            else
            {
                serviceResult.SetBadRequest(serviceResult);
                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorGet_Msg, entityId));
            }
            return serviceResult;
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult Insert(T entity)
        {
            ServiceResult serviceResult = CheckValidate(entity, null);
            setValueEntity(entity);
            serviceResult.MoreInfo = Properties.Resource.POST;

            if (serviceResult.ResultCode == (int)CodeResult.Success)
            {
                var rowEffects = _baseRepository.Insert(entity);

                if (rowEffects > 0)
                {
                    serviceResult.SetSuccess(serviceResult, rowEffects);
                    serviceResult.ResultCode = (int)Enumerations.CodeResult.Created;
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add(Properties.Resource.Dev_Info_Msg);
                }
            }


            return serviceResult;
        }

        /// <summary>
        /// Sửa một bản ghi theo Id
        /// </summary>
        /// <param name="entity">Id của entity</param>
        /// <param name="entityId">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult Update(T entity, Guid entityId)
        {
            ServiceResult serviceResult = CheckValidate(entity, entityId.ToString());
            setValueEntity(entity);
            if (serviceResult.ResultCode != (int)CodeResult.Success)
            {
                return serviceResult;
            }
            serviceResult.MoreInfo = Properties.Resource.PUT;
            T isExistEntity = _baseRepository.GetById(entityId);
            if (isExistEntity == null)
            {
                serviceResult.SetBadRequest(serviceResult);
                serviceResult.DevMessage.Add(string.Format(Properties.Resource.Dev_ErrorGet_Msg, entityId));
            }
            else
            {
                serviceResult.SetSuccess(serviceResult, _baseRepository.Update(entity, entityId));
            }

            return serviceResult;
        }

        public async Task<ServiceResult> DeleteMultiAsync(List<string> listID)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.DELETE;
            var rowEffects = await _baseRepository.DeleteMulti(listID);

            if (rowEffects == listID.Count)
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

        /// <summary>
        /// Gán service result với từng loại validate
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="id">idEntity</param>
        /// <returns>service result</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult CheckValidate(T entity, string id)
        {
            ServiceResult serviceResult = new ServiceResult();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var column = prop.Name;
                object value = prop.GetValue(entity);


                if (prop.IsDefined(typeof(Required), true) && CheckEmpty(value))
                {
                    serviceResult.ResultCode = (int)CodeResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Required_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Required_Msg, column));
                }
                if (prop.IsDefined(typeof(Email), true) && !CheckEmail((string)value))
                {
                    serviceResult.ResultCode = (int)CodeResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Email_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Email_Msg, column));
                    serviceResult.ValidateInfo.Add(new
                    {
                        column = column,
                        state = "email"
                    });
                }
                if (prop.IsDefined(typeof(Duplication), true) && CheckDuplicate(column, (string)value, id))
                {
                    serviceResult.ResultCode = (int)CodeResult.BadRequest;
                    serviceResult.DevMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, column));
                    serviceResult.UserMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, column));
                    serviceResult.ValidateInfo.Add(new
                    {
                        column = column,
                        state = "duplicate"
                    });
                }
            }
            return serviceResult;
        }


        /// <summary>
        /// Check null
        /// </summary>
        /// <param name="value">value</param>
        /// <returns> true: null / false: not null</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public bool CheckEmpty(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check trùng
        /// </summary>
        /// <param name="column">tên cột</param>
        /// <param name="value">giá trị cột</param>
        /// <param name="id">id entity</param>
        /// <returns>true: trùng / false: không trùng</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public bool CheckDuplicate(string column, string value, string id)
        {
            var isDuplicate = _baseRepository.GetByProperty(column, value, id);
            if (isDuplicate != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check email
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>true: là email / false: không phải email</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public bool CheckEmail(string value)
        {
            //if( value == null)
            //{
            //    return true;
            //}
            string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(value);
            if (match.Success || value.Trim() == "")
            {
                return true;
            }
            return false;
        }
        public void setValueEntity(T entity)
        {
            PropertyInfo p;
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                var column = prop.Name;
                object value = prop.GetValue(entity);
                if (column == "Gender")
                {
                    p = entity.GetType().GetProperty("GenderName");
                    p.SetValue(entity, changeGender((int)value));
                }
            }
        }

        public string changeGender(int value)
        {
            switch (value)
            {
                case 0:
                    return "Nam";
                    break;
                case 1:
                    return "Nữ";
                    break;
                case 2:
                    return "Không xác định";
                    break;
                default:
                    break;
            }
            return "";
        }
        #endregion

    }
}
