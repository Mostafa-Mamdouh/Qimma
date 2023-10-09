using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.ManufacturerMaster
{
    public class ManufacturerMasterManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public ManufacturerMasterManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.DTOManufacturerMaster>>> GetAll()
        {
            
                var ManufacturerMaster = await _manager.ManufacturerMaster.GetByQuery(_ => _.ManufacturerId != 0).Include(_ => _.BasCountries).ToListAsync();

                return new wap.Response<List<dto.DTOManufacturerMaster>>()
                {
                    Data = _mapper.Map<List<dto.DTOManufacturerMaster>>(ManufacturerMaster)
                };
          
        }


        public async Task<wap.Response<dto.DTOManufacturerMaster>> GetManufacturerMasterById(string Id)
        {
            
                var getById = await _manager.ManufacturerMaster.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.DTOManufacturerMaster>()
                    {
                        Data = _mapper.Map<dto.DTOManufacturerMaster>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.DTOManufacturerMaster>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }

        public async Task<wap.Response<bool>> UpdateManufacturerMaster(dto.DTOManufacturerMaster body)
        {
           
                var ManufacturerMaster = await _manager.ManufacturerMaster.GetEncryptByIdAsync(body.Id);
                ManufacturerMaster.ManufacturerDescEn = body.ManufacturerDescEn;
                ManufacturerMaster.ManufacturerDescAr = body.ManufacturerDescAr;
                ManufacturerMaster.AddressLine2 = body.AddressLine2;
                ManufacturerMaster.AddressLine3 = body.AddressLine3;
                ManufacturerMaster.AddressLine1 = body.AddressLine1;
                ManufacturerMaster.City = body.City;
                ManufacturerMaster.Location = body.Location;
                ManufacturerMaster.MobileNo = body.MobileNo;
                ManufacturerMaster.PhoneNo = body.PhoneNo;
                ManufacturerMaster.POBox = body.POBox;
                ManufacturerMaster.CountryId = body.CountryId;
                ManufacturerMaster.EmailId = body.EmailId;
                ManufacturerMaster.ZipCode = body.ZipCode;
                _manager.ManufacturerMaster.Update(ManufacturerMaster);
                await _manager.CompleteAsync();

                return new wap.Response<bool>() { Data = true };
            
          
        }
        public async Task<wap.Response<bool>> DeleteManufacturerMaster(string Id)
        {
            
                var getById = await _manager.ManufacturerMaster.GetEncryptByIdAsync(Id);

                await _manager.ManufacturerMaster.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
           
        }

        public async Task<wap.Response<bool>> AddManufacturerMaster(dto.DTOManufacturerMaster body)
        {
            


                bool insert = await _manager.ManufacturerMaster.AddAsync(new ent.ManufacturerMaster
                {
                    AddressLine1 = body.AddressLine1,
                    AddressLine2 = body.AddressLine2,
                    AddressLine3 = body.AddressLine3,
                    City = body.City,
                    CountryId = body.CountryId,
                    Location = body.Location,
                    ManufacturerDescAr = body.ManufacturerDescAr,
                    ManufacturerDescEn = body.ManufacturerDescEn,
                    MobileNo = body.MobileNo,
                    EmailId = body.EmailId,
                    PhoneNo = body.PhoneNo,
                    POBox = body.POBox,
                    ZipCode = body.ZipCode,
                    CreatedBy = body.CreatedBy,
                    ModifiedBy = body.ModifiedBy,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                });

                if (insert)
                {
                    return new wap.Response<bool>() { Data = true };
                }
                else
                {
                    return new wap.Response<bool>() { Data = true, Succeeded = false };
                }
            
        }

        public async Task<wap.PagingResponse<List<dto.DTOManufacturerMaster>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.ManufacturerMaster>>()
            {
                Draw = param.Draw
            };
           
                // Get all
                IQueryable<ent.ManufacturerMaster> dataList = _manager.ManufacturerMaster.GetAll();

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
                IQueryable<ent.ManufacturerMaster> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.ManufacturerId.ToString().Contains(param.Search.Value) ||
                        r.ManufacturerDescAr.Contains(param.Search.Value) ||
                        r.ManufacturerDescEn.Contains(param.Search.Value) ||
                        r.PhoneNo.Contains(param.Search.Value) ||
                        r.MobileNo.Contains(param.Search.Value) ||
                        r.EmailId.Contains(param.Search.Value) ||
                        r.Location.Contains(param.Search.Value) ||
                        r.AddressLine1.Contains(param.Search.Value) ||
                        r.AddressLine2.Contains(param.Search.Value) ||
                        r.AddressLine3.Contains(param.Search.Value) ||
                        r.City.ToString().Contains(param.Search.Value) ||
                        r.ZipCode.Contains(param.Search.Value) ||
                        r.POBox.Contains(param.Search.Value) ||
                        r.CountryId.ToString().Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "manufacturerId":
                                    query = dataList.Where(u => u.ManufacturerId.ToString().Contains(search.Search.Value));
                                    break;
                                case "manufacturerDescAr":
                                    query = dataList.Where(u => u.ManufacturerDescAr.Contains(search.Search.Value));
                                    break;
                                case "manufacturerDescEn":
                                    query = dataList.Where(u => u.ManufacturerDescEn.Contains(search.Search.Value));
                                    break;
                                case "phoneNo":
                                    query = dataList.Where(u => u.PhoneNo.Contains(search.Search.Value));
                                    break;
                                case "mobileNo":
                                    query = dataList.Where(u => u.MobileNo.Contains(search.Search.Value));
                                    break;
                                case "emailId":
                                    query = dataList.Where(u => u.EmailId.Contains(search.Search.Value));
                                    break;
                                case "location":
                                    query = dataList.Where(u => u.Location.Contains(search.Search.Value));
                                    break;
                                case "addressLine1":
                                    query = dataList.Where(u => u.AddressLine1.Contains(search.Search.Value));
                                    break;
                                case "addressLine2":
                                    query = dataList.Where(u => u.AddressLine2.ToString().Contains(search.Search.Value));
                                    break;
                                case "addressLine3":
                                    query = dataList.Where(u => u.AddressLine3.ToString().Contains(search.Search.Value));
                                    break;
                                case "city":
                                    query = dataList.Where(u => u.City.ToString().Contains(search.Search.Value));
                                    break;
                                case "zipCode":
                                    query = dataList.Where(u => u.ZipCode.ToString().Contains(search.Search.Value));
                                    break;
                                case "POBox":
                                    query = dataList.Where(u => u.POBox.ToString().Contains(search.Search.Value));
                                    break;
                                case "countryId":
                                    query = dataList.Where(u => u.CountryId.ToString().Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerDescAr) : query.OrderByDescending(r => r.ManufacturerDescAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerDescEn) : query.OrderByDescending(r => r.ManufacturerDescEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PhoneNo) : query.OrderByDescending(r => r.PhoneNo);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EmailId) : query.OrderByDescending(r => r.EmailId);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.MobileNo) : query.OrderByDescending(r => r.MobileNo);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.City) : query.OrderByDescending(r => r.City);
                        break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ZipCode) : query.OrderByDescending(r => r.ZipCode);
                        break;
                    case 7:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.POBox) : query.OrderByDescending(r => r.POBox);
                        break;
                    case 8:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CountryId) : query.OrderByDescending(r => r.CountryId);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.DTOManufacturerMaster>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.DTOManufacturerMaster>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
           

        }
    }

}
