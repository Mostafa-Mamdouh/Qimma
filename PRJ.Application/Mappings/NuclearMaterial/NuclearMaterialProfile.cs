using AutoMapper;
using PRJ.Application.DTOs.NuclearMaterial;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialElement;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialFiles;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialRadionuclide;
using PRJ.Application.DTOs.NuclearMaterial.Shield;
using PRJ.Domain.Entities.NuclearMaterial;
using PRJ.GlobalComponents.Encryption;
using System.Text;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.Mappings.NuclearMaterial
{
    public class NuclearMaterialProfile : Profile
    {
        public NuclearMaterialProfile()
        {
            #region NuclearMaterial
            CreateMap<DTONuclearMaterial, ent.NuclearMaterial.NuclearMaterial>()
                      .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SourceId) ? null : EncryptQueryURL.Decrypt(src.SourceId.ToString())))
                      .ForMember(
            dest => dest.EntityId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.EntityId) ? null : EncryptQueryURL.Decrypt(src.EntityId.ToString())))
                      .ForMember(
            dest => dest.FacilityId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.FacilityId) ? null : EncryptQueryURL.Decrypt(src.FacilityId.ToString())))
                      .ForMember(
            dest => dest.LicenseId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LicenseId) ? null : EncryptQueryURL.Decrypt(src.LicenseId.ToString())))
                      .ForMember(
            dest => dest.LicenseInventoryId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LicenseInventoryId) ? null : EncryptQueryURL.Decrypt(src.LicenseInventoryId.ToString())))
                      .ForMember(
            dest => dest.PermitInventoryId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PermitInventoryId) ? null : EncryptQueryURL.Decrypt(src.PermitInventoryId.ToString())))
                      .ForMember(
            dest => dest.PermitdetailsId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PermitdetailsId) ? null : EncryptQueryURL.Decrypt(src.PermitdetailsId.ToString())))
                      .ForMember(
            dest => dest.PractiseId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PractiseId) ? null : EncryptQueryURL.Decrypt(src.PractiseId.ToString())))
                      .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Status) ? null : EncryptQueryURL.Decrypt(src.Status.ToString())))
                      .ForMember(
            dest => dest.ManufacturerCountryId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ManufacturerCountryId) ? null : EncryptQueryURL.Decrypt(src.ManufacturerCountryId.ToString())))
                      .ForMember(
            dest => dest.ManufacturerId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ManufacturerId) ? null : EncryptQueryURL.Decrypt(src.ManufacturerId.ToString())))
                      .ForMember(
            dest => dest.PhysicalForm,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PhysicalForm) ? null : EncryptQueryURL.Decrypt(src.PhysicalForm.ToString())))
                      .ForMember(
            dest => dest.QuantityUnitId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.QuantityUnitId) ? null : EncryptQueryURL.Decrypt(src.QuantityUnitId.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialElements,
            opt => opt.MapFrom(src => src.NuclearMaterialElements));


            CreateMap<ent.NuclearMaterial.NuclearMaterial, DTONuclearMaterial>()
                .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SourceId.ToString())))
                .ForMember(
            dest => dest.EntityId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.EntityId.ToString())))
                .ForMember(
            dest => dest.FacilityId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FacilityId.ToString())))
                .ForMember(
            dest => dest.LicenseId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseId.ToString())));
            #endregion

            #region Files
            CreateMap<DTONuclearMaterialFiles, ent.NuclearMaterial.NuclearMaterialFiles>()
                .ForMember(
            dest => dest.FileBytes,
            opt => opt.MapFrom(src => Encoding.ASCII.GetBytes(src.Base64)))
                .ForMember(
            dest => dest.FileName,
            opt => opt.MapFrom(src => new Random().Next(DateTime.Now.DayOfYear, DateTime.Now.DayOfYear + 100).ToString() + "-" + String.Format(src.Name + "-" + "{0:yyyy-MM-dd_hh-mm-ssss-fff-tt}", DateTime.Now)))
                .ForMember(
            dest => dest.FileSize,
            opt => opt.MapFrom(src => src.Size))
                .ForMember(
            dest => dest.FileOriginalName,
            opt => opt.MapFrom(src => src.Name))
                .ForMember(
            dest => dest.NuclearMaterialId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.NuclearMaterialId) ? null : EncryptQueryURL.Decrypt(src.NuclearMaterialId.ToString())));

            CreateMap<ent.NuclearMaterial.NuclearMaterialFiles, DTONuclearMaterialFiles>()
                .ForMember(
            dest => dest.Base64,
            opt => opt.MapFrom(src => Encoding.Default.GetString(src.FileBytes)))
                .ForMember(
            dest => dest.FileId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ItemSourceFileId.ToString())))
                .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.FileOriginalName));
            #endregion

            #region Radionuclides
            CreateMap<DTONuclearMaterialRadionuclide, ent.NuclearMaterial.NuclearMaterialRadionulcide>()
                .ForMember(
            dest => dest.RadionulcideId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RadionulcideId) ? null : EncryptQueryURL.Decrypt(src.RadionulcideId.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialElementId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.NuclearMaterialElementId) ? null : EncryptQueryURL.Decrypt(src.NuclearMaterialElementId.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialRadionulcideId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.NuclearMaterialElementId) ? null : EncryptQueryURL.Decrypt(src.NuclearMaterialRadionulcideId.ToString())));

            CreateMap<ent.NuclearMaterial.NuclearMaterialRadionulcide, DTONuclearMaterialRadionuclide>()
                .ForMember(
            dest => dest.NuclearMaterialRadionulcideId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.NuclearMaterialRadionulcideId.ToString())))
                .ForMember(
            dest => dest.RadionulcideId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RadionulcideId.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialElementId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.NuclearMaterialElementId.ToString())));
            #endregion

            #region Elements
            CreateMap<DTONuclearMaterialElement, NuclearMaterialElement>()
                .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? null : EncryptQueryURL.Decrypt(src.Id.ToString())))
                .ForMember(
            dest => dest.ElementMassUnit,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ElementMassUnit) ? null : EncryptQueryURL.Decrypt(src.ElementMassUnit.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.NuclearMaterialId) ? null : EncryptQueryURL.Decrypt(src.NuclearMaterialId.ToString())))
                .ForMember(
            dest => dest.NuclearMaterialType,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.NuclearMaterialType) ? null : EncryptQueryURL.Decrypt(src.NuclearMaterialType.ToString())))
                .ForMember(
                dest => dest.NuclearMaterialRadionulcides,
                opt => opt.MapFrom(src => src.NuclearMaterialRadionulcides));

            CreateMap<NuclearMaterialElement, DTONuclearMaterialElement>()
               .ForMember(
           dest => dest.Id,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Id.ToString())))
               .ForMember(
           dest => dest.ElementMassUnit,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ElementMassUnit.ToString())))
               .ForMember(
           dest => dest.NuclearMaterialId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.NuclearMaterialId.ToString())))
               .ForMember(
           dest => dest.NuclearMaterialType,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.NuclearMaterialType.ToString())))
                  .ForMember(
                dest => dest.NuclearMaterialRadionulcides,
                opt => opt.MapFrom(src => src.NuclearMaterialRadionulcides))
                  .ForMember(
                dest => dest.Element,
                opt => opt.MapFrom(src => src.NuclearMaterialRadionulcides.FirstOrDefault().Element));


            #endregion

            #region NuclearMaterialsPaginated
            CreateMap<ent.NuclearMaterial.NuclearMaterial, DTOPaginatedNuclearMaterials>()
                 .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SourceId.ToString())))
                 .ForMember(
            dest => dest.StatusAr,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameAr))
                .ForMember(
            dest => dest.StatusEn,
            opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
                .ForMember(
            dest => dest.PhysicalFormAr,
            opt => opt.MapFrom(src => src.PhysicalFormLookup.DisplayNameAr))
                .ForMember(
            dest => dest.PhysicalFormEn,
            opt => opt.MapFrom(src => src.PhysicalFormLookup.DisplayNameEn))
                .ForMember(
            dest => dest.NuclearMaterialTypeAr,
            opt => opt.MapFrom(src => src.NuclearMaterialElements))
                .ForMember(
            dest => dest.NuclearMaterialTypeEn,
            opt => opt.MapFrom(src => src.NuclearMaterialElements))
                .ForMember(
            dest => dest.EntityAr,
            opt => opt.MapFrom(src => src.EntityProfile.EntityNameAr))
                .ForMember(
            dest => dest.EntityEn,
            opt => opt.MapFrom(src => src.EntityProfile.EntityNameEn))
                .ForMember(
            dest => dest.FacilityAr,
            opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameAr))
                .ForMember(
            dest => dest.FacilityEn,
            opt => opt.MapFrom(src => src.FacilityProfile.FacilityNameEn))
                .ForMember(
            dest => dest.ManufacturerAr,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameAr))
                .ForMember(
            dest => dest.ManufacturerEn,
            opt => opt.MapFrom(src => src.ManufacturerLookup.DisplayNameEn))
                .ForMember(
            dest => dest.License,
            opt => opt.MapFrom(src => src.LicenseProfile.LicenseCode))
                .ForMember(
            dest => dest.Permit,
            opt => opt.MapFrom(src => src.PermitDetailsProfile.PermitNumber));
            #endregion

            #region UpdateNuclearMaterial
            CreateMap<DTOUpdateNuclearMaterial, ent.NuclearMaterial.NuclearMaterial>()
                .ForMember(
                    dest => dest.SourceId,
                    opt => opt.MapFrom(src => string.IsNullOrEmpty(src.SourceId) ? null : EncryptQueryURL.Decrypt(src.SourceId.ToString())))
            .ForMember(
                dest => dest.NuclearMaterialElements,
                opt => opt.MapFrom(src => src.NuclearMaterialElements))
            .ForSourceMember(
                source => source.NuclearMaterialRadionulcides, o => o.DoNotValidate());

            CreateMap<ent.NuclearMaterial.NuclearMaterial, DTOUpdateNuclearMaterial>()
                .ForMember(
                    dest => dest.SourceId,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SourceId.ToString())))
                .ForMember(
                    dest => dest.NuclearMaterialElements,
                    opt => opt.MapFrom(src => src.NuclearMaterialElements))
                .ForMember(
                    dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity));

            #endregion

            #region NuclearMaterialsAsLookup
            CreateMap<ent.NuclearMaterial.NuclearMaterial, DTOGetNuclearMaterialsAsLookup>()
                .ForMember(
            dest => dest.SourceId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SourceId.ToString())));
            #endregion
        }
    }
}
