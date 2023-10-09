using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.Domain.Entities;
using System.Collections.Generic;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dec = PRJ.GlobalComponents.Encryption;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.BasCountries
{
    public class CommonManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;


        public CommonManager(rep.IUnitOfWork manager, IMapper mapper, IMemoryCache memoryCache)
        {
            this._manager = manager;
            this._mapper = mapper;
            this._memoryCache = memoryCache;
        }

        #region Get Countries
        public async Task<wap.Response<List<dto.BasCountries.DTONationalites>>> GetNationalites()
        {
            
                var nationalites = await _manager.BasCountries.GetAllAsync();

                return new wap.Response<List<dto.BasCountries.DTONationalites>>()
                {
                    Data = _mapper.Map<List<dto.BasCountries.DTONationalites>>(nationalites)
                };
            
         
        }
        public async Task<wap.Response<List<dto.BasCountries.DTOCountries2>>> AddCountries(dto.BasCountries.DTOCountries2 Data)
        {
            

                var body = _mapper.Map<ent.BasCountries>(Data);

                await _manager.BasCountries.AddAsync(body);

                return new wap.Response<List<dto.BasCountries.DTOCountries2>>();

            
           
        }
        public async Task<wap.Response<List<dto.BasCountries.DTOCountries2>>> UpdateCountries(dto.BasCountries.DTOCountries2 Data)
        {
            
                var body = _manager.BasCountries.GetEncryptById(Data.Id);
                body.NationalityNameNtv = Data.NationalityNameNtv;
                body.NationalityNameFrn = Data.NationalityNameFrn;
                body.CountryNameAr = Data.CountryNameAr;
                body.CountryCodeISO = Data.CountryCodeISO;
                body.CountryNameEn = Data.CountryNameEn;
                _manager.BasCountries.Update(body);
                await _manager.CompleteAsync();
                return new wap.Response<List<dto.BasCountries.DTOCountries2>>();

            
           
        }
        public async Task<wap.Response<List<dto.BasCountries.DTOCountries2>>> GetCountries()
        {
           
                var nationalites = await _manager.BasCountries.GetAllAsync();

                return new wap.Response<List<dto.BasCountries.DTOCountries2>>()
                {
                    Data = _mapper.Map<List<dto.BasCountries.DTOCountries2>>(nationalites)
                };
            
           
        }

        public async Task<wap.Response<dto.BasCountries.DTOCountries>> GetCountryByCode(string Code)
        {
            
                var getByCode = await _manager.BasCountries.GetByQuery(_ => _.CountryCodeISO == Code).FirstOrDefaultAsync();

                if (getByCode != null)
                {
                    return new wap.Response<dto.BasCountries.DTOCountries>()
                    {
                        Data = _mapper.Map<dto.BasCountries.DTOCountries>(getByCode)
                    };
                }
                else
                {
                    return new wap.Response<dto.BasCountries.DTOCountries>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }
        public async Task<wap.Response<dto.BasCountries.DTONationalites>> GetNationalityByCode(string Code)
        {
            
                var getByCode = await _manager.BasCountries.GetByQuery(_ => _.CountryCodeISO == Code).FirstOrDefaultAsync();

                if (getByCode != null)
                {
                    return new wap.Response<dto.BasCountries.DTONationalites>()
                    {
                        Data = _mapper.Map<dto.BasCountries.DTONationalites>(getByCode)
                    };
                }
                else
                {
                    return new wap.Response<dto.BasCountries.DTONationalites>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }
        public async Task<wap.Response<dto.BasCountries.DTOCountries>> GetCountryById(string Id)
        {
            
                var getByCode = await _manager.BasCountries.GetEncryptByIdAsync(Id);

                if (getByCode != null)
                {
                    return new wap.Response<dto.BasCountries.DTOCountries>()
                    {
                        Data = _mapper.Map<dto.BasCountries.DTOCountries>(getByCode)
                    };
                }
                else
                {
                    return new wap.Response<dto.BasCountries.DTOCountries>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
            
        }
        public async Task<wap.Response<List<ent.BasCountries>>> GetCountriesClear()
        {
            
                return new wap.Response<List<ent.BasCountries>>()
                {
                    Data = await _manager.BasCountries.GetAllAsync()
                };
            
            
        }
        public async Task<wap.Response<ent.BasCountries>> GetCountryByIdClear(int Id)
        {
            
                var getByCode = await _manager.BasCountries.GetByIdAsync(Id);

                if (getByCode != null)
                {
                    return new wap.Response<ent.BasCountries>()
                    {
                        Data = getByCode
                    };
                }
                else
                {
                    return new wap.Response<ent.BasCountries>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
            
        }
        public async Task<wap.Response<dto.BasCountries.DTONationalites>> GetNationalityById(string Id)
        {
            
                var getByCode = await _manager.BasCountries.GetEncryptByIdAsync(Id);

                if (getByCode != null)
                {
                    return new wap.Response<dto.BasCountries.DTONationalites>()
                    {
                        Data = _mapper.Map<dto.BasCountries.DTONationalites>(getByCode)
                    };
                }
                else
                {
                    return new wap.Response<dto.BasCountries.DTONationalites>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }
        public async Task<wap.Response<dto.BasCountries.DTOCountries>> DeleteCountries(string Id)
        {
            
                var getById = await _manager.BasCountries.GetEncryptByIdAsync(Id);

                await _manager.BasCountries.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<dto.BasCountries.DTOCountries>();
            
           
        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<dto.BasCountries.DTOCountries2>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.BasCountries>>()
            {
                Draw = param.Draw
            };
            
                // Get all
                IQueryable<ent.BasCountries> dataList = _manager.BasCountries.GetAll();

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
                IQueryable<ent.BasCountries> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.CountryId.ToString().Contains(param.Search.Value) ||
                        r.CountryNameAr.Contains(param.Search.Value) ||
                        r.CountryNameEn.Contains(param.Search.Value) ||
                        r.CountryCodeISO.Contains(param.Search.Value) ||
                        r.NationalityNameFrn.Contains(param.Search.Value) ||
                        r.NationalityNameNtv.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "countryId":
                                    query = dataList.Where(u => u.CountryId.ToString().Contains(search.Search.Value));
                                    break;
                                case "countryNameAr":
                                    query = dataList.Where(u => u.CountryNameAr.Contains(search.Search.Value));
                                    break;
                                case "countryNameEn":
                                    query = dataList.Where(u => u.CountryNameEn.Contains(search.Search.Value));
                                    break;
                                case "countryCodeISO":
                                    query = dataList.Where(u => u.CountryCodeISO.Contains(search.Search.Value));
                                    break;
                                case "nationalityNameFrn":
                                    query = dataList.Where(u => u.NationalityNameFrn.Contains(search.Search.Value));
                                    break;
                                case "nationalityNameNtv":
                                    query = dataList.Where(u => u.NationalityNameNtv.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CountryId) : query.OrderByDescending(r => r.CountryId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CountryNameAr) : query.OrderByDescending(r => r.CountryNameAr);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CountryNameEn) : query.OrderByDescending(r => r.CountryNameEn);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CountryCodeISO) : query.OrderByDescending(r => r.CountryCodeISO);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NationalityNameFrn) : query.OrderByDescending(r => r.NationalityNameFrn);
                        break;
                }

                response.Data = await query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToListAsync();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.BasCountries.DTOCountries2>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.BasCountries.DTOCountries2>>(response.Data),
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered

                };
            
            
        }

        #endregion

        #region Get Cites
        public async Task<wap.Response<List<dto.BasCites.DTOCites>>> GetCitesByCountryCode(string Code)
        {
            
                var _country = await _manager.BasCountries.GetByQuery(_ => _.CountryCodeISO == Code).FirstOrDefaultAsync();

                if (_country != null)
                {
                    var _cites = _manager.BasCities.GetByQuery(_ => _.CountryId == _country.CountryId).ToListAsync().Result;

                    if (_cites != null)
                    {
                        return new wap.Response<List<dto.BasCites.DTOCites>>()
                        {
                            Data = _mapper.Map<List<dto.BasCites.DTOCites>>(_cites)
                        };
                    }
                }
                return new wap.Response<List<dto.BasCites.DTOCites>>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };

            
           
        }
        public async Task<wap.Response<List<dto.BasCites.DTOCites>>> GetCitesByCountryId(string CountryId)
        {
            
                var _country = await _manager.BasCountries.GetByQuery(_ => _.CountryId == int.Parse(dec.EncryptQueryURL.Decrypt(CountryId))).FirstOrDefaultAsync();

                if (_country != null)
                {
                    var _cites = _manager.BasCities.GetByQuery(_ => _.CountryId == _country.CountryId).ToListAsync().Result;

                    if (_cites != null)
                    {
                        return new wap.Response<List<dto.BasCites.DTOCites>>()
                        {
                            Data = _mapper.Map<List<dto.BasCites.DTOCites>>(_cites)
                        };
                    }
                }
                return new wap.Response<List<dto.BasCites.DTOCites>>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };

            
          
        }
        public async Task<wap.Response<List<ent.BasCities>>> GetCitesByCountryIdClear(int CountryId)
        {
            
                var _country = await _manager.BasCountries.GetByQuery(_ => _.CountryId == CountryId).FirstOrDefaultAsync();

                if (_country != null)
                {
                    var _cites = _manager.BasCities.GetByQuery(_ => _.CountryId == _country.CountryId).ToListAsync().Result;

                    if (_cites != null)
                    {
                        return new wap.Response<List<ent.BasCities>>()
                        {
                            Data = _cites
                        };
                    }
                }
                return new wap.Response<List<ent.BasCities>>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };

            
            
        }
        public async Task<wap.Response<dto.BasCites.DTOCites>> GetCityById(string Id)
        {
            
                var getById = await _manager.BasCities.GetByQuery(_ => _.CityId == int.Parse(dec.EncryptQueryURL.Decrypt(Id))).FirstOrDefaultAsync();


                if (getById != null)
                {
                    return new wap.Response<dto.BasCites.DTOCites>()
                    {
                        Data = _mapper.Map<dto.BasCites.DTOCites>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.BasCites.DTOCites>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
            
        }
        public async Task<wap.Response<bool>> AddCity(dto.BasCites.DTOCites Data)
        {
            
                await _manager.BasCities.AddAsync(new ent.BasCities
                {
                    CountryId = int.Parse(dec.EncryptQueryURL.Decrypt(Data.CountryId)),
                    NameAr = Data.NameAr,
                    NameEn = Data.NameEn,
                    ModifiedBy = Data.CreatedBy,
                    CityAbbrCode = Data.CityAbbrCode,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = Data.CreatedBy,
                    CreatedOn = DateTime.Now,
                });

                return new wap.Response<bool>()
                {
                    Data = true
                };
            
           
        }
        public async Task<wap.Response<bool>> UpdateCity(dto.BasCites.DTOCites Data)
        {
            
                _manager.BasCities.Update(new ent.BasCities
                {
                    CityId = int.Parse(dec.EncryptQueryURL.Decrypt(Data.Id)),
                    CountryId = int.Parse(dec.EncryptQueryURL.Decrypt(Data.CountryId)),
                    NameAr = Data.NameAr,
                    NameEn = Data.NameEn,
                    ModifiedBy = Data.ModifiedBy,
                    ModifiedOn = DateTime.Now,
                    CityAbbrCode = Data.CityAbbrCode,

                });
                await _manager.CompleteAsync();

                return new wap.Response<bool>()
                {
                    Data = true
                };
            
            
        }
        public async Task<wap.Response<bool>> DeleteCity(string Id)
        {
           
                int _id = int.Parse(dec.EncryptQueryURL.Decrypt(Id));
                var getById = _manager.BasCities.GetByQuery(_ => _.CityId == _id).FirstOrDefault();

                await _manager.BasCities.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            
            
        }
        public async Task<wap.PagingResponse<List<dto.BasCites.DTOCites>>> GetAllCitiesFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.BasCities>>()
            {
                Draw = param.Draw
            };
           
                var _country = await _manager.BasCountries.GetByQuery(_ => _.CountryId == int.Parse(dec.EncryptQueryURL.Decrypt(param.extra))).FirstOrDefaultAsync();

                // Get all
                IQueryable<ent.BasCities> dataList = _manager.BasCities.GetByQuery(_ => _.CountryId == _country.CountryId);

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
                IQueryable<ent.BasCities> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.CityId.ToString().Contains(param.Search.Value) ||
                        r.CityAbbrCode.Contains(param.Search.Value) ||
                        r.CountryId.ToString().Contains(param.Search.Value) ||
                        r.CreatedBy.Contains(param.Search.Value) ||
                        r.CreatedOn.ToString().Contains(param.Search.Value) ||
                        r.NameEn.Contains(param.Search.Value) ||
                        r.NameAr.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "cityAbbrCode":
                                    query = dataList.Where(u => u.CityAbbrCode.Contains(search.Search.Value));
                                    break;
                                case "createdBy":
                                    query = dataList.Where(u => u.CreatedBy.Contains(search.Search.Value));
                                    break;
                                case "createdOn":
                                    query = dataList.Where(u => u.CreatedOn.ToString().Contains(search.Search.Value));
                                    break;
                                case "nameEn":
                                    query = dataList.Where(u => u.NameEn.Contains(search.Search.Value));
                                    break;
                                case "nameAr":
                                    query = dataList.Where(u => u.NameAr.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NameAr) : query.OrderByDescending(r => r.NameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NameEn) : query.OrderByDescending(r => r.NameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CityAbbrCode) : query.OrderByDescending(r => r.CityAbbrCode);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.BasCites.DTOCites>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.BasCites.DTOCites>>(response.Data),
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered

                };
            
           
        }
        #endregion

        #region Trabsaction Types and Status
        public async Task<wap.Response<List<dto.DTOTransactionTypeMaster>>> GetAllTransactionTypes()
        {
            
                var transactionTypes = new List<dto.DTOTransactionTypeMaster>();
                bool isExist = _memoryCache.TryGetValue(CashingType.TransactionTypeMaster.ToString(), out transactionTypes);
                if (!isExist)
                {
                    transactionTypes = _mapper.Map<List<dto.DTOTransactionTypeMaster>>(await _manager.TransactionTypeMaster.GetAllAsync());
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8));
                    _memoryCache.Set(CashingType.TransactionTypeMaster.ToString(), transactionTypes, cacheEntryOptions);
                }
                return new wap.Response<List<dto.DTOTransactionTypeMaster>>()
                {
                    Data = transactionTypes
                };
            
            
        }


        public async Task<wap.Response<List<dto.DTOItemSourceStatus>>> GetAllItemSourceStatus()
        {
           
                var itemSourceStatus = new List<dto.DTOItemSourceStatus>();
                bool isExist = _memoryCache.TryGetValue(CashingType.ItemSourceStatus.ToString(), out itemSourceStatus);
                if (!isExist)
                {
                    itemSourceStatus = _mapper.Map<List<dto.DTOItemSourceStatus>>(await _manager.ItemSourceStatus.GetAllAsync());
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(8));
                    _memoryCache.Set(CashingType.ItemSourceStatus.ToString(), itemSourceStatus, cacheEntryOptions);
                }
                return new wap.Response<List<dto.DTOItemSourceStatus>>()
                {
                    Data = itemSourceStatus
                };
              
            
            
        }
        #endregion




    }
}
