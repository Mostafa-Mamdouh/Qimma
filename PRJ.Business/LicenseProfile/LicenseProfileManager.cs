using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using PRJ.GlobalComponents.Encryption;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.LicenseProfile
{
    public class LicenseProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public LicenseProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.LicenseProfile.DTOLicenseProfile>>> GetAll()
        {

            var LicenseProfile = await _manager.LicenseProfile.GetByQuery(_ => _.LicenseId != null).Include(_ => _.EntityProfile).Include(_ => _.FacilityProfile).ToListAsync();

            return new wap.Response<List<dto.LicenseProfile.DTOLicenseProfile>>()
            {
                Data = _mapper.Map<List<dto.LicenseProfile.DTOLicenseProfile>>(LicenseProfile)
            };
        }

        public async Task<wap.Response<List<dto.LicenseProfile.DTOLicenseNumber>>> GetLicenseNumber()
        {

            var LicenseProfile = await _manager.LicenseProfile.GetAllAsync();
            return new wap.Response<List<dto.LicenseProfile.DTOLicenseNumber>>()
            {
                Data = _mapper.Map<List<dto.LicenseProfile.DTOLicenseNumber>>(LicenseProfile)
            };


        }
        public async Task<wap.Response<List<dto.LicenseProfile.DTOLicenseProfile>>> GetByFacilityId(string FacId)
        {

            var Facility = EncryptQueryURL.Decrypt(FacId);
            var FacilityId = int.Parse(Facility);
            var data = await _manager.LicenseProfile.GetByQuery(_ => _.FacilityId == FacilityId).ToListAsync();
            List<dto.LicenseProfile.DTOLicenseProfile> res = _mapper.Map<List<dto.LicenseProfile.DTOLicenseProfile>>(data);

            foreach (var item in res)
            {
                item.NumberOfSealedSources = 0;
                item.NumberOfUnsealedSources = 0;
                item.NumberOfShortLivedSources = 0;
                item.NumberOfActiveSources = 0;

                item.NumberOfSealedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == item.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 104).Count();

                item.NumberOfUnsealedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == item.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 105).Count();

                item.NumberOfShortLivedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == item.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 106).Count();

                item.NumberOfActiveSources = item.NumberOfSealedSources + item.NumberOfUnsealedSources + item.NumberOfShortLivedSources;

                item.NumberOfMonitoredWorkers = 0;
            }

            return new wap.Response<List<dto.LicenseProfile.DTOLicenseProfile>>()
            {
                Data = res
            };
        }


        public async Task<wap.Response<dto.LicenseProfile.DTOLicenseProfile>> GetLicenseProfileById(string Id)
        {

            var License = EncryptQueryURL.Decrypt(Id);
            var LicenseId = int.Parse(License);
            var data = _manager.LicenseProfile.GetByQuery(_ => _.LicenseId == LicenseId).FirstOrDefault();
            dto.LicenseProfile.DTOLicenseProfile res = _mapper.Map<dto.LicenseProfile.DTOLicenseProfile>(data);

                res.NumberOfSealedSources = 0;
                res.NumberOfUnsealedSources = 0;
                res.NumberOfShortLivedSources = 0;
                res.NumberOfActiveSources = 0;

                res.NumberOfSealedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == res.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 104).Count();

                res.NumberOfUnsealedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == res.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 105).Count();

                res.NumberOfShortLivedSources = _manager.TrnItemSource.GetByQuery(x => x.LicenseId == res.LicenseId && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == 106).Count();

                res.NumberOfActiveSources = res.NumberOfSealedSources + res.NumberOfUnsealedSources + res.NumberOfShortLivedSources;

                res.NumberOfMonitoredWorkers = 0;
           

            return new wap.Response<dto.LicenseProfile.DTOLicenseProfile>()
            {
                Data = res
            };

        }

        public async Task<wap.Response<dto.LicenseProfile.DTOLicenseProfile>> UpdateLicenseProfile(dto.LicenseProfile.DTOLicenseProfile body)
        {

            var LicenseProfile = await _manager.LicenseProfile.GetEncryptByIdAsync(body.Id);
            var Data = _mapper.Map<ent.LicenseProfile>(body);
            _manager.LicenseProfile.Update(Data);
            _manager.Complete();

            return new wap.Response<dto.LicenseProfile.DTOLicenseProfile>();


        }


        public async Task<wap.Response<dto.LicenseProfile.DTOLicenseProfile>> AddLicenseProfile(dto.LicenseProfile.DTOLicenseProfile body)
        {

            var Data = _mapper.Map<ent.LicenseProfile>(body);
            await _manager.LicenseProfile.AddAsync(Data);
            return new wap.Response<dto.LicenseProfile.DTOLicenseProfile>();


        }

        public async Task<wap.PagingResponse<List<dto.LicenseProfile.DTOLicenseProfile>>> GetAllLicensesFuncational(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.LicenseProfile>>()
            {
                Draw = param.Draw
            };

            IQueryable<ent.LicenseProfile> dataList = _manager.LicenseProfile.GetAll();

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
            IQueryable<ent.LicenseProfile> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.AmanInsertedOn.ToString().Contains(param.Search.Value) ||
                    r.LicenseCode.Contains(param.Search.Value) ||
                    r.LicenseDescAr.Contains(param.Search.Value) ||
                    r.LicenseDescEn.Contains(param.Search.Value));
            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "amanInsertedOn":
                                query = dataList.Where(u => u.AmanInsertedOn.ToString().Contains(search.Search.Value));
                                break;
                            case "licenseCode":
                                query = dataList.Where(u => u.LicenseCode.Contains(search.Search.Value));
                                break;
                            case "licenseDescAr":
                                query = dataList.Where(u => u.LicenseDescAr.Contains(search.Search.Value));
                                break;
                            case "licenseDescEn":
                                query = dataList.Where(u => u.LicenseDescEn.Contains(search.Search.Value));
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
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseDescAr) : query.OrderByDescending(r => r.LicenseDescAr);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseDescEn) : query.OrderByDescending(r => r.LicenseDescEn);
                    break;
                case 2:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseCode) : query.OrderByDescending(r => r.LicenseCode);
                    break;
                case 3:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.AmanInsertedOn) : query.OrderByDescending(r => r.AmanInsertedOn);
                    break;
            }

            response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.AmanInsertedOn).ToList();
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;

            return new PagingResponse<List<dto.LicenseProfile.DTOLicenseProfile>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<dto.LicenseProfile.DTOLicenseProfile>>(response.Data),
                Draw = response.Draw,
                RecordsTotal = response.RecordsTotal,
                RecordsFiltered = response.RecordsFiltered
            };


        }
    }
}
