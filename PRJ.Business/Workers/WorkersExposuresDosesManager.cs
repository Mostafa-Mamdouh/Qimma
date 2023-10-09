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
    public class WorkersExposuresDosesManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public WorkersExposuresDosesManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<bool>> Add(dto.Workers.DTOWorkersExposuresDoses DTOWorkersExposuresDoses)
        {
            
                var _id = int.Parse(enc.EncryptQueryURL.Decrypt(DTOWorkersExposuresDoses.Id));
                var _DosimeterType = enc.EncryptQueryURL.Decrypt(DTOWorkersExposuresDoses.DosimeterType);

                var LineNum = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == _id).Count();
                DTOWorkersExposuresDoses.LineNum = LineNum + 1;
                DTOWorkersExposuresDoses.Id = enc.EncryptQueryURL.Decrypt(DTOWorkersExposuresDoses.Id);
                DTOWorkersExposuresDoses.DosimeterType = _DosimeterType;
                var data = _mapper.Map<ent.WorkersExposuresDoses>(DTOWorkersExposuresDoses);
                data.Id = _id;
                data.DosimeterType = int.Parse(_DosimeterType);
                return new wap.Response<bool>()
                {
                    Data = await _manager.WorkersExposuresDoses.AddAsync(data)
                };
           
        }

        public async Task<wap.Response<List<dto.Workers.DTOWorkersExposuresDoses>>> GetAll()
        {
            
                return new wap.Response<List<dto.Workers.DTOWorkersExposuresDoses>>()
                {
                    Data = _mapper.Map<List<dto.Workers.DTOWorkersExposuresDoses>>(await _manager.WorkersExposuresDoses.GetAllAsync())
                };
            
        }
        public async Task<wap.Response<List<dto.Workers.DTOWorkersExposuresDoses>>> GetById(string Id)
        {
           
                var _id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));
                var data = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == _id).ToList();

                return new wap.Response<List<dto.Workers.DTOWorkersExposuresDoses>>()
                {
                    Data = _mapper.Map<List<dto.Workers.DTOWorkersExposuresDoses>>(data)
                };
           
        }
        public async Task<wap.Response<dto.Workers.DTOWorkersExposuresDoses>> GetLastReading(string Id)
        {
            var _id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));
            var data = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == _id).OrderBy(_ => _.LineNum).LastOrDefault() ;
            return new wap.Response<dto.Workers.DTOWorkersExposuresDoses>()
            {
                Data = _mapper.Map<dto.Workers.DTOWorkersExposuresDoses>(data)
            };

        }
        public async Task<wap.Response<List<WorkerIncludeProblem>>> AddReadingFromMassPage(dto.Workers.DTOMassUpdate_2[] workers, int year, string quarter)
        {
            List<dto.Workers.WorkerIncludeProblem> listWorkerIncludeProblem = new List<WorkerIncludeProblem>();

            foreach (var worker in workers)
            {
              var _id = int.Parse(enc.EncryptQueryURL.Decrypt(worker.WorkerId));
              var old_data = _manager.WorkersExposuresDoses.GetByQuery(_ => _.FiscalYear == year && _.QuarterCode == quarter && _.Id == _id).FirstOrDefault();
              if(old_data != null)
                {
                    var obj1 = new WorkerIncludeProblem();
                    var workerDate = _manager.Workers.GetById(_id).WorkerNameEn;
                    obj1.userId = _id;
                    obj1.problem = "This user has a previous reading recorded in the same year and quarter";
                    listWorkerIncludeProblem.Add(obj1);
                }
              else if(worker.DeepDose != 0)
                {
                    var LineNum = _manager.WorkersExposuresDoses.GetByQuery(_ => _.Id == _id).Count();
                    var data = new ent.WorkersExposuresDoses();
                    data.Id = _id;
                    data.FiscalYear = year;
                    data.QuarterCode = quarter;
                    data.LineNum = LineNum + 1;
                    data.DosimeterType = 74;
                    data.DeepDose = worker.DeepDose;
                    await _manager.WorkersExposuresDoses.AddAsync(data);
                }
              
            }

            return new wap.Response<List<WorkerIncludeProblem>>()
            {
                Data = listWorkerIncludeProblem
            };

        }
        public async Task<wap.Response<string>> GetQuarterByMonth(string month)
        {

            var data = _manager.DssQuarterSetupDetails.GetByQuery(_ => _.PeriodNum == month).Select(_ => _.QuarterCode).FirstOrDefault(); ;

            return new wap.Response<string>()
            {
                Data = data
            };

        }
        

    }

}

