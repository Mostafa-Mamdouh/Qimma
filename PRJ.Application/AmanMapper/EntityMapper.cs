using AutoMapper;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.Mappings
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<dto.DTOEntityProfile, ent.EntityProfile>()
              .ForMember(
                   dest => dest.EntityId,
                   opt => opt.MapFrom(src => 0)
                   )
             .ForMember(
                   dest => dest.AmanId,
                   opt => opt.MapFrom(src => src.EntityId.ToString())
                   )
             .ForMember(
                   dest => dest.EmailId,
                   opt => opt.MapFrom(src => src.Email)
                   )
             .ForMember(
                   dest => dest.AmanInsertedOn,
                   opt => opt.MapFrom(src => src.AmanInsertedOn)
                   )
             .ForMember(
                   dest => dest.EntityNameEn,
                   opt => opt.MapFrom(src => src.NameEn)
                   )
             .ForMember(
                   dest => dest.EntityNameAr,
                   opt => opt.MapFrom(src => src.NameAr)
                   )
             .ForMember(
                   dest => dest.GovernmentID,
                   opt => opt.MapFrom(src => src.EntityNumber)
                   )
                  .ForMember(
                   dest => dest.EntityType,
                   opt => opt.MapFrom(src => src.EntityType.Id)
                   )
             .ForMember(
                   dest => dest.MobileNo,
                   opt => opt.MapFrom(src => src.Telephone)
                   );



          

        }
    }
}