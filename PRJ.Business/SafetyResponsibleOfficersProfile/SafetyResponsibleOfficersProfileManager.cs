using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
namespace PRJ.Business.SafetyResponsibleOfficersProfile
{
    public class SafetyResponsibleOfficersProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public SafetyResponsibleOfficersProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>>> GetAll()
        {
            
                var SafetyResponsibleOfficersProfile = await _manager.SafetyResponsibleOfficersProfile.GetAllAsync();

                return new wap.Response<List<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>>()
                {
                    Data = _mapper.Map<List<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>>(SafetyResponsibleOfficersProfile)
                };
           
        }


        public async Task<wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>> GetSafetyResponsibleOfficersProfileById(string Id)
        {
           
                var getById = await _manager.SafetyResponsibleOfficersProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>()
                    {
                        Data = _mapper.Map<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }

        public async Task<wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>> UpdateSafetyResponsibleOfficersProfile(dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile body)
        {
            
                var SafetyResponsibleOfficersProfile = await _manager.SafetyResponsibleOfficersProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.SafetyResponsibleOfficersProfile>(body);
                _manager.SafetyResponsibleOfficersProfile.Update(Data);
                _manager.Complete();
                return new wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>();
          
        }


        public async Task<wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>> AddSafetyResponsibleOfficersProfile(dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile body)
        {
           
                var SafetyResponsibleOfficersProfile = new ent.SafetyResponsibleOfficersProfile();
                SafetyResponsibleOfficersProfile.SRONameAr = body.SRONameAr;
                SafetyResponsibleOfficersProfile.SRONameEn = body.SRONameEn;
                SafetyResponsibleOfficersProfile.NationalID = body.NationalID;
                SafetyResponsibleOfficersProfile.PhoneNo = body.PhoneNo;
                SafetyResponsibleOfficersProfile.MobileNo = body.MobileNo;
                SafetyResponsibleOfficersProfile.EmailId = body.EmailId;
                SafetyResponsibleOfficersProfile.IssuanceDate = body.IssuanceDate;
                SafetyResponsibleOfficersProfile.AmanInsertedOn = body.AmanInsertedOn;
                SafetyResponsibleOfficersProfile.CertificateNo = body.CertificateNo;
                SafetyResponsibleOfficersProfile.ExpiryDate = body.ExpiryDate;

                await _manager.SafetyResponsibleOfficersProfile.AddAsync(SafetyResponsibleOfficersProfile);
                return new wap.Response<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>()
                {
                    Data = _mapper.Map<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile>(SafetyResponsibleOfficersProfile)
                };
           
        }
    }

}

