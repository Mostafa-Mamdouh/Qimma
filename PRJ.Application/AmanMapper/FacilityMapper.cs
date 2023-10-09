using AutoMapper;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.Mappings
{
    public class FacilityMapper : Profile
    {
        public FacilityMapper()
        {
            CreateMap<dto.DTOFacilityProfile, ent.FacilityProfile>()
                .ForMember(
                dest => dest.FacilityId,
                opt => opt.MapFrom(src => 0)
                )
                .ForMember(
                dest => dest.EntityId,
                opt => opt.MapFrom(src => 0)
                )
                .ForMember(
                dest => dest.AmanId,
                opt => opt.MapFrom(src => src.EntityID)
                )
                .ForMember(
                dest => dest.AmanFacilityId,
                opt => opt.MapFrom(src => src.FacilityId)
                )
                .ForMember(
                dest => dest.FacilityNameEn,
                opt => opt.MapFrom(src => src.FacilityNameEn)
                )
                .ForMember(
                dest => dest.FacilityNameAr,
                opt => opt.MapFrom(src => src.FacilityNameAr)
                )
                .ForMember(
                dest => dest.FacilityCode,
                opt => opt.MapFrom(src => src.FacilityCode)
                )
                .ForMember(
                dest => dest.AmanInsertedOn,
                opt => opt.MapFrom(src => src.AmanInsertedOn))
                .ForMember(
                dest => dest.Province,
                opt => opt.MapFrom(src => src.Province.Id))
                .ForMember(
                dest => dest.City,
                opt => opt.MapFrom(src => src.City))
                .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => src.Location)
                );
        }
    }
}
