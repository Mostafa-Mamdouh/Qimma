using AutoMapper;
using PRJ.Application.DTOs;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business
{
    public class TransactionTypeMasterManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public TransactionTypeMasterManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;
            _mapper = mapper;
        }

        public async Task<wap.Response<List<DTOTransactionTypeMaster>>> GetAll()
        {
            
                var Screen = await _manager.InternalScreen.GetAllAsync();

                return new wap.Response<List<DTOTransactionTypeMaster>>()
                {
                    Data = _mapper.Map<List<DTOTransactionTypeMaster>>(Screen)
                };
            
            
        }

        public async Task<wap.Response<DTOTransactionTypeMaster>> GetScreenById(string Id)
        {
            
            var getById = await _manager.InternalScreen.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<DTOTransactionTypeMaster>()
                    {
                        Data = _mapper.Map<DTOTransactionTypeMaster>(getById)
                    };
                }
                else
                {
                    return new wap.Response<DTOTransactionTypeMaster>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }



    }
}
