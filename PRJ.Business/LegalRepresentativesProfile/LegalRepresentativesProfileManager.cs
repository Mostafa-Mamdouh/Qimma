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
namespace PRJ.Business.LegalRepresentativesProfile
{
    public class LegalRepresentativesProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public LegalRepresentativesProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>>> GetAll()
        {
           
                var LegalRepresentativesProfile = await _manager.LegalRepresentativesProfile.GetAllAsync();

                return new wap.Response<List<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>>()
                {
                    Data = _mapper.Map<List<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>>(LegalRepresentativesProfile)
                };
            
           
        }


        public async Task<wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>> GetLegalRepresentativesProfileById(string Id)
        {
            
                var getById = await _manager.LegalRepresentativesProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>()
                    {
                        Data = _mapper.Map<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>> UpdateLegalRepresentativesProfile(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile body)
        {
            
                var LegalRepresentativesProfile = await _manager.LegalRepresentativesProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.LegalRepresentativesProfile>(body);
                _manager.LegalRepresentativesProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>();
            
           
        }


        public async Task<wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>> AddLegalRepresentativesProfile(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile body)
        {
            
                var Data = _mapper.Map<ent.LegalRepresentativesProfile>(body);
                await _manager.LegalRepresentativesProfile.AddAsync(Data);
                return new wap.Response<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile>();
            
           
        }
    }
}
