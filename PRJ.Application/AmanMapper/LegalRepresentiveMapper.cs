using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.AmanMapper
{
    public class LegalRepresentiveMapper : Profile
    {
        public LegalRepresentiveMapper()
        {
            CreateMap<dto.DTOLegalRepresentative, ent.LegalRepresentativesProfile>()

                  .ForMember(
                   dest => dest.AmanId,
                   opt => opt.MapFrom(src => src.LegalRepID)
                   )
             .ForMember(
                 dest => dest.Title,
                 opt => opt.MapFrom(src => src.Title))
             .ForMember(
                 dest => dest.LegalRepresentativesNameEn,
                 opt => opt.MapFrom(src => src.FullName))
             .ForMember(
                 dest => dest.LegalRepresentativesNameAr,
                 opt => opt.MapFrom(src => src.FullName))
             .ForMember(
                 dest => dest.PhoneNo,
                 opt => opt.MapFrom(src => src.Telephone))
             .ForMember(
                 dest => dest.EmailId,
                 opt => opt.MapFrom(src => src.Email))
             .ForMember(
                 dest => dest.CurrentFacilities,
                 opt => opt.MapFrom(src => src.CurrentFacilities))
             .ForMember(
                 dest => dest.Note,
                 opt => opt.MapFrom(src => src.Note))
             .ForMember(
                 dest => dest.Status,
                 opt => opt.MapFrom(src => src.IsActive))
             .ForMember(
                 dest => dest.AmanInsertedOn,
                 opt => opt.MapFrom(src => src.AmanInsertedOn))
             .ForMember(
                   dest => dest.UserIdentity,
                   opt => opt.MapFrom(src => src.UserIdentity)
                   )
             .ForMember(
                   dest => dest.UserType,
                   opt => opt.MapFrom(src => src.UserType.Id)
                   )
             ;
        }
    }
}
