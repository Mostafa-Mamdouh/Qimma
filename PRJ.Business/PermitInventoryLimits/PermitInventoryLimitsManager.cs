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

namespace PRJ.Business.PermitInventoryLimits
{
    public class PermitInventoryLimitsManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public PermitInventoryLimitsManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.PermitInventoryLimits.DTOPermitInventoryLimits>>> GetAll()
        {
            
                var PermitInventoryLimits = await _manager.PermitInventoryLimits.GetAllAsync();

                return new wap.Response<List<dto.PermitInventoryLimits.DTOPermitInventoryLimits>>()
                {
                    Data = _mapper.Map<List<dto.PermitInventoryLimits.DTOPermitInventoryLimits>>(PermitInventoryLimits)
                };
           
        }


        public async Task<wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>> GetPermitInventoryLimitsById(string Id)
        {
            
                var getById = await _manager.PermitInventoryLimits.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>()
                    {
                        Data = _mapper.Map<dto.PermitInventoryLimits.DTOPermitInventoryLimits>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }

        public async Task<wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>> UpdatePermitInventoryLimits(dto.PermitInventoryLimits.DTOPermitInventoryLimits body)
        {
            
                var PermitInventoryLimits = await _manager.PermitInventoryLimits.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.AmanIntegrationEntities.PermitInventoryLimits>(body);
                _manager.PermitInventoryLimits.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>();
           
        }


        public async Task<wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>> AddPermitInventoryLimits(dto.PermitInventoryLimits.DTOPermitInventoryLimits body)
        {
            
                var Data = _mapper.Map<ent.AmanIntegrationEntities.PermitInventoryLimits>(body);
                await _manager.PermitInventoryLimits.AddAsync(Data);
                return new wap.Response<dto.PermitInventoryLimits.DTOPermitInventoryLimits>();
            
        }
    }
}
