using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Enums;
using bus = PRJ.Business;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;
using PRJ.Domain.Entities;
using Org.BouncyCastle.Math.EC.Rfc7748;
using PRJ.GlobalComponents.Const;
using PRJ.Repository;
using System.Diagnostics;

namespace PRJ.Business.AmanBusiness
{
    public class FacilityManager
    {

        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;

        public FacilityManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

  
        public async Task<ApiResponse> SaveFacility(dto.DTOFacilityProfile body)
        {
            ent.FacilityProfile facilityprofile = null;
            ent.LookupSet province = null;
            ent.LookupSet city = null;
            ent.LookupSetTerm provinceterm = _mapper.Map<LookupSetTerm>(body.Province);



            using (var transaction = _manager.BeginTransaction())
            {
                try
                {
                    // Province
                    #region province and city
                    province = await _manager.LookupSet.GetByQuery(_ => _.ClassName == LookupSetClass.Province.ToString()).Include(c => c.LookupSetTerms).FirstOrDefaultAsync();
                    if (province == null)
                    {
                        province = new ent.LookupSet();
                        province.IsActive = true;
                        province.ClassName = LookupSetClass.Province.ToString();
                        province.DisplayNameAr = LookupSetClass.Province.ToString();
                        province.DisplayNameEn = LookupSetClass.Province.ToString();
                        province.LookupSetTerms.Add(provinceterm);
                        await _manager.LookupSet.AddAsync(province);
                    }
                    else
                    {
                        var lookupTerm = province.LookupSetTerms.FirstOrDefault(x => body.Province.Id > 0 && x.AmanId == body.Province.Id);
                        if (lookupTerm == null)
                        {
                            provinceterm.LookupSetId = province.LookupSetId;
                            await _manager.LookupSetTerm.AddAsync(provinceterm);
                        }
                        else
                        {
                            province.LookupSetTerms.First(x => x.AmanId == body.Province.Id).DisplayNameAr = provinceterm.DisplayNameAr;
                            province.LookupSetTerms.First(x => x.AmanId == body.Province.Id).DisplayNameEn = provinceterm.DisplayNameEn;
                        }
                        _manager.LookupSet.Update(province);
                    }
                    await _manager.CompleteAsync();
                    #endregion
                    #region Facility
                    var data = _mapper.Map<ent.FacilityProfile>(body);
                    provinceterm = await _manager.LookupSetTerm.GetByQuery(x => x.AmanId == data.Province && x.LookupSet.ClassName == LookupSetClass.Province.ToString()).FirstOrDefaultAsync();
                    var entity = await _manager.EntityProfile.GetByQuery(x => x.AmanId == body.EntityID.ToString()).FirstOrDefaultAsync();
                    if (entity == null)
                        return new ApiResponse(400) { Message = ConstErrors.EntityIdNotExist };
                    data.EntityId = entity.EntityId;
                    facilityprofile = await _manager.FacilityProfile.GetByQuery(x => x.AmanFacilityId == body.FacilityId.ToString()).FirstOrDefaultAsync();
                    if (facilityprofile == null)
                        await _manager.FacilityProfile.AddAsync(data);
                    else
                    {
                        facilityprofile.EntityId = data.EntityId;
                        facilityprofile.FacilityNameEn = data.FacilityNameEn;
                        facilityprofile.FacilityNameAr = data.FacilityNameAr;
                        facilityprofile.FacilityCode = data.FacilityCode;
                        facilityprofile.AmanInsertedOn = data.AmanInsertedOn;
                        facilityprofile.Province = provinceterm.LookupSetTermId;
                        facilityprofile.City = data.City;
                        facilityprofile.Location = data.Location;
                        
                        _manager.FacilityProfile.Update(facilityprofile);
                    }
                    await _manager.CompleteAsync();

                    transaction.Commit();
                    return new ApiResponse(200) { Message = ReponseMsg.success.ToString() };

                    #endregion
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }
    }
}
