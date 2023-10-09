using Azure;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Any;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using PRJ.DataAccess.Migrations;
using PRJ.Domain.Entities;
using System.Diagnostics.Metrics;
using System.IO;
using System.IO.Packaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers
{
    // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Workers")]

    public class WorkersController : ControllerBase
    {
        private readonly bus.Workers.WorkersManager _manager;
        private readonly bus.LookupSet.LookupSetManager _managerLookup;

        
        public WorkersController(bus.Workers.WorkersManager manager, bus.LookupSet.LookupSetManager managerLookup)
        {
            _manager = manager;
            _managerLookup = managerLookup;
        }
        #region Workers
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost("GetAllQuarter")]
        public async Task<IActionResult> GetAllQuarter()
        {
            return Ok(await _manager.GetAllQuarter());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await _manager.GetById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.Workers.DTOWorkers dto)
        {
            return Ok(await _manager.Add(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.Workers.DTOWorkers dto)
        {
            return Ok(await _manager.Update(dto));
        }
        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(dto.Workers.DTOUpdateStatusWorker dto)
        {
            return Ok(await _manager.UpdateStatus(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.Delete(Id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
        [HttpPost("GetAllFunctionalMassUpdate")]
        public async Task<IActionResult> GetAllFunctionalMassUpdate(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctionalMassUpdate(param));
        }
        
        [HttpPost("ExportExcel")] 
        public async Task<IActionResult> ExportExcel(string License, string selectedYear)
        {
            var stream = new MemoryStream();
            FileInfo newFile = new FileInfo("WorkerExcel.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo("WorkerExcel.xlsx");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory());
            var filePath = folderPath + "\\WorkerExcel.xlsx";
            
                var data = _manager.GetAllForExcel(License, selectedYear);
                var ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var DoseType = _managerLookup.GetLookupsByClassNameForExcel("DosimeterType");
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Worker List");
                    //Add the headers
                    worksheet.Cells[1, 1].Value = "Worker Id";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Birth Date";
                    worksheet.Cells[1, 4].Value = "Gender";
                    worksheet.Cells[1, 5].Value = "Job Position";
                    worksheet.Cells[1, 6].Value = "iqame Id";
                    worksheet.Cells[1, 7].Value = "Employment Status";
                    worksheet.Cells[1, 8].Value = "Q1";
                    worksheet.Cells[1, 9].Value = "Q2";
                    worksheet.Cells[1, 10].Value = "Q3";
                    worksheet.Cells[1, 11].Value = "Q4";
                    worksheet.Cells[1, 12].Value = "Dosimeter Type";
                    worksheet.Cells[1, 13].Value = "This Year";
                    
                    int i = 2;
                    foreach (var req in data.Result.Data)
                    {
                            worksheet.Cells[i, 1].Value = req.Id;
                            worksheet.Cells[i, 2].Value = req.WorkerNameAr;
                            worksheet.Cells[i, 3].Value = req.BirthDate.GetDateTimeFormats();
                            worksheet.Cells[i, 4].Value = req.GenderLookup.DisplayNameAr;
                            worksheet.Cells[i, 5].Value = req.JobPositionLookup.DisplayNameAr;
                            worksheet.Cells[i, 6].Value = req.NationalityId;
                            worksheet.Cells[i, 7].Value = req.LookupSetTerm.DisplayNameAr;
                            var dd = worksheet.Cells[i , 12].DataValidation.AddListDataValidation() as ExcelDataValidationList;  
                            dd.AllowBlank = true;
                            dd.PromptTitle = "Dosimeter Ttyp";
                    //dd.Formula.ExcelFormula = "ss";
                    foreach (var item in DoseType.Result.Data.LookupSetTerms)
                            {
                              dd.Formula.Values.Add(item.LookupSetTermId + "-" + item.DisplayNameEn);
                            }
                             worksheet.Cells[i, 12].Value = dd;
                          foreach (var dos in req.WorkersExposuresDoses)
                            {
                                int count = 7;
                                int q = int.Parse(dos.QuarterCode);
                                switch (q)
                                {
                                    case 1:
                                        worksheet.Cells[i, count + 1].Value = dos.DeepDose; break;
                                    case 2:
                                        worksheet.Cells[i, count + 2].Value = dos.DeepDose; break;
                                    case 3:
                                        worksheet.Cells[i, count + 3].Value = dos.DeepDose; break;
                                    case 4:
                                        worksheet.Cells[i, count + 4].Value = dos.DeepDose; break;
                                }
                            
                            }
                        i++;
                    }
                    package.Workbook.Properties.Title = "Worker List";
                    package.Workbook.Properties.Author = "NRRC";
                    package.Workbook.Properties.Comments = "This list worker For Import reading Quarter";
                    package.Save();
                }
                //  return Ok(downloadUrl);
            
            string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, "WorkerExcel.xlsx");

            stream.Position = 0;

            return File(stream, _manager.ContectType(filePath), "WorkerExcel.xlsx");


         //   return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);  
          //  return newFile;
              

            }
       
        [HttpPost("import")]        
        public async Task<IActionResult> Import([FromForm] dto.Workers.DTOFileInfo fileInfo , CancellationToken cancellationToken)
        {
            if (fileInfo.formFile == null || fileInfo.formFile.Length <= 0)
            {
                return Problem("formfile is empty");
            }
            if (!Path.GetExtension(fileInfo.formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return Problem("Not Support file extension");
            }

            var list = new List<dto.Workers.DTOMassUpdate>();

            using (var stream = new MemoryStream())
            {
                await fileInfo.formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var index = 7 + fileInfo.quarter;

                        list.Add( new dto.Workers.DTOMassUpdate
                        {
                            WorkerId = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                            Quartervalue = worksheet.Cells[row, index].Value != null ? decimal.Parse(worksheet.Cells[row, index].Value.ToString().Trim()) : 0,
                            DosimeterType = worksheet.Cells[row, 12].Value != null ? worksheet.Cells[row, 12].Value.ToString().Trim() : ""
                        });
                    }
                }
            }

            return Ok(await _manager.massUpdate(list, fileInfo));
          
        }
        #endregion
    }
    
}
