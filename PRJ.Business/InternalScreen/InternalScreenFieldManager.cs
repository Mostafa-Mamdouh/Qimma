using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.InternalScreen;
using PRJ.GlobalComponents.Encryption;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.ScreenField
{
    public class InternalScreenFieldManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public InternalScreenFieldManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.InternalScreen.DTOInternalScreenField>>> GetScreenFieldsByScreenId(string Id)
        {
            
                var screenId = int.Parse(EncryptQueryURL.Decrypt(Id.Replace(" ", "+")));
                var screenFields = await _manager.InternalScreenField.GetByQuery(x=>x.ScreenId==screenId).Include(x=>x.LookupSet).Include(x=>x.ListOfValue).Include(x=>x.Screen).ToListAsync();
                if (screenFields!=null && screenFields.Any())
                {
                    return new wap.Response<List<dto.InternalScreen.DTOInternalScreenField>>()
                    {
                        Data = _mapper.Map<List<dto.InternalScreen.DTOInternalScreenField>>(screenFields)
                    };
                }
                else
                {
                    return new wap.Response<List<dto.InternalScreen.DTOInternalScreenField>>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<DTOInternalScreenField>> UpdateScreenField(dto.InternalScreen.DTOInternalScreenField body)
        {
            
                var ScreenField = await _manager.InternalScreenField.GetEncryptByIdAsync(body.Id);
                if (ScreenField != null)
                {
                    ScreenField.FieldDescEn = body.FieldDescEn;
                    ScreenField.FieldDescAr = body.FieldDescAr;
                    ScreenField.FieldFormat = body.FieldFormat;
                    ScreenField.FieldType = body.FieldType;
                    ScreenField.LookupSetId = body.LookupSetId;
                    ScreenField.LovId = body.LovId;
                }
                _manager.InternalScreenField.Update(ScreenField);
               await _manager.CompleteAsync();
                return new wap.Response<DTOInternalScreenField>() { Data = _mapper.Map<dto.InternalScreen.DTOInternalScreenField>(ScreenField) };

            
            
        }

        public async Task<wap.Response<DTOInternalScreenField>> AddScreenField(dto.InternalScreen.DTOInternalScreenField body)
        {
            
                var Data = _mapper.Map<ent.InternalScreenField>(body);
                Data.ScreenId = int.Parse(EncryptQueryURL.Decrypt(body.ScreenIdValue.ToString()));

                await _manager.InternalScreenField.AddAsync(Data);
                await _manager.CompleteAsync();

                return new wap.Response<DTOInternalScreenField>() { Data = _mapper.Map<dto.InternalScreen.DTOInternalScreenField>(Data) };
            
           
        }

        public async Task<wap.Response<bool>> DeleteScreenField(string Id)
        {
            
                var getById = await _manager.InternalScreenField.GetEncryptByIdAsync(Id);

                await _manager.InternalScreenField.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            
            
        }

    }
}
