using AutoMapper;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.AmanMapper
{
    public class ServiceEntryFeesMapper : Profile
    {
       public ServiceEntryFeesMapper()
        {
            CreateMap<dto.Billing.DTOServiceEntryFees, ent.BillingServiceTrn.ServiceEntryFees>()
           .ForMember(
                 dest => dest.CustomerId,
                 opt => opt.MapFrom(src => src.CustomerId)
                 )
             .ForMember(
                 dest => dest.CustomerNameAr,
                 opt => opt.MapFrom(src => src.CustomerNameAr)
                 )
             .ForMember(
                 dest => dest.CustomerNameEn,
                 opt => opt.MapFrom(src => src.CustomerNameEn)
                 );
        }
    }
}
