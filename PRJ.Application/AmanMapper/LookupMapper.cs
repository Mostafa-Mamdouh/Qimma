using AutoMapper;
using PRJ.Application.AmanDTOs;
using PRJ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanMapper
{
    public class LookupMapper : Profile
    {
        public LookupMapper()
        {
            CreateMap<AmanLookupDto, LookupSetTerm>()
           .ForMember(
            dest => dest.AmanId,
            opt => opt.MapFrom(src => src.Id))
            .ForMember(
            dest => dest.DisplayNameAr,
            opt => opt.MapFrom(src => src.NameAr))
                        .ForMember(
            dest => dest.DisplayNameEn,
            opt => opt.MapFrom(src => src.NameEn))
           ;
        }
    }
}
