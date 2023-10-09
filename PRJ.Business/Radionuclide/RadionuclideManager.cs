using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities;
using dto = PRJ.Application.DTOs;
using enc = PRJ.GlobalComponents.Encryption;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;


namespace PRJ.Business.Radionuclide
{
    public class RadionuclideManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        private readonly RepositoryContext _db;
        private readonly IMemoryCache _memoryCache;


        public RadionuclideManager(rep.IUnitOfWork manager, IMapper mapper, RepositoryContext db, IMemoryCache memoryCache)
        {
            this._manager = manager;
            this._db = db;
            _mapper = mapper;
            this._memoryCache = memoryCache;
        }

        public IQueryable<Radionuclides> GetRadionuclides()
        {
            return _db.Radionuclides;
        }
        #region Get Radionuclide
        //public async Task<wap.PagedResponse<Radionuclides>> GetAll(int? pageNumber, int pageSize = 2)
        //{
        //    try
        //    {
        //        IQueryable<Radionuclides> Radionuclides = this.GetRadionuclides();

        //        return await wap.PagedResponse<Radionuclides>.CreateAsync(Radionuclides.AsNoTracking(), Radionuclides.Count(), pageNumber ?? 1, pageSize: pageSize, true, "Success", "", null);
        //    }
        //    catch (Exception exp)
        //    {
        //        List<string> _error = new List<string>();
        //        _error.Add(this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name);
        //        _error.Add(exp.Message);
        //        return await wap.PagedResponse<Radionuclides>.CreateAsync(null, 0, 0, 0, false, exp.Message, "", _error);
        //    }
        //}
        #endregion


        public async Task<wap.Response<List<dto.Radionuclide.RadionuclideDto>>> GetAll()
        {
            List<dto.Radionuclide.RadionuclideDto> response;
            bool isExist = _memoryCache.TryGetValue(CashingType.Radionuclides.ToString(), out response);

            if (!isExist)
            {
                var Radionclides = await _manager.Radionuclides.GetAllAsync();
                response = _mapper.Map<List<dto.Radionuclide.RadionuclideDto>>(Radionclides);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
                _memoryCache.Set(CashingType.Radionuclides.ToString(), response, cacheEntryOptions);
            }
            return new wap.Response<List<dto.Radionuclide.RadionuclideDto>>()
            {
                Data = response
            };
        }

        #region Add Radionuclide
        public async Task<wap.Response<bool>> AddRadionuclide(dto.Radionuclide.AddRadionuclideDto body)
        {

            var Data = _mapper.Map<ent.Radionuclides>(body);

            if (_db.Radionuclides.Any(r => r.Isotop.ToLower() == Data.Isotop.ToLower()))
            {
                return new wap.Response<bool>()
                {
                    Succeeded = false,
                    Message = "EXISTS"
                };
            }

            await _manager.Radionuclides.AddAsync(Data);

            return new wap.Response<bool>()
            {
                Data = true,
            };

        }
        #endregion

        #region Update Radionuclide
        public async Task<wap.Response<bool>> UpdateRadionuclide(dto.Radionuclide.UpdateRadionuclideDto body)
        {

            var Data = _mapper.Map<ent.Radionuclides>(body);

            var record = _db.Radionuclides.FirstOrDefaultAsync(r => r.RadionuclideId == Data.RadionuclideId).Result;

            if (record != null)
            {
                record.Isotop = Data.Isotop;
                record.DisplayName = Data.DisplayName;
                record.HalfLife = Data.HalfLife;
                record.HalfLifeUnit = Data.HalfLifeUnit;
                record.IsActive = Data.IsActive;

                await _db.SaveChangesAsync();

                return new wap.Response<bool>()
                {
                    Data = true
                };
            }

            return new wap.Response<bool>()
            {
                Data = false,
                Succeeded = false,
                Message = "NOTEXIST"
            };


        }
        #endregion

        #region Delete Radionuclide
        public async Task<wap.Response<bool>> DeleteRadionuclide(string id)
        {

            int _Id = int.Parse(enc.EncryptQueryURL.Decrypt(id));
            var record = await _manager.Radionuclides.GetEncryptByIdAsync(id);

            if (record != null)
            {
                _db.Remove(record);
                await _db.SaveChangesAsync();
                return new wap.Response<bool>()
                {
                    Data = true
                };
            }

            return new wap.Response<bool>()
            {
                Succeeded = false
            };

        }
        #endregion

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<dto.Radionuclide.RadionuclideDto>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<Radionuclides>>()
            {
                Draw = param.Draw
            };

