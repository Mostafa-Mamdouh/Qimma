using AutoMapper;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;
using enc = PRJ.GlobalComponents.Encryption;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using PRJ.Application.Warppers;
using System.Linq;
using PRJ.Domain.Entities;
using PRJ.Application.DTOs.Workers;
namespace PRJ.Business.Workers
{
    public class WorkersDosimetersManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public WorkersDosimetersManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<bool>> Add(dto.Workers.DTOWorkersDosimeters DTOWorkersDosimeters)
        {
         
                var _id = enc.EncryptQueryURL.Decrypt(DTOWorkersDosimeters.Id);
                DTOWorkersDosimeters.Id = _id;
                var LineNum = _manager.WorkersDosimeters.GetByQuery(_ => _.Id == int.Parse(_id)).Count();
                DTOWorkersDosimeters.LineNum = LineNum + 1;
                var data = _mapper.Map<ent.WorkersDosimeters>(DTOWorkersDosimeters);

                var oldData = await _manager.WorkersDosimeters.GetAllAsync();
                for (int z = 0; z < oldData.Count; z++)
                {
                    oldData[z].ActiveFlag = false;
                    _manager.WorkersDosimeters.Update(oldData[z]);
                }
                return new wap.Response<bool>()
                {
                    Data = await _manager.WorkersDosimeters.AddAsync(data)
                };
        }

        public async Task<wap.Response<List<dto.Workers.DTOWorkersDosimeters>>> GetAll()
        {
            
                return new wap.Response<List<dto.Workers.DTOWorkersDosimeters>>()
                {
                    Data = _mapper.Map<List<dto.Workers.DTOWorkersDosimeters>>(await _manager.WorkersDosimeters.GetAllAsync())
                };
           
        }
        public async Task<wap.Response<List<dto.Workers.DTOWorkersDosimeters>>> GetById(string Id)
        {
            
                var _id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));
                var data = _manager.WorkersDosimeters.GetByQuery(_ => _.Id == _id).ToList();

                return new wap.Response<List<dto.Workers.DTOWorkersDosimeters>>()
                {
                    Data = _mapper.Map<List<dto.Workers.DTOWorkersDosimeters>>(data)
                };
            
           
        }
    }

}

