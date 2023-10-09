using AutoMapper;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.AmanMapper
{
    public class RsoMapper : Profile
    {
        public RsoMapper()
        {
            CreateMap<dto.DTORso, ent.SafetyResponsibleOfficersProfile>()
            .ForMember(
                  dest => dest.AmanId,
                  opt => opt.MapFrom(src => src.RSOID)
                  )
            .ForMember(
                dest => dest.CertificateNo,
                opt => opt.MapFrom(src => src.CertificateNo))
            .ForMember(
                dest => dest.SRONameEn,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.SRONameAr,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.NationalID,
                opt => opt.MapFrom(src => src.NationalId))
            .ForMember(
                dest => dest.PhoneNo,
                opt => opt.MapFrom(src => src.Phone))
            .ForMember(
                dest => dest.AmanInsertedOn,
                opt => opt.MapFrom(src => src.AmanInsertedOn))
            .ForMember(
                dest => dest.EmailId,
                opt => opt.MapFrom(src => src.Email));
        }
    } 
}
