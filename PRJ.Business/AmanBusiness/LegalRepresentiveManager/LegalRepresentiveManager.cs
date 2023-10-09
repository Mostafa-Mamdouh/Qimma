using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRJ.Application.Enums;
using PRJ.Domain.Entities;
using bus = PRJ.Business;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.AmanBusiness
{
    public class LegalRepresentiveManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;


        public LegalRepresentiveManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<ApiResponse> SaveEntity(dto.DTOLegalRepresentative body)
        {
            ent.LegalRepresentativesProfile legalRepresentativesProfile = null;
            ent.LookupSet userType = null;
            ent.LookupSetTerm term = _mapper.Map<LookupSetTerm>(body.UserType);
            using (var transaction = _manager.BeginTransaction())
            {
                try
                {
                    #region UserType
                    userType = await _manager.LookupSet.GetByQuery(_ => _.ClassName == LookupSetClass.UserType.ToString()).Include(c => c.LookupSetTerms).FirstOrDefaultAsync();
                    if (userType == null)
                    {
                        userType = new ent.LookupSet();
                        userType.IsActive = true;
                        userType.ClassName = LookupSetClass.UserType.ToString();
                        userType.DisplayNameAr = LookupSetClass.UserType.ToString();
                        userType.DisplayNameEn = LookupSetClass.UserType.ToString();
                        userType.LookupSetTerms.Add(term);
                        await _manager.LookupSet.AddAsync(userType);
                    }
                    else
                    {
                        var lookupTerm = userType.LookupSetTerms.FirstOrDefault(x => body.UserType.Id > 0 && x.AmanId == body.UserType.Id);
                        if (lookupTerm == null)
                        {
                            term.LookupSetId = userType.LookupSetId;
                            await _manager.LookupSetTerm.AddAsync(term);
                        }
                        else
                        {
                            userType.LookupSetTerms.First(x => x.AmanId == body.UserType.Id).DisplayNameAr = term.DisplayNameAr;
                            userType.LookupSetTerms.First(x => x.AmanId == body.UserType.Id).DisplayNameEn = term.DisplayNameEn;
                        }
                        _manager.LookupSet.Update(userType);
                    }
                    await _manager.CompleteAsync();
                    #endregion


                    // legalRepresentativesProfile
                    #region legalRepresentativesProfile
                    var data = _mapper.Map<ent.LegalRepresentativesProfile>(body);
                    data.UserType = userType.LookupSetTerms.FirstOrDefault(x => x.AmanId == body.UserType.Id).LookupSetTermId;

                    legalRepresentativesProfile = await _manager.LegalRepresentativesProfile.GetByQuery(x => x.AmanId == body.LegalRepID.ToString()).FirstOrDefaultAsync();
                    if (legalRepresentativesProfile == null)
                        await _manager.LegalRepresentativesProfile.AddAsync(data);
                    else
                    {
                        legalRepresentativesProfile.LegalRepresentativesNameEn = data.LegalRepresentativesNameEn;
                        legalRepresentativesProfile.LegalRepresentativesNameAr = data.LegalRepresentativesNameAr;
                        legalRepresentativesProfile.EmailId = data.EmailId;
                        legalRepresentativesProfile.Title = data.Title;
                        legalRepresentativesProfile.AmanInsertedOn = data.AmanInsertedOn;
                        legalRepresentativesProfile.PhoneNo = data.PhoneNo;
                        legalRepresentativesProfile.CurrentFacilities = data.CurrentFacilities;
                        legalRepresentativesProfile.Note = data.Note;
                        legalRepresentativesProfile.Status = bool.Parse(data.Status) ? "1" : "0";

                        _manager.LegalRepresentativesProfile.Update(legalRepresentativesProfile);
                    }
                    await _manager.CompleteAsync();
                    #endregion

                    transaction.Commit();
                    return new ApiResponse(200) { Message = ReponseMsg.success.ToString() };
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
