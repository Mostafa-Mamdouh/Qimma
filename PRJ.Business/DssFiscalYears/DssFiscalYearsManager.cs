using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using System.Reflection;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;
using enc = PRJ.GlobalComponents.Encryption;

namespace PRJ.Business.DssFiscalYears
{
    public class DssFiscalYearsManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public DssFiscalYearsManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<wap.Response<List<ent.DssFiscalYears>>> GetAll()
        {
            return new wap.Response<List<ent.DssFiscalYears>>()
            {
                Data = await _manager.DssFiscalYears.GetAllAsync()
            };
        }
    }
}
