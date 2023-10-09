using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Enums;
using PRJ.GlobalComponents.Const;
using bus = PRJ.Business;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.AmanBusiness
{
    public class RsoManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;


        public RsoManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<ApiResponse> SaveEntity(dto.DTORso body)
        {
            ent.SafetyResponsibleOfficersProfile SROprofile = null;

            using (var transaction = _manager.BeginTransaction())
            {
                try
                {
                    #region Rso
                    var data = _mapper.Map<ent.SafetyResponsibleOfficersProfile>(body);
              
                    SROprofile = await _manager.SafetyResponsibleOfficersProfile.GetByQuery(x => x.AmanId == body.RSOID.ToString()).FirstOrDefaultAsync();
                    if (SROprofile == null)
                        await _manager.SafetyResponsibleOfficersProfile.AddAsync(data);
                    else
                    {
                        SROprofile.CertificateNo = data.CertificateNo;
                        SROprofile.SRONameEn = data.SRONameEn;
                        SROprofile.SRONameAr = data.SRONameEn;
                        SROprofile.NationalID = data.NationalID;
                        SROprofile.AmanInsertedOn = data.AmanInsertedOn;
                        SROprofile.PhoneNo = data.PhoneNo;
                        SROprofile.EmailId = data.EmailId;

                        _manager.SafetyResponsibleOfficersProfile.Update(SROprofile);
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
