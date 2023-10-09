using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;
using enc = PRJ.GlobalComponents.Encryption;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using PRJ.Application.Warppers;
using System.Linq;
using PRJ.Domain.Entities;
using PRJ.Application.DTOs.Workers;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.Net.Mime;
using Microsoft.AspNetCore.StaticFiles;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using PRJ.Application.Warppers.models;

namespace PRJ.Business.Workers
{
    public class WorkersManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public WorkersManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        #region Get Workers
       
        public string ContectType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public async Task<wap.Response<bool>> Add(dto.Workers.DTOWorkers Workers)
        {
                var _Natio = enc.EncryptQueryURL.Decrypt(Workers.Nationality);
                var _status = enc.EncryptQueryURL.Decrypt(Workers.Status);
                var _FacilityId = enc.EncryptQueryURL.Decrypt(Workers.FacilityId);
                var _Gender = enc.EncryptQueryURL.Decrypt(Workers.Gender);
                var _Jop = enc.EncryptQueryURL.Decrypt(Workers.JobPosition);
                Workers.CurrentLicense = enc.EncryptQueryURL.Decrypt(Workers.CurrentLicense);
                Workers.CurrentPractise = enc.EncryptQueryURL.Decrypt(Workers.CurrentPractise);

                Workers.JobPosition = _Jop; 
                Workers.Nationality = _Natio;
                Workers.Status = _status;
                Workers.FacilityId = _FacilityId;
                Workers.Gender = _Gender;

                var _afterMaps = _mapper.Map<ent.Workers>(Workers);
                return new wap.Response<bool>()
                {
                    Data = await _manager.Workers.AddAsync(_afterMaps)
                };
            
        }
        
        public async Task<wap.Response<bool>> UpdateStatus(dto.Workers.DTOUpdateStatusWorker worker)
        {

            var obj = await _manager.Workers.GetEncryptByIdAsync(worker.Id);
            if (obj != null)
            {
                obj.Status = int.Parse(enc.EncryptQueryURL.Decrypt(worker.Status));
                _manager.Workers.Update(obj);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            }
            else
            {
                return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
            }

        }

        public async Task<wap.Response<List<WorkerIncludeProblem>>> massUpdate(List<dto.Workers.DTOMassUpdate> list, dto.Workers.DTOFileInfo fileInfo)
        {
            var counter = 0;
            List<dto.Workers.WorkerIncludeProblem> listWorkerIncludeProblem = new List<WorkerIncludeProblem>();

            foreach (var worker in list)
            {
                var obj = _manager.Workers.GetById(worker.WorkerId);
                var count = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == worker.WorkerId).Count();
                var x = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == worker.WorkerId && _.QuarterCode == fileInfo.quarter.ToString() && _.FiscalYear == int.Parse(fileInfo.year)).OrderBy(_ => _.LineNum).FirstOrDefault();
                ent.WorkersExposuresDoses doseObj = new ent.WorkersExposuresDoses();
               
