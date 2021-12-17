using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AmisAPI.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Declare
        IBaseService<T> _baseService;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        // GET: api/v1/<BaseController>
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet]
        public IActionResult Get()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.GetAll();
                return Ok(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        // GET: api/v1/<BaseController>/5
        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>bản ghi</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.GetById(entityId);
                if(serviceResult.ResultCode == (int)CodeResult.Success)
                {
                    return Ok(serviceResult);
                }
                return BadRequest(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        // POST: api/v1/<BaseController>
        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>result</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpPost]
        public IActionResult Insert([FromBody] T entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.Insert(entity);
                if (serviceResult.ResultCode == (int) CodeResult.Created)
                {
                    return base.StatusCode(201, serviceResult);
                }
                return BadRequest(serviceResult);

            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        // PUT: api/v1/<BaseController>/5
        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>result</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpPut("{entityId}")]
        public IActionResult Update([FromBody] T entity, Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.Update(entity, entityId);
                if (serviceResult.ResultCode == (int) CodeResult.Success)
                {
                    return base.Ok(serviceResult);
                }
                return BadRequest(serviceResult);

            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }

        }

        // DELETE: api/v1/<BaseController>/5
        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>result</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.Delete(entityId);
                if (serviceResult.ResultCode == (int) CodeResult.Success)
                {
                    return base.Ok(serviceResult);
                }
                return BadRequest(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500, serviceResult);
            }
        }

        [HttpDelete("delete-multi")]
        public async Task<IActionResult> DeleteMulti(string[] listId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = await _baseService.DeleteMultiAsync(listId);
                if(serviceResult.ResultCode == (int) CodeResult.Success)
                {
                    return base.Ok(serviceResult);
                }
                return BadRequest(serviceResult);
            }
            catch (Exception e)
            {
                serviceResult.SetInternalServerError(e);
                return StatusCode(500,serviceResult);
            }
        }

        #endregion
    }
}
