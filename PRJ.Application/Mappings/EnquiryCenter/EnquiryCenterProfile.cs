using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class EnquiryCenterProfile : Profile
    {
        public EnquiryCenterProfile()
        {
            CreateMap<TrnItemSource, DTOEnquiryCenter>()
            .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TrnSourceId.ToString())))
           .ForMember(
            dest => dest.TransactionDate,
            opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(
            dest => dest.SourceTypeAr,
            opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameAr))
            .ForMember(
            dest => dest.SourceTypeEn,
            opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameEn))
            .ForMember(
             dest => dest.ManufacturerEn,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameEn))
            .ForMember(
            dest => dest.ManufacturerAr,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameAr))
            .ForMember(
             dest => dest.StatusEn,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
            .ForMember(
            dest => dest.StatusAr,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameAr))
            .ForMember(
            dest => dest.ManufacturerSerialNo,
            opt => opt.MapFrom(src => src.ManufacturerSerialNo))
           .ForMember(
            dest => dest.FacilitySourceID,
            opt => opt.MapFrom(src => src.FacilitySerialNo))
           .ForMember(
            dest => dest.RadionuclideName,
            opt => opt.MapFrom(src => src.TrnItemSourceRadionuclides.Any() ? src.TrnItemSourceRadionuclides.FirstOrDefault().Radionuclides.Isotop : ""))
             .ForMember(
            dest => dest.FacilitySourceID,
            opt => opt.MapFrom(src => src.FacilitySerialNo));
        }
    }
}