                    doseObj.DeepDose = worker.Quartervalue;
                    doseObj.Id = worker.WorkerId;
                    doseObj.FiscalYear = int.Parse(fileInfo.year);
                    doseObj.QuarterCode = fileInfo.quarter.ToString();
                    var str = "";
                    // Copy character by character into array 
                    if(worker.DosimeterType == "OfficeOpenXml.DataValidation.ExcelDataValidationList")
                    {
                    var obj1 = new WorkerIncludeProblem();
                    obj1.userId = doseObj.Id;
                    obj1.problem = " Enter your DosimeterType please ";
                    listWorkerIncludeProblem.Add(obj1);
                    }
                    else
                    {
                        for (int i = 0; i < worker.DosimeterType.Length; i++)
                        {
                            if (worker.DosimeterType[i] != '-')
                            {
                                str += worker.DosimeterType[i];
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                    doseObj.DosimeterType = int.Parse(str);
                    if (x == null)
                    {
                        doseObj.LineNum = count + 1;
                        _manager.WorkersExposuresDoses.Add(doseObj);
                         await _manager.CompleteAsync();
                    }
                    else if(x.DeepDose != doseObj.DeepDose){
                        var obj1 = new WorkerIncludeProblem();
                        obj1.userId = doseObj.Id;
                        obj1.problem = "This user has a previous reading recorded in the same year and quarter";
                        listWorkerIncludeProblem.Add(obj1);
/*                        x.DeepDose = doseObj.DeepDose;
                        _manager.WorkersExposuresDoses.Update(x);*/
                    }
                }

            }
            return new wap.Response<List<WorkerIncludeProblem>>() { Data = listWorkerIncludeProblem };
        }
        public async Task<wap.Response<bool>> Update(dto.Workers.DTOWorkers worker)
        {
           
                var obj = await _manager.Workers.GetEncryptByIdAsync(worker.Id);
                var FacilityId = await _manager.FacilityProfile.GetEncryptByIdAsync(worker.FacilityId);
                if (obj != null)
                {
                    obj.WorkerNameEn = worker.WorkerNameEn;
                    obj.WorkerNameAr = worker.WorkerNameAr;
                    obj.Status = int.Parse(enc.EncryptQueryURL.Decrypt(worker.Status));
                    obj.BirthDate = worker.BirthDate;
                    obj.JobPosition = int.Parse(enc.EncryptQueryURL.Decrypt(worker.JobPosition));
                    obj.PassportNo = worker.PassportNo;
                    obj.MobileNo = worker.MobileNo;
                    obj.NationalityId = worker.NationalityId;
                    obj.Nationality = int.Parse(enc.EncryptQueryURL.Decrypt(worker.Nationality));
                    obj.FacilityId = int.Parse(enc.EncryptQueryURL.Decrypt(worker.FacilityId));
                    obj.Gender = int.Parse(enc.EncryptQueryURL.Decrypt(worker.Gender));
                    _manager.Workers.Update(obj);
                    await _manager.CompleteAsync();

                    return new wap.Response<bool>() { Data = true };
                }
                else
                {
                    return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
                }

           
        }
        public async Task<wap.Response<bool>> Delete(string Id)
        {
            
                var obj = await _manager.Workers.GetEncryptByIdAsync(Id);

                if (obj != null)
                {
                    await _manager.Workers.DeleteAsync(obj);
                    await _manager.CompleteAsync();
                    return new wap.Response<bool>() { Data = true };
                }
                else
                {
                    return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
                }
           
        }
        public async Task<wap.Response<List<dto.Workers.DTOWorkers>>> GetAll()
        {
            
                var x = await _manager.Workers.GetByQuery(_ => _.Id != null).Include(_ => _.JobPositionLookup).Include(_ => _.WorkersExposuresDoses).Include(_ => _.LookupSetTerm).Include(_ => _.FacilityProfile).OrderBy(_ => _.Id).ToListAsync();
                return new wap.Response<List<dto.Workers.DTOWorkers>>()
                {
                    Data = _mapper.Map<List<dto.Workers.DTOWorkers>>(x)
                };
           
        }
        public async Task<wap.Response<List<ent.Workers>>> GetAllForExcel(string currentLicense,string selectedYear)
        {
            var License = int.Parse(enc.EncryptQueryURL.Decrypt(currentLicense));

            var x = _manager.Workers.GetByQuery(_ => _.Id != null).Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                     .Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                        .Where(_ => _.CurrentLicense == License)
                                        .Select(m => new ent.Workers
                                        {
                                            Id = m.Id,
                                            WorkerNameAr = m.WorkerNameAr,
                                            WorkerNameEn = m.WorkerNameEn,
                                            Gender = m.Gender,
                                            BirthDate = m.BirthDate,
                                            JobPosition = m.JobPosition,
                                            LookupSetTerm = m.LookupSetTerm,
                                            Status = m.Status,
                                            NationalityId = m.NationalityId,
                                            GenderLookup = m.GenderLookup,
                                            FacilityProfile = m.FacilityProfile,
                                            JobPositionLookup = m.JobPositionLookup,
                                            MobileNo = m.MobileNo,
                                            WorkersExposuresDoses = m.WorkersExposuresDoses.Where(_ => _.FiscalYear == int.Parse(selectedYear)).OrderBy(_ => _.QuarterCode).ToList(),
                                            LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                        }).ToList();


            return new wap.Response<List<ent.Workers>>()
            {
                Data = x
            };

        }
        public async Task<wap.Response<List<ent.DssQuarterSetup>>> GetAllQuarter()
        {
            return new wap.Response<List<ent.DssQuarterSetup>>()
            {
                Data = _mapper.Map<List<ent.DssQuarterSetup>>(await _manager.DssQuarterSetup.GetAllAsync())
            }
        }

