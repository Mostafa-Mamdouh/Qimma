using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using System.Text;

namespace PRJ.Application.Mappings
{
    public class TrnItemSourceProfile : Profile
    {
        public TrnItemSourceProfile()
        {

            CreateMap<DTOItemSourceEditor, TrnItemSource>()

            .ForMember(
            dest => dest.TrnSourceId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? null : EncryptQueryURL.Decrypt(src.Id.ToString())))
                      .ForMember(
            dest => dest.EntityId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Entity) ? null : EncryptQueryURL.Decrypt(src.Entity.ToString())))
                      .ForMember(
            dest => dest.FacilityId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Facility) ? null : EncryptQueryURL.Decrypt(src.Facility.ToString())))
                      .ForMember(
            dest => dest.LicenseId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LicenseNumber) ? null : EncryptQueryURL.Decrypt(src.LicenseNumber.ToString())))
                      .ForMember(
            dest => dest.PermitdetailsId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PermitNumber) ? null : EncryptQueryURL.Decrypt(src.PermitNumber.ToString())))
                      .ForMember(
            dest => dest.SourceType,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SourceType) ? null : EncryptQueryURL.Decrypt(src.SourceType.ToString())))
                      .ForMember(
            dest => dest.TransactionHeaderId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TransactionHeaderId) ? null : EncryptQueryURL.Decrypt(src.TransactionHeaderId.ToString())))
                      .ForMember(
            dest => dest.ManufacturerId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Manufacturer) ? null : EncryptQueryURL.Decrypt(src.Manufacturer.ToString())))
                      .ForMember(
            dest => dest.ManufacturerCountryId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ManufacturerCountry) ? null : EncryptQueryURL.Decrypt(src.ManufacturerCountry.ToString())))
                      .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Status) ? null : EncryptQueryURL.Decrypt(src.Status.ToString())))
                      .ForMember(
            dest => dest.FacilitySerialNo,
            opt => opt.MapFrom(src => src.FacilitySourceID))
                      .ForMember(
            dest => dest.AssociatedEquipment,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.AssociatedEquipment) ? null : EncryptQueryURL.Decrypt(src.AssociatedEquipment.ToString())))
                      .ForMember(
            dest => dest.ShieldNuclearMaterialCode,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ShieldNuclearMaterialCode) ? null : EncryptQueryURL.Decrypt(src.ShieldNuclearMaterialCode.ToString())))
                      .ForMember(
            dest => dest.InitialMass,
            opt => opt.MapFrom(src => src.InitialMass))
                      .ForMember(
            dest => dest.InitialMassUnit,
             opt => opt.MapFrom(src => !src.IsShieldDU ? null : EncryptQueryURL.Decrypt(src.InitialMassUnit.ToString())))
                      .ForMember(
            dest => dest.PhysicalForm,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PhysicalForm) ? null : EncryptQueryURL.Decrypt(src.PhysicalForm.ToString())))
                      .ForMember(
            dest => dest.Quantity,
            opt => opt.MapFrom(src => src.Quantity))
                        .ForMember(
            dest => dest.SourceActivity,
            opt => opt.MapFrom(src => src.SourceActivity))
                         .ForMember(
            dest => dest.SourceActivityUnit,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SourceActivityUnit) ? null : EncryptQueryURL.Decrypt(src.SourceActivityUnit.ToString())));

            CreateMap<TrnItemSource, ItemSourceProfile>()
                .ForMember(
            dest => dest.ItemSourceRadionulcides,
            opt => opt.MapFrom(src => src.TrnItemSourceRadionuclides))
                .ForMember(
            dest => dest.NrrcId,
            opt => opt.MapFrom(src => src.NrrcId))
                   .ForMember(
            dest => dest.ItemSourceFiles,
            opt => opt.MapFrom(src => src.TransactionAttactcments));

            CreateMap<DTOItemSourceRadionulcidesEditor, TrnItemSourceRadionuclides>()
                .ForMember(dest => dest.TrnRadionuclideId,
            opt => opt.MapFrom(src => src.RadionuclideTrnId))
                .ForMember(
            dest => dest.RadionulcideId,
            opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.Radionuclide.ToString())))
           .ForMember(
            dest => dest.InitialActivityUnit,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.InitialActivityUnit) ? null : EncryptQueryURL.Decrypt(src.InitialActivityUnit.ToString())))
           ;
            CreateMap<TrnItemSourceRadionuclides, ItemSourceRadionulcides>();
            CreateMap<TransactionAttachments, ItemSourceFiles>();

            CreateMap<TrnItemSource, DTOGetItemSourceTran>()
                .ForMember(
            dest => dest.TransactionTypeId,
            opt => opt.MapFrom(src => src.TransactionsHeader.TransactionTypeMaster.TransactionTypeId))
                 .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ItemSourceProfile.SourceId.ToString())))
                 .ForMember(
            dest => dest.ShieldNuclearMaterialCode,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ShieldNuclearMaterialCode.ToString())))
                 .ForMember(
            dest => dest.TransactionHeaderId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TransactionHeaderId.ToString())));

            CreateMap<TrnItemSourceRadionuclides, DTOGetTrnItemSourceRadionuclides>();
            CreateMap<TransactionAttachments, DTOGetTransactionAttachments>()
                .ForMember(
                dest => dest.FileBytes,
                src => src.MapFrom(x => Encoding.Default.GetString(x.FileBytes))
                );

            CreateMap<TrnItemSource, TrnItemSource>()
                .ForMember(
                 dest => dest.TrnSourceId,
                 opt => opt.MapFrom(src => 0))
                .ForMember(
                 dest => dest.TransactionAttactcments,
                 opt => opt.MapFrom(src => src.TransactionAttactcments))
                .ForMember(
                 dest => dest.TrnItemSourceRadionuclides,
                 opt => opt.MapFrom(src => src.TrnItemSourceRadionuclides))
                .ForMember(
                 dest => dest.TransactionHeaderId,
                 opt => opt.MapFrom(src => 0))
                ;


            CreateMap<TrnItemSourceRadionuclides, TrnItemSourceRadionuclides>()
                .ForMember(
                 dest => dest.TrnRadionuclideId,
                 opt => opt.MapFrom(src => 0))
                .ForMember(
                 dest => dest.TrnSourceID,
                 opt => opt.MapFrom(src => 0));
            CreateMap<TransactionAttachments, TransactionAttachments>()
                .ForMember(
                 dest => dest.TrnItemSourceFileId,
                 opt => opt.MapFrom(src => 0))
                .ForMember(
                 dest => dest.TrnSourceID,
                 opt => opt.MapFrom(src => 0));

            CreateMap<TrnItemSource, DTOGetSourcesByPermit>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TrnSourceId.ToString()))
                )
                .ForMember(
                dest => dest.SerialNumber,
                opt => opt.MapFrom(src => src.ManufacturerSerialNo)
                );
        }
    }
}
