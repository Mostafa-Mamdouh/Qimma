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
namespace PRJ.Business.PermitDetailsProfile
{
    public class PermitDetailsProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public PermitDetailsProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>> GetAll()
        {

            var PermitDetailsProfile = await _manager.PermitDetailsProfile.GetByQuery(_ => _.PermitDetailsId != null).Include(_ => _.LicenseProfile).ToListAsync();

            return new wap.Response<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>()
            {
                Data = _mapper.Map<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>(PermitDetailsProfile)
            };

        }

        public async Task<wap.Response<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>> GetByLicenseId(string LicId)
        {

            var Lic = EncryptQueryURL.Decrypt(LicId);
            var LicenseId = int.Parse(Lic);
            var PermitDetailsProfile = await _manager.PermitDetailsProfile.GetByQuery(_ => _.LicenseId == LicenseId).ToListAsync();

            return new wap.Response<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>()
            {
                Data = _mapper.Map<List<dto.PermitDetailsProfile.DTOPermitDetailsProfile>>(PermitDetailsProfile)
            };

        }

        

        public async Task<wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>> GetPermitDetailsProfileById(string Id)
        {
            
                var getById = await _manager.PermitDetailsProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>()
                    {
                        Data = _mapper.Map<dto.PermitDetailsProfile.DTOPermitDetailsProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }

        public async Task<wap.Response<dto.PermitDetailsProfile.DTOPermitNumber>> GetPermitNumberProfileById(string Id)
        {
            
                var getById = await _manager.PermitDetailsProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.PermitDetailsProfile.DTOPermitNumber>()
                    {
                        Data = _mapper.Map<dto.PermitDetailsProfile.DTOPermitNumber>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.PermitDetailsProfile.DTOPermitNumber>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
        }
        public async Task<wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>> UpdatePermitDetailsProfile(dto.PermitDetailsProfile.DTOPermitDetailsProfile body)
        {
            
                var PermitDetailsProfile = await _manager.PermitDetailsProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.PermitDetailsProfile>(body);
                _manager.PermitDetailsProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>();
            
        }


        public async Task<wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>> AddPermitDetailsProfile(dto.PermitDetailsProfile.DTOPermitDetailsProfile body)
        {
            
                var Data = _mapper.Map<ent.PermitDetailsProfile>(body);
                await _manager.PermitDetailsProfile.AddAsync(Data);
                return new wap.Response<dto.PermitDetailsProfile.DTOPermitDetailsProfile>();
          
        }
    }
}
