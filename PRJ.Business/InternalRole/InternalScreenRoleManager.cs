using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.InternalScreenRole
{
    public class InternalScreenRoleManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public InternalScreenRoleManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;
            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.InternalRole.DTOInternalScreenRole>>> GetInternalScreens(string Id)
        {
            
                var roleId = int.Parse(EncryptQueryURL.Decrypt(Id.Replace(" ", "+")));
                var roleScreens = await _manager.InternalScreen.GetAll().Include(r=>r.InternalScreenRoles.Where(x=>x.RoleId== roleId)).ThenInclude(c=>c.Role).ToListAsync();

                if (roleScreens!=null && roleScreens.Any())
                {
                    return new wap.Response<List<dto.InternalRole.DTOInternalScreenRole>>()
                    {
                        Data = _mapper.Map<List<dto.InternalRole.DTOInternalScreenRole>>(roleScreens)
                    };
                }
                else
                {
                    return new wap.Response<List<dto.InternalRole.DTOInternalScreenRole>>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<bool>> UpdateInternalRoleScreens(List<dto.InternalRole.DTOInternalScreenRole> body)
        {
            
                foreach (var screenRole in body)
                {
                    if (screenRole.ScreenRoleId==null|| screenRole.ScreenRoleId==0)
                    {
                        var roleId = int.Parse(EncryptQueryURL.Decrypt(screenRole.RoleIdValue.Replace(" ", "+")));
                        screenRole.RoleId = roleId;
                        var addedData = _mapper.Map<ent.InternalScreenRole>(screenRole);
                        await _manager.InternalScreenRole.AddAsync(addedData);
                    }
                    else
                    {
                        var updatedData = await _manager.InternalScreenRole.GetByIdAsync(screenRole.ScreenRoleId);
                        if (updatedData != null)
                        {
                            updatedData.Query = screenRole.Query;
                            updatedData.Insert = screenRole.Insert;
                            updatedData.Update = screenRole.Update;
                            updatedData.Delete = screenRole.Delete;
                            updatedData.ScreenOrder = (int)(screenRole.ScreenOrder.HasValue?screenRole.ScreenOrder:0);
                        }
                        _manager.InternalScreenRole.Update(updatedData);

                    }

                }
                _manager.Complete();

                return new wap.Response<bool> () { Data = true };

            
           
        }

   

    }
}
