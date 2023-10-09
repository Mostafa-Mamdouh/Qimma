using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.InternalScreen;
using PRJ.Application.Warppers;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using PRJ.Repository;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.ScreenField
{
    public class InternalFieldPermissionManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public InternalFieldPermissionManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.InternalScreen.DTOInternalFieldPermission>>> GetByFieldId(int id,string roleId)
        {
            var role = int.Parse( EncryptQueryURL.Decrypt(roleId));
            
                var permissions = await _manager.InternalFieldPermission.GetByQuery(x => x.FieldId == id&& x.RoleId== role).ToListAsync();

                if (permissions != null)
                {
                    return new wap.Response<List<dto.InternalScreen.DTOInternalFieldPermission>>()
                    {
                        Data = _mapper.Map<List<dto.InternalScreen.DTOInternalFieldPermission>>(permissions)
                    };
                }
                else
                {
                    return new wap.Response<List<dto.InternalScreen.DTOInternalFieldPermission>>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }


        public async Task<wap.Response<bool>> SaveFieldsPermission(List<dto.InternalScreen.DTOInternalFieldPermission> body)
        {
            
                if (body.Any() && !body.Any(x=>x.AttributeId==0))
                {
                    var savedPermissions = await _manager.InternalFieldPermission.GetByQuery(x => x.FieldId == body[0].FieldId).ToListAsync();
                    if (savedPermissions != null && savedPermissions.Any())
                    {
                        var deletedPermissions = savedPermissions.Where(p => body.All(p2 => p2.FieldId != p.FieldId && p2.AttributeId != p.AttributeId)).ToList();
                        foreach (var item in deletedPermissions)
                        {
                            await _manager.InternalFieldPermission.DeleteAsync(item);
                        }
                        var updatedPermissions = savedPermissions.Where(p => body.Any(p2 => p2.FieldId == p.FieldId && p2.AttributeId == p.AttributeId)).ToList();
                        foreach (var item in updatedPermissions)
                        {
                            var updatedFieldPermission = _mapper.Map<InternalFieldPermission>(item);
                            _manager.InternalFieldPermission.Update(updatedFieldPermission);
                        }

                        var AddedPermissions = body.Where(p => p.FieldPermissionId == 0).ToList();
                        foreach (var item in AddedPermissions)
                        {
                            var addedFieldPermission = _mapper.Map<InternalFieldPermission>(item);
                            await _manager.InternalFieldPermission.AddAsync(addedFieldPermission);
                        }
                    }
                    else
                    {
                        foreach (var item in body)
                        {
                            var addedFieldPermission = _mapper.Map<InternalFieldPermission>(item);
                            await _manager.InternalFieldPermission.AddAsync(addedFieldPermission);
                        }
                    }
               
                }
                else
                {
                    if (body.Any() && body.First().FieldId != 0)
                    {
                        var permissions = await _manager.InternalFieldPermission.GetByQuery(x => x.FieldId == body.First().FieldId).ToListAsync();
                        foreach (var item in permissions)
                        {
                            await _manager.InternalFieldPermission.DeleteAsync(item);
                        }

                    }

                }

                _manager.Complete();
                return new wap.Response<bool>() { Data = true };

            
            
        }


    }
}