            // Get all radionuclides
            IQueryable<Radionuclides> radionuclidesList =
                _manager.Radionuclides.GetAll()
                .Include(x => x.HalfLifeUnit)
                .Include(x => x.ExemptionValueUnit);

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
            IQueryable<Radionuclides> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = radionuclidesList
                    .Where(r =>
                    r.RadionuclideId.ToString().Contains(param.Search.Value) ||
                    r.IsActive.ToString().Contains(param.Search.Value) ||
                    r.Isotop.Contains(param.Search.Value) ||
                    //r.DValueUnit.Contains(param.Search.Value) ||
                    r.DisplayName.Contains(param.Search.Value) ||
                    r.HalfLife.ToString().Contains(param.Search.Value)
                    //r.HalfLifeUnit.Contains(param.Search.Value)
                    );
            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "radionuclideId":
                                query = radionuclidesList.Where(u => u.RadionuclideId.ToString().Contains(search.Search.Value));
                                break;
                            case "isActive":
                                query = radionuclidesList.Where(u => u.IsActive.ToString().Contains(search.Search.Value));
                                break;
                            case "isotop":
                                query = radionuclidesList.Where(u => u.Isotop.Contains(search.Search.Value));
                                break;
                            //case "dValueUnit":
                            //    query = radionuclidesList.Where(u => u.DValueUnit.Contains(search.Search.Value));
                            //    break;
                            case "displayName":
                                query = radionuclidesList.Where(u => u.DisplayName.Contains(search.Search.Value));
                                break;
                            case "halfLife":
                                query = radionuclidesList.Where(u => u.HalfLife.ToString().Contains(search.Search.Value));
                                break;
                                //case "halfLifeUnit":
                                //    query = radionuclidesList.Where(u => u.HalfLifeUnit.Contains(search.Search.Value));
                                //    break;
                        }
                    }
                }
            }
            else
            {
                query = radionuclidesList;
            }

            var colOrder = param.Order[0];

            switch (colOrder.Column)
            {
                case 0:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.Isotop) : query.OrderByDescending(r => r.Isotop);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.DisplayName) : query.OrderByDescending(r => r.DisplayName);
                    break;
                case 4:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.HalfLife) : query.OrderByDescending(r => r.HalfLife);
                    break;
                case 5:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.HalfLifeUnit) : query.OrderByDescending(r => r.HalfLifeUnit);
                    break;
                case 6:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.IsActive) : query.OrderByDescending(r => r.IsActive);
                    break;
            }

            response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;

            return new PagingResponse<List<dto.Radionuclide.RadionuclideDto>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<dto.Radionuclide.RadionuclideDto>>(response.Data),
                Draw = response.Draw,
                RecordsTotal = response.RecordsTotal,
                RecordsFiltered = response.RecordsFiltered
            };


        }

        public async Task<wap.Response<List<dto.Radionuclide.RadionuclideDto>>> GetByNuclearMaterial(string nm)
        {
            List<Radionuclides> Radionclides;
            if (nm == "th" || nm == "pu")
            {
                Radionclides = await _db.Radionuclides.Where(r => r.Isotop.ToLower().Contains(nm.ToLower())).ToListAsync();
            }
            else if (nm == "d" || nm == "n")
            {
                Radionclides = await _db.Radionuclides.Where(r => r.Isotop.ToLower() == "U-235".ToLower()).ToListAsync();
            }
            else if (nm == "e")
            {
                Radionclides = await _db.Radionuclides.Where(r => r.Isotop.ToLower() == "U-235".ToLower() || r.Isotop.ToLower() == "U-233".ToLower() || r.Isotop.ToLower() == "U-238".ToLower()).ToListAsync();
            }
            else
            {
                Radionclides = await _manager.Radionuclides.GetAllAsync();
            }

            return new wap.Response<List<dto.Radionuclide.RadionuclideDto>>()
            {
                Data = _mapper.Map<List<dto.Radionuclide.RadionuclideDto>>(Radionclides)
            };

        }
    }
}
