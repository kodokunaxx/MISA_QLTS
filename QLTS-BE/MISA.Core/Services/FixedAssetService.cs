using MISA.Core.Entities;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq;
using System.Text;

namespace MISA.Core.Services
{
    /// <summary>
    /// Employee Service
    /// </summary>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        #region Declare
        IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Property

        #endregion
        #region Constructor
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
        }

        #endregion

        #region Method
        /// <summary>
        /// Lất tất cả bản ghi
        /// </summary>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>FixedAssets</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetAll(int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;

            var data = _fixedAssetRepository.GetAll(pageIndex, pageSize);
            serviceResult.SetSuccess(serviceResult, data);
            serviceResult.Total = _fixedAssetRepository.GetCount();


            return serviceResult;
        }


        /// <summary>
        /// Lấy bản ghi qua keyword
        /// </summary>
        /// <param name="fixedAssetCode">Mã tài sản</param>
        /// <param name="fixedAssetName">Tên tài sản</param>
        /// <param name="departmentId">Phòng ban Id</param>
        /// <param name="fixedAssetCategoryId">Loại tài sản Id</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>Employees</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetFilter(string fixedAssetCode, string fixedAssetName, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize)
        {

            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 20;
            var data = _fixedAssetRepository.GetFilter(fixedAssetCode, fixedAssetName, departmentId, fixedAssetCategoryId, pageIndex, pageSize);
            serviceResult.SetSuccess(serviceResult, data);
            serviceResult.Total = _fixedAssetRepository.GetFilterCount(fixedAssetCode, fixedAssetName, departmentId, fixedAssetCategoryId);

            return serviceResult;
        }

        /// <summary>
        /// Lấy mã NV mới
        /// </summary>
        /// <returns>Mã NV mới</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public ServiceResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var lastestCode = _fixedAssetRepository.GetLastCode();
                if (lastestCode == null)
                {
                    serviceResult.SetSuccess(serviceResult, "TS00001");
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

                if(lastCharIndex == lastestCode.Length)
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

        /// <summary>
        /// Export file Excel
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <param name="pageIndex">trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <returns>file</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public Stream ExportExcel(string keyword, Guid? departmentId, Guid? fixedAssetCategoryId, int pageIndex, int pageSize)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var res = _fixedAssetRepository.GetFilter(keyword, keyword, departmentId, fixedAssetCategoryId, pageIndex, pageSize);

            var list = res.ToList();
            var stream = new MemoryStream();
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH TÀI SẢN");

            // set độ rộng col
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(3).Width = 40;
            workSheet.Column(4).Width = 25;
            workSheet.Column(5).Width = 25;
            workSheet.Column(6).Width = 10;
            workSheet.Column(7).Width = 15;
            workSheet.Column(8).Width = 15;
            workSheet.Column(9).Width = 15;


            using (var range = workSheet.Cells["A1:I1"])
            {
                range.Merge = true;
                range.Value = "DANH SÁCH TÀI SẢN";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 16;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[3, 1].Value = "STT";
            workSheet.Cells[3, 2].Value = "Mã tài sản";
            workSheet.Cells[3, 3].Value = "Tên tài sản";
            workSheet.Cells[3, 4].Value = "Loại tài sản";
            workSheet.Cells[3, 5].Value = "Bộ phận sử dụng";
            workSheet.Cells[3, 6].Value = "Số lượng";
            workSheet.Cells[3, 7].Value = "Nguyên giá";
            workSheet.Cells[3, 8].Value = "HM/KH lũy kế";
            workSheet.Cells[3, 9].Value = "Giá trị còn lại";


            using (var range = workSheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                //range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var ts in list)
            {
                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = ts.FixedAssetCode;
                workSheet.Cells[i + 4, 3].Value = ts.FixedAssetName;
                workSheet.Cells[i + 4, 4].Value = ts.FixedAssetCategoryName;
                workSheet.Cells[i + 4, 5].Value = ts.DepartmentName;
                workSheet.Cells[i + 4, 6].Value = ts.Quantity;
                workSheet.Cells[i + 4, 7].Value = ts.Cost;
                workSheet.Cells[i + 4, 8].Value = ts.Cost*(decimal)ts.DepreciationRate/100;
                workSheet.Cells[i + 4, 9].Value = ts.Cost - ts.Cost * (decimal)ts.DepreciationRate / 100;

                workSheet.Cells[i + 3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 3, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 3, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                i++;
            }

            package.Save();
            stream.Position = 0;
            return package.Stream;
        }

        #endregion
    }
}