        public async Task<wap.Response<dto.Workers.DTOWorkers>> GetById(string Id)
        {
            
            var _id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));
            var data = _manager.Workers.GetByQuery(_ => _.Id == _id).Include(_ => _.LookupSetTerm).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.BasCountries).FirstOrDefault();
            return new wap.Response<dto.Workers.DTOWorkers>()
             {
                 Data = _mapper.Map<dto.Workers.DTOWorkers>(data)
             };
            
        }

      
        public async Task<wap.PagingResponse<List<dto.Workers.DTOWorkers>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.Workers>>()
            {
                Draw = param.Draw
            };

            IQueryable<ent.Workers> dataList = _manager.Workers.GetAll();

            // Check if search is on specific column
            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            // General Search
            IQueryable<ent.Workers> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.WorkerNameAr.Contains(param.Search.Value) ||
                    r.WorkerNameEn.Contains(param.Search.Value) ||
                    r.GenderLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.GenderLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameAr.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameEn.Contains(param.Search.Value) ||
                    r.NationalityId.Contains(param.Search.Value) ||
                    r.JobPositionLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.JobPositionLookup.DisplayNameEn.Contains(param.Search.Value));

            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "facilityProfile":
                                query = dataList.Where(u => u.FacilityProfile.FacilityNameEn.Contains(search.Search.Value) || u.FacilityProfile.FacilityNameEn.Contains(search.Search.Value));
                                break;
                            case "workerNameAr":
                                query = dataList.Where(u => u.WorkerNameAr.Contains(search.Search.Value));
                                break;
                            case "workerNameEn":
                                query = dataList.Where(u => u.WorkerNameEn.Contains(search.Search.Value));
                                break;
                            case "jobPosition":
                                query = dataList.Where(u => u.JobPositionLookup.DisplayNameAr.Contains(search.Search.Value) && u.JobPositionLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "basCountries":
                                query = dataList.Where(u => u.BasCountries.CountryNameAr.Contains(search.Search.Value) || u.BasCountries.CountryNameEn.Contains(search.Search.Value));
                                break;
                            case "gender":
                                query = dataList.Where(u => u.GenderLookup.DisplayNameAr.Contains(search.Search.Value) || u.GenderLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "lastWorkersExposuresDose":
                                query = dataList.Where(u => u.WorkersExposuresDoses.Any(_ => _.DosimeterTypeList.DisplayNameAr.Contains(search.Search.Value) || _.DosimeterTypeList.DisplayNameEn.Contains(search.Search.Value)));
                                break;
                                
                        }
                    }
                }
            }
            else
            {
                query = dataList;
            }

            var colOrder = param.Order[0];

            switch (colOrder.Column)
            {
                case 0:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityProfile.FacilityId) : query.OrderByDescending(r => r.FacilityProfile.FacilityId);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.WorkerNameAr) : query.OrderByDescending(r => r.WorkerNameAr);
                    break;
                case 2:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NationalityId) : query.OrderByDescending(r => r.NationalityId);
                    break;
                case 3:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BasCountries.CountryNameAr) : query.OrderByDescending(r => r.BasCountries.CountryNameAr);
                    break;
                case 4:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BasCountries.CountryNameAr) : query.OrderByDescending(r => r.BasCountries.CountryNameAr);
                    break;
                case 5:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BasCountries.CountryNameAr) : query.OrderByDescending(r => r.BasCountries.CountryNameAr);
                    break;
                case 6:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BasCountries.CountryNameAr) : query.OrderByDescending(r => r.BasCountries.CountryNameAr);
                    break;
                case 7:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BasCountries.CountryNameAr) : query.OrderByDescending(r => r.BasCountries.CountryNameAr);
                    break;
            }

            if (param.extra != null)
            { 
                if(param.extra2 != null)
                {
                    var x = int.Parse(enc.EncryptQueryURL.Decrypt(param.extra));
                    var year = int.Parse(param.extra2);
                    // response.Data = query.Where(_ => _.CurrentLicense == x).Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm).Include(x => x.WorkersExposuresDoses.OrderByDescending(_ => _.LineNum).FirstOrDefault()).OrderByDescending(_ => _.CreatedOn).ToList();
                    response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                        .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                         .Where(_ => _.CurrentLicense == x)
                                         .Select(m => new ent.Workers
                                         {
                                             Id = m.Id,
                                             WorkerNameAr = m.WorkerNameAr,
                                             WorkerNameEn = m.WorkerNameEn,
                                             Gender = m.Gender,
                                             BirthDate = m.BirthDate,
                                             JobPosition = m.JobPosition,
                                             LookupSetTerm = m.LookupSetTerm,
                                             Status = m.Status, 
                                             NationalityId = m.NationalityId,
                                             GenderLookup = m.GenderLookup,
                                             FacilityProfile = m.FacilityProfile,
                                             JobPositionLookup = m.JobPositionLookup,
                                             MobileNo = m.MobileNo,
                                             WorkersExposuresDoses = m.WorkersExposuresDoses.Where(_ => _.FiscalYear == year).OrderBy(_ => _.QuarterCode).ToList(),
                                             LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                         }).ToList();
                }
                else
                {
                    var x = int.Parse(enc.EncryptQueryURL.Decrypt(param.extra));
                    // response.Data = query.Where(_ => _.CurrentLicense == x).Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm).Include(x => x.WorkersExposuresDoses.OrderByDescending(_ => _.LineNum).FirstOrDefault()).OrderByDescending(_ => _.CreatedOn).ToList();
                    response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                        .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                         .Where(_ => _.CurrentLicense == x)
                                         .Select(m => new ent.Workers
                                         {
                                             Id = m.Id,
                                             WorkerNameAr = m.WorkerNameAr,
                                             WorkerNameEn = m.WorkerNameEn,
                                             Gender = m.Gender,
                                             BirthDate = m.BirthDate,
                                             JobPosition = m.JobPosition,
                                             LookupSetTerm = m.LookupSetTerm,
                                             Status = m.Status,
                                             NationalityId = m.NationalityId,
                                             GenderLookup = m.GenderLookup,
                                             FacilityProfile = m.FacilityProfile,
                                             JobPositionLookup = m.JobPositionLookup,
                                             MobileNo = m.MobileNo,
                                             PassportNo = m.PassportNo,
                                             Nationality = m.Nationality,
                                             LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                         }).ToList();
                }  
            }
            else
            {
                response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                                  .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                                   .Select(m => new ent.Workers
                                                   {
                                                       Id = m.Id,
                                                       WorkerNameAr = m.WorkerNameAr,
                                                       WorkerNameEn = m.WorkerNameEn,
                                                       Gender = m.Gender,
                                                       NationalityId = m.NationalityId,
                                                       GenderLookup = m.GenderLookup,
                                                       FacilityProfile = m.FacilityProfile,
                                                       JobPositionLookup = m.JobPositionLookup,
                                                       LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                                   }).ToList();
            }
           
                var Data = _mapper.Map<List<dto.Workers.DTOWorkers>>(response.Data);
                var recordsTotal = Data.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

            return new PagingResponse<List<dto.Workers.DTOWorkers>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.Workers.DTOWorkers>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
            
        }


        public async Task<wap.PagingResponse<List<dto.Workers.DTOGetMassUpdate>>> GetAllFunctionalMassUpdate(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.Workers>>()
            {
                Draw = param.Draw
            };
            var year = int.Parse(param.extra2);

            IQueryable<ent.Workers> dataList = _manager.Workers.GetAll();

            // Check if search is on specific column
            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            // General Search
            IQueryable<ent.Workers> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.WorkerNameAr.Contains(param.Search.Value) ||
                    r.WorkerNameEn.Contains(param.Search.Value) ||
                    r.GenderLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.GenderLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameAr.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameEn.Contains(param.Search.Value) ||
                    r.NationalityId.Contains(param.Search.Value) ||
                    r.JobPositionLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.JobPositionLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.DeepDose.ToString().Contains(param.Search.Value)));

            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "nationalityId":
                                query = dataList.Where(u => u.NationalityId.Contains(search.Search.Value));
                                break;
                            case "birthDate":
                                query = dataList.Where(u => u.BirthDate.ToString().Contains(search.Search.Value));
                                break;
                            case "workerNameAr":
                            case "workerNameEn":
                                query = dataList.Where(u => u.WorkerNameAr.Contains(search.Search.Value) || u.WorkerNameEn.Contains(search.Search.Value));
                                break;
                            case "statusAr":
                            case "statusEn":
                                query = dataList.Where(u => u.LookupSetTerm.DisplayNameAr.Contains(search.Search.Value) || u.LookupSetTerm.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "genderEn":
                            case "genderAr":
                                query = dataList.Where(u => u.GenderLookup.DisplayNameAr.Contains(search.Search.Value) || u.GenderLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "jobPositionAr":
                            case "jobPositionEn":
                                query = dataList.Where(u => u.JobPositionLookup.DisplayNameAr.Contains(search.Search.Value) || u.JobPositionLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "q1Value":
                                query = dataList.Where(u => u.WorkersExposuresDoses.Any(_ => _.QuarterCode == "1" && _.FiscalYear == year && _.DeepDose.ToString().Contains(search.Search.Value)));
                                break;
                            case "q2Value":
                                query = dataList.Where(u => u.WorkersExposuresDoses.Any(_ => _.QuarterCode == "2" && _.FiscalYear == year && _.DeepDose.ToString().Contains(search.Search.Value)));
                                break;
                            case "q3Value":
                                query = dataList.Where(u => u.WorkersExposuresDoses.Any(_ => _.QuarterCode == "3" && _.FiscalYear == year && _.DeepDose.ToString().Contains(search.Search.Value)));
                                break;
                            case "q4Value":
                                query = dataList.Where(u => u.WorkersExposuresDoses.Any(_ => _.QuarterCode == "4" && _.FiscalYear == year && _.DeepDose.ToString().Contains(search.Search.Value)));
                                break;
                        }
                    }
                }
            }
            else
            {
                query = dataList;
            }

            var colOrder = param.Order[0];
            var lang = param.lang == 1 ? "en" : "ar";
            switch (colOrder.Column)
            {
                case 0:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.Id) : query.OrderByDescending(r => r.Id);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => lang=="en" ? r.WorkerNameEn : r.WorkerNameAr) : query.OrderByDescending(r => lang == "en" ? r.WorkerNameEn : r.WorkerNameAr);
                    break;
                case 2:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NationalityId) : query.OrderByDescending(r => r.NationalityId);
                    break;
                case 3:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => lang == "en" ? r.GenderLookup.DisplayNameEn : r.GenderLookup.DisplayNameAr) : query.OrderByDescending(r => lang == "en" ? r.GenderLookup.DisplayNameEn : r.GenderLookup.DisplayNameAr);
                    break;
                case 4:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.BirthDate) : query.OrderByDescending(r => r.BirthDate);
                    break;
                case 5:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => lang == "en" ? r.JobPositionLookup.DisplayNameEn : r.JobPositionLookup.DisplayNameAr) : query.OrderByDescending(r => lang == "en" ? r.JobPositionLookup.DisplayNameEn : r.JobPositionLookup.DisplayNameAr);
                    break;
                case 6:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => lang == "en" ? r.LookupSetTerm.DisplayNameEn:r.LookupSetTerm.DisplayNameAr) : query.OrderByDescending(r => lang == "en" ? r.LookupSetTerm.DisplayNameEn : r.LookupSetTerm.DisplayNameAr);
                    break;
                case 7:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "1")) : query.OrderByDescending(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "1"));
                    break;
                case 8:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "2")) : query.OrderByDescending(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "1"));
                    break;
                case 9:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "3")) : query.OrderByDescending(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "1"));
                    break;
                case 10:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "4")) : query.OrderByDescending(r => r.WorkersExposuresDoses.Any(_ => _.FiscalYear == year && _.QuarterCode == "1"));
                    break;

            }

            if (param.extra != null)
            {
                if (param.extra2 != null)
                {
                    var x = int.Parse(enc.EncryptQueryURL.Decrypt(param.extra));
                    var years = int.Parse(param.extra2);
                    response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                        .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                         .Where(_ => _.CurrentLicense == x)
                                         .Select(m => new ent.Workers
                                         {
                                             Id = m.Id,
                                             WorkerNameAr = m.WorkerNameAr,
                                             WorkerNameEn = m.WorkerNameEn,
                                             Gender = m.Gender,
                                             BirthDate = m.BirthDate,
                                             JobPosition = m.JobPosition,
                                             LookupSetTerm = m.LookupSetTerm,
                                             Status = m.Status,
                                             NationalityId = m.NationalityId,
                                             GenderLookup = m.GenderLookup,
                                             FacilityProfile = m.FacilityProfile,
                                             JobPositionLookup = m.JobPositionLookup,
                                             MobileNo = m.MobileNo,

                                             WorkersExposuresDoses = m.WorkersExposuresDoses.Where(_ => _.FiscalYear == year).OrderBy(_ => _.QuarterCode).ToList(),
                                             LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                         }).ToList();
                }
                else
                {
                    var x = int.Parse(enc.EncryptQueryURL.Decrypt(param.extra));
                    // response.Data = query.Where(_ => _.CurrentLicense == x).Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm).Include(x => x.WorkersExposuresDoses.OrderByDescending(_ => _.LineNum).FirstOrDefault()).OrderByDescending(_ => _.CreatedOn).ToList();
                    response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                        .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                         .Where(_ => _.CurrentLicense == x)
                                         .Select(m => new ent.Workers
                                         {
                                             Id = m.Id,
                                             WorkerNameAr = m.WorkerNameAr,
                                             WorkerNameEn = m.WorkerNameEn,
                                             Gender = m.Gender,
                                             NationalityId = m.NationalityId,
                                             GenderLookup = m.GenderLookup,
                                             FacilityProfile = m.FacilityProfile,
                                             JobPositionLookup = m.JobPositionLookup,
                                             LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                         }).ToList();
                }
            }
            else
            {
                response.Data = query.Include(m => m.WorkersExposuresDoses).ThenInclude(_ => _.DosimeterTypeList)
                                  .Skip(param.Start).Take(param.Length).Include(_ => _.FacilityProfile).Include(_ => _.GenderLookup).Include(_ => _.JobPositionLookup).Include(_ => _.LookupSetTerm)
                                                   .Select(m => new ent.Workers
                                                   {
                                                       Id = m.Id,
                                                       WorkerNameAr = m.WorkerNameAr,
                                                       WorkerNameEn = m.WorkerNameEn,
                                                       Gender = m.Gender,
                                                       NationalityId = m.NationalityId,
                                                       GenderLookup = m.GenderLookup,
                                                       FacilityProfile = m.FacilityProfile,
                                                       JobPositionLookup = m.JobPositionLookup,
                                                       LastWorkersExposuresDose = m.WorkersExposuresDoses.OrderByDescending(c => c.LineNum).FirstOrDefault()
                                                 
                                                   }).ToList();
            }
           
            response.Succeeded = true;
            var Data = _mapper.Map<List<dto.Workers.DTOWorkers>>(response.Data);


            var obj = new List<dto.Workers.DTOGetMassUpdate>();
            var count = 0;
            foreach (var item in Data)
            {
                var mass = new dto.Workers.DTOGetMassUpdate();
                mass.q1Value = item.WorkersExposuresDoses.Any(_ => _.QuarterCode == "1") ? item.WorkersExposuresDoses.FirstOrDefault(_ => _.QuarterCode == "1").DeepDose : 0;
                mass.q2Value = item.WorkersExposuresDoses.Any(_ => _.QuarterCode == "2") ? item.WorkersExposuresDoses.FirstOrDefault(_ => _.QuarterCode == "2").DeepDose : 0 ;
                mass.q3Value = item.WorkersExposuresDoses.Any(_ => _.QuarterCode == "3") ? item.WorkersExposuresDoses.FirstOrDefault(_ => _.QuarterCode == "3").DeepDose : 0;
                mass.q4Value = item.WorkersExposuresDoses.Any(_ => _.QuarterCode == "4") ? item.WorkersExposuresDoses.FirstOrDefault(_ => _.QuarterCode == "4").DeepDose : 0; ;
                mass.Id = item.Id;
                mass.GenderAr = item.GenderLookup.DisplayNameAr != null ? item.GenderLookup.DisplayNameAr : "";
                mass.GenderEn = item.GenderLookup.DisplayNameEn != null ? item.GenderLookup.DisplayNameAr : "";
                mass.JobPositionAr = item.JobPositionLookup.DisplayNameAr != null ? item.JobPositionLookup.DisplayNameAr : "";
                mass.JobPositionEn = item.JobPositionLookup.DisplayNameEn != null ? item.JobPositionLookup.DisplayNameAr : "";
                mass.NationalityId = item.NationalityId != null ? item.NationalityId : "";
                mass.StatusAr =  item.LookupSetTerm.DisplayNameAr != null ? item.LookupSetTerm.DisplayNameAr : "" ;
                mass.StatusEn = item.LookupSetTerm.DisplayNameEn != null ? item.LookupSetTerm.DisplayNameEn : "";
                mass.BirthDate = item.BirthDate != null ? item.BirthDate : new DateTime();
                mass.WorkerNameAr = item.WorkerNameAr != null ? item.WorkerNameAr : "";
                mass.WorkerNameEn= item.WorkerNameEn != null ? item.WorkerNameEn : "";

                obj.Add(mass);
            }
            var recordsTotal = obj.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;

            return new PagingResponse<List<dto.Workers.DTOGetMassUpdate>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<dto.Workers.DTOGetMassUpdate>>(obj),
                Draw = response.Draw,
                RecordsTotal = response.RecordsTotal,
                RecordsFiltered = response.RecordsFiltered
            };

        }


        #endregion


    }
}
