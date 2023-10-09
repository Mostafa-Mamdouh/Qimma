using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Application.DTOs.RelatedItems.Hierarchy;
using PRJ.Application.DTOs.RelatedItems.Items;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using System.Text;

namespace PRJ.Application.Mappings.RelatedItems
{
    public class RelatedItemsProfile : Profile
    {
        public RelatedItemsProfile()
        {
            #region RelatedItemsHierarchy
            CreateMap<DTORelatedItemsHierarchy, RelatedItemsHierarchyStructure>().ReverseMap();

            CreateMap<DTORelatedItemsHierarchyAsLookup, RelatedItemsHierarchyStructure>().ReverseMap();
            #endregion

            #region RelatedItems
            CreateMap<DTORelatedItem, RelatedItem>()
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
            dest => dest.PermitdetailsId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PermitdetailsId) ? null : EncryptQueryURL.Decrypt(src.PermitdetailsId.ToString())))
                .ForMember(
            dest => dest.StatusID,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.StatusID) ? null : EncryptQueryURL.Decrypt(src.StatusID.ToString())))
                .ForMember(
            dest => dest.ManufacturerId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ManufacturerId) ? null : EncryptQueryURL.Decrypt(src.ManufacturerId.ToString())))
                .ForMember(
            dest => dest.ManufacturerCountryId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ManufacturerCountryId) ? null : EncryptQueryURL.Decrypt(src.ManufacturerCountryId.ToString())));

            CreateMap<RelatedItem, DTORelatedItem>()
               .ForMember(
           dest => dest.EntityId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.EntityId.ToString())))
               .ForMember(
           dest => dest.FacilityId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FacilityId.ToString())))
               .ForMember(
           dest => dest.LicenseId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseId.ToString())))
               .ForMember(
           dest => dest.PermitdetailsId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.PermitdetailsId.ToString())))
               .ForMember(
           dest => dest.StatusID,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.StatusID.ToString())))
               .ForMember(
           dest => dest.ManufacturerId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ManufacturerId.ToString())))
               .ForMember(
           dest => dest.ManufacturerCountryId,
           opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ManufacturerCountryId.ToString())));

            CreateMap<RelatedItem, DTOPaginRelatedItems>()
                .ForMember(dest => dest.RelatedItemCode,
           opt => opt.MapFrom(src => src.RelatedItemCode))
                .ForMember(dest => dest.Status,
           opt => opt.MapFrom(src => src.StatusLookup.DisplayNameEn))
                .ForMember(dest => dest.HierarchyStructureCode,
           opt => opt.MapFrom(src => src.HierarchyStructureCode))
                .ForMember(dest => dest.HSCode,
           opt => opt.MapFrom(src => src.HierarchyStructure.HSCode));
            #endregion

            #region RelatedItemsFiles
            CreateMap<DTORelatedItemsFiles, RelatedItemFiles>()
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
            dest => dest.RelatedItemCode,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RelatedItemCode) ? null : EncryptQueryURL.Decrypt(src.RelatedItemCode.ToString())));

            CreateMap<RelatedItemFiles, DTORelatedItemsFiles>()
               .ForMember(
            dest => dest.Base64,
            opt => opt.MapFrom(src => Encoding.Default.GetString(src.FileBytes)))
                .ForMember(
            dest => dest.FileId,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FileId.ToString())))
                .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.FileOriginalName))
                .ForMember(
            dest => dest.RelatedItemCode,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RelatedItemCode.ToString())));
            #endregion

        }
    }
}
