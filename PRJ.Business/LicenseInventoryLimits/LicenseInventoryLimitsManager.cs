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

namespace PRJ.Business.LicenseInventoryLimits
{
    public class LicenseInventoryLimitsManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public LicenseInventoryLimitsManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>>> GetAll()
        {
            
                var LicenseInventoryLimits = await _manager.LicenseInventoryLimits.GetAllAsync();

                return new wap.Response<List<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>>()
                {
                    Data = _mapper.Map<List<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>>(LicenseInventoryLimits)
                };
            
          
        }


        public async Task<wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>> GetLicenseInventoryLimitsById(string Id)
        {
            
                var getById = await _manager.LicenseInventoryLimits.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>()
                    {
                        Data = _mapper.Map<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
          
        }

        public async Task<wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>> UpdateLicenseInventoryLimits(dto.LicenseInventoryLimits.DTOLicenseInventoryLimits body)
        {
            
                var LicenseInventoryLimits = await _manager.LicenseInventoryLimits.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.AmanIntegrationEntities.LicenseInventoryLimits>(body);
                _manager.LicenseInventoryLimits.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>();
            
           
        }


        public async Task<wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>> AddLicenseInventoryLimits(dto.LicenseInventoryLimits.DTOLicenseInventoryLimits body)
        {
           
                var Data = _mapper.Map<ent.AmanIntegrationEntities.LicenseInventoryLimits>(body);
                await _manager.LicenseInventoryLimits.AddAsync(Data);
                return new wap.Response<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits>();
            
          
        }
    }
}
