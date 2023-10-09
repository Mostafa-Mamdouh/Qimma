using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using System.Text;

namespace PRJ.Application.Mappings
{
    public class ActionCenterProfile : Profile
    {
        public ActionCenterProfile()
        {

            CreateMap<ItemSourceProfile, DTOActionCenter>()

            .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SourceId.ToString())))
            .ForMember(
            dest => dest.TransactionTypeEn,
            opt => opt.MapFrom(src => src.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn))
            .ForMember(
            dest => dest.TransactionTypeAr,
            opt => opt.MapFrom(src => src.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv))
            .ForMember(
            dest => dest.EntityEn,
            opt => opt.MapFrom(src => src.EntityProfile.EntityNameEn))
            .ForMember(
            dest => dest.EntityAr,
            opt => opt.MapFrom(src => src.EntityProfile.EntityNameAr))
            .ForMember(
            dest => dest.FacilityEn,
            opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameEn))
            .ForMember(
            dest => dest.FacilityAr,
            opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameAr))
            .ForMember(
            dest => dest.LicenseEn,
            opt => opt.MapFrom(src => src.LicenseProfile.LicenseDescEn))
            .ForMember(
            dest => dest.LicenseAr,
            opt => opt.MapFrom(src => src.LicenseProfile.LicenseDescAr))
            .ForMember(
            dest => dest.PermitNumber,
            opt => opt.MapFrom(src => src.PermitDetailsProfile.PermitNumber))
           .ForMember(
            dest => dest.TransactionDate,
            opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(
            dest => dest.sourceType,
            opt => opt.MapFrom(src => int.Parse(src.SourceTypeLookup.Value)))
            .ForMember(
            dest => dest.SourceTypeAr,
            opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameAr))
            .ForMember(
            dest => dest.SourceTypeEn,
            opt => opt.MapFrom(src => src.SourceTypeLookup.DisplayNameEn))
            .ForMember(
            dest => dest.TransactionStatusEn,
            opt => opt.MapFrom(src => src.ItemSourceStatusHistories.OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn))
            .ForMember(
            dest => dest.TransactionStatusAr,
            opt => opt.MapFrom(src => src.ItemSourceStatusHistories.OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameAr))
            .ForMember(
            dest => dest.TransactionId,
            opt => opt.MapFrom(src => src.TrnSourceId))

            .ForMember(
            dest => dest.CreateUser,
            opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(
            dest => dest.ModifiedUser,
            opt => opt.MapFrom(src => src.ModifiedBy))
            .ForMember(
            dest => dest.ConfirmedUser,
            opt => opt.MapFrom(src => src.TrnItemSource.TransactionsHeader.ConfirmedUser))
            .ForMember(
            dest => dest.CreateDate,
            opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(
            dest => dest.ModifiedDate,
            opt => opt.MapFrom(src => src.ModifiedOn))
            .ForMember(
            dest => dest.ConfirmedDate,
            opt => opt.MapFrom(src => src.TrnItemSource.TransactionsHeader.ConfirmedDate))
            .ForMember(
               dest => dest.currentActivity,
               opt => opt.MapFrom(src => src.SourceType == 106 ? src.SourceActivity : src.ItemSourceRadionulcides.Select(x => x.InitialActivity * float.Parse(x.InitialActivityUnitLookup.ExtraData1) * Math.Exp((double)(-0.693 * (DateTime.Now.Subtract(x.InitialActivityDate.Value).TotalDays) / (float.Parse(x.Radionuclides.HalfLifeUnit.ExtraData1) * x.Radionuclides.HalfLife)))).Max()))
            //.ForMember(
            //  dest => dest.currentActivity,
            //  opt => opt.MapFrom(src => src.SourceType == 106 ? src.SourceActivity : src.TrnItemSource.TrnItemSourceRadionuclides.Select(x => x.InitialActivity * float.Parse(x.InitialActivityUnitLookup.ExtraData1) * Math.Exp((double)(-0.693 * (DateTime.Now.Subtract(x.InitialActivityDate.Value).TotalDays) / (float.Parse(x.Radionuclides.HalfLifeUnit.ExtraData1) * x.Radionuclides.HalfLife)))).Max()))

            .ForMember(
             dest => dest.ManufacturerEn,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameEn))
            .ForMember(
            dest => dest.ManufacturerAr,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameAr))
            .ForMember(
            dest => dest.ManufacturerSerialNo,
            opt => opt.MapFrom(src => src.ManufacturerSerialNo))
            .ForMember(
             dest => dest.ManufacturerCountryEn,
            opt => opt.MapFrom(src => src.ManufacturerCountryLookup.DisplayNameEn))
            .ForMember(
            dest => dest.ManufacturerCountryAr,
            opt => opt.MapFrom(src => src.ManufacturerCountryLookup.DisplayNameAr))
           .ForMember(
            dest => dest.FacilitySourceID,
            opt => opt.MapFrom(src => src.FacilitySerialNo))
             .ForMember(
            dest => dest.FacilitySourceID,
            opt => opt.MapFrom(src => src.FacilitySerialNo))
                      .ForMember(
            dest => dest.RadionuclideName,
            opt => opt.MapFrom(src => src.ItemSourceRadionulcides.FirstOrDefault().Radionuclides.Isotop))
            .ForMember(
            dest => dest.ItemSourceStatusHistories,
            opt => opt.MapFrom(src => src.ItemSourceStatusHistories))
                 .ForMember(
            dest => dest.ItemSourceMsgHistories,
            opt => opt.MapFrom(src => src.ItemSourceMsgs))
                 .ForMember(
            dest => dest.currentSourceStatusAr,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameAr))
                 .ForMember(
            dest => dest.currentSourceStatusEn,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
                 .ForMember(
            dest => dest.AssociatedEquipmentAr,
            opt => opt.MapFrom(src => src.AssociatedEquipmentLookup.DisplayNameAr))
                 .ForMember(
            dest => dest.AssociatedEquipmentEn,
            opt => opt.MapFrom(src => src.AssociatedEquipmentLookup.DisplayNameEn))
                 .ForMember(
           dest => dest.ShieldAttachments,
           opt => opt.MapFrom(src => src.ItemSourceFiles.Where(x => x.ForShield == true)))
                   .ForMember(
           dest => dest.TransactionAttactcments,
           opt => opt.MapFrom(src => src.ItemSourceFiles.Where(x => x.ForShield == null)))
                        .ForMember(
           dest => dest.shieldCode,
           opt => opt.MapFrom(src => src.ShieldNuclearMaterial.NrrcId));



            CreateMap<ItemSourceFiles, DTOGetTransactionAttachments>()
             .ForMember(
             dest => dest.FileBytes,
             src => src.MapFrom(x => Encoding.Default.GetString(x.FileBytes))
             );











            CreateMap<TrnItemSource, DTOActionCenter>()

           .ForMember(
           dest => dest.SourceId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TrnSourceId.ToString())))
           .ForMember(
           dest => dest.TransactionTypeEn,
           opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn))
           .ForMember(
           dest => dest.TransactionTypeAr,
           opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv))
           .ForMember(
           dest => dest.EntityEn,
           opt => opt.MapFrom(src => src.EntityProfile.EntityNameEn))
           .ForMember(
           dest => dest.EntityAr,
           opt => opt.MapFrom(src => src.EntityProfile.EntityNameAr))
           .ForMember(
           dest => dest.FacilityEn,
           opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameEn))
           .ForMember(
           dest => dest.FacilityAr,
           opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameAr))
           .ForMember(
           dest => dest.LicenseEn,
           opt => opt.MapFrom(src => src.LicenseProfile.LicenseDescEn))
           .ForMember(
           dest => dest.LicenseAr,
           opt => opt.MapFrom(src => src.LicenseProfile.LicenseDescAr))
           .ForMember(
           dest => dest.PermitNumber,
           opt => opt.MapFrom(src => src.PermitDetailsProfile.PermitNumber))
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
           dest => dest.TransactionId,
           opt => opt.MapFrom(src => src.TrnSourceId))
               .ForMember(
           dest => dest.TransactionType,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TransactionsHeader.TransactionTypeId.ToString())))
           .ForMember(
           dest => dest.CreateUser,
           opt => opt.MapFrom(src => src.CreatedBy))
           .ForMember(
           dest => dest.ModifiedUser,
           opt => opt.MapFrom(src => src.ModifiedBy))
           .ForMember(
           dest => dest.ConfirmedUser,
           opt => opt.MapFrom(src => src.TransactionsHeader.ConfirmedUser))
           .ForMember(
           dest => dest.CreateDate,
           opt => opt.MapFrom(src => src.CreatedOn))
           .ForMember(
           dest => dest.ModifiedDate,
           opt => opt.MapFrom(src => src.ModifiedOn))
           .ForMember(
           dest => dest.ConfirmedDate,
           opt => opt.MapFrom(src => src.TransactionsHeader.ConfirmedDate))

           .ForMember(
            dest => dest.ManufacturerEn,
           opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameEn))
           .ForMember(
           dest => dest.ManufacturerAr,
           opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameAr))
           .ForMember(
           dest => dest.ManufacturerSerialNo,
           opt => opt.MapFrom(src => src.ManufacturerSerialNo))
           .ForMember(
            dest => dest.ManufacturerCountryEn,
           opt => opt.MapFrom(src => src.ManufacturerCountryLookup.DisplayNameEn))
           .ForMember(
           dest => dest.ManufacturerCountryAr,
           opt => opt.MapFrom(src => src.ManufacturerCountryLookup.DisplayNameAr))
          .ForMember(
           dest => dest.FacilitySourceID,
           opt => opt.MapFrom(src => src.FacilitySerialNo))
            .ForMember(
           dest => dest.FacilitySourceID,
           opt => opt.MapFrom(src => src.FacilitySerialNo))
               .ForMember(
               dest => dest.currentActivity,
               opt => opt.MapFrom(src => src.TransactionsHeader.TrnStatus == 2 ?
               (src.SourceType == 106 ? src.SourceActivity : src.TrnItemSourceRadionuclides.Select
               (x => x.InitialActivity * float.Parse(x.InitialActivityUnitLookup.ExtraData1) *
               Math.Exp((double)(-0.693 * (DateTime.Now.Subtract(x.InitialActivityDate.Value).TotalDays) /
               (float.Parse(x.Radionuclides.HalfLifeUnit.ExtraData1) * x.Radionuclides.HalfLife)))).Max()) : 0))
                     .ForMember(
           dest => dest.RadionuclideName,
           opt => opt.MapFrom(src => src.TrnItemSourceRadionuclides.FirstOrDefault().Radionuclides.Isotop))
                .ForMember(
           dest => dest.currentSourceStatusAr,
           opt => opt.MapFrom(src => src.StatusLookup.DisplayNameAr))
                .ForMember(
           dest => dest.currentSourceStatusEn,
           opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
                .ForMember(
           dest => dest.AssociatedEquipmentAr,
           opt => opt.MapFrom(src => src.AssociatedEquipmentLookup.DisplayNameAr))

                .ForMember(
           dest => dest.AssociatedEquipmentEn,
           opt => opt.MapFrom(src => src.AssociatedEquipmentLookup.DisplayNameEn))

                .ForMember(
           dest => dest.ShieldAttachments,
           opt => opt.MapFrom(src => src.TransactionAttactcments.Where(x => x.ForShield == true)))
                            .ForMember(
           dest => dest.TransactionAttactcments,
           opt => opt.MapFrom(src => src.TransactionAttactcments.Where(x => x.ForShield == null)));

            CreateMap<TransactionAttachments, DTOGetTransactionAttachments>()
                .ForMember(
                dest => dest.FileBytes,
                src => src.MapFrom(x => Encoding.Default.GetString(x.FileBytes))
                );

        }
    }
}
