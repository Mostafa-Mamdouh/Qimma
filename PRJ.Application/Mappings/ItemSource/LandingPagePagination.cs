using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Application.DTOs.LandingPage_Pagination;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class LandingPagePagination : Profile
    {
        public LandingPagePagination()
        {

            CreateMap<TrnItemSource, DTOlandingPagepagination>()
                      .ForMember(
                     dest => dest.Id,
                     opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TrnSourceId.ToString())))

                     .ForMember(
           dest => dest.FacilityEn,
           opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameEn.ToString()))
                     .ForMember(
           dest => dest.FacilityAr,
           opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameAr.ToString()))
                     .ForMember(
           dest => dest.LicenseNumber,
           opt => opt.MapFrom(src => src.LicenseProfile.LicenseCode.ToString()))
                     .ForMember(
           dest => dest.PermitNumber,
           opt => opt.MapFrom(src => src.PermitDetailsProfile.PermitNumber.ToString()))
                     .ForMember(
           dest => dest.TransactionTypeEn,
           opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn.ToString()))
                     .ForMember(
           dest => dest.TransactionTypeAr,
           opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv.ToString()))
                       .ForMember(
           dest => dest.SourceTypeEn,
           opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameEn.ToString()))
                     .ForMember(
           dest => dest.SourceTypeAr,
           opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameAr.ToString()))
                     .ForMember(
           dest => dest.ManufacturerSerialNo,
           opt => opt.MapFrom(src => src.ManufacturerSerialNo.ToString()))
                     .ForMember(
           dest => dest.SourceStatus,
           opt => opt.MapFrom(src => src.TransactionsHeader.TrnStatus.ToString()))
                     .ForMember(
           dest => dest.TransactionType,
           opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeId.ToString()))
                     .ForMember(
           dest => dest.StatusEn,
           opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
                     .ForMember(
           dest => dest.StatusAr,
           opt => opt.MapFrom(src => src.StatusLookup.DisplayNameAr))
               .ForMember(
               dest => dest.currentActivity,
               opt => opt.MapFrom(src => src.TransactionsHeader.TrnStatus == 2 ? (src.SourceType == 106 ? src.SourceActivity : src.TrnItemSourceRadionuclides.Select(x => x.InitialActivity * float.Parse(x.InitialActivityUnitLookup.ExtraData1) * Math.Exp((double)(-0.693 * (DateTime.Now.Subtract(x.InitialActivityDate.Value).TotalDays) / (float.Parse(x.Radionuclides.HalfLifeUnit.ExtraData1) * x.Radionuclides.HalfLife)))).Max()) : 0))
                     .ForMember(
           dest => dest.RadionuclideName,
           opt => opt.MapFrom(src => src.TrnItemSourceRadionuclides))
                     .ForMember(
           dest => dest.SourceId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ItemSourceProfile.SourceId.ToString())));


            CreateMap<TrnItemSourceRadionuclides, DTOLandingPageRadionuclides>()
                 .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Radionuclides.RadionuclideId.ToString())))
                 .ForMember(
            dest => dest.isotope,
            opt => opt.MapFrom(src => src.Radionuclides.Isotop.ToString()));

        }
    }
}
