using AutoMapper;
using PRJ.Application.DTOs.InternalScreen;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.GlobalComponents.Encryption;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<dto.BasCountries.DTOCountries, ent.BasCountries>().ReverseMap()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CountryId.ToString())));
            CreateMap<dto.BasCountries.DTOCountries2, ent.BasCountries>().ReverseMap()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CountryId.ToString())));


            CreateMap<dto.BasCountries.DTONationalites, ent.BasCountries>().ReverseMap()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CountryId.ToString())));

            CreateMap<dto.BasCites.DTOCites, ent.BasCities>().ReverseMap()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CityId.ToString())))
                .ForMember(
                    dest => dest.CountryId,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CountryId.ToString())));

            CreateMap<dto.EntityProfile.DTOEntityProfile, ent.EntityProfile>().ReverseMap()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.EntityId.ToString())));

            CreateMap<dto.FacilityProfile.DTOFacilityProfile, ent.FacilityProfile>().ReverseMap()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FacilityId.ToString()))
                   )
               .ForMember(
                   dest => dest.EntityId,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.EntityId.ToString()))
                ).ReverseMap();

            CreateMap<dto.DTOManufacturerMaster, ent.ManufacturerMaster>().ReverseMap()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ManufacturerId.ToString())));

            CreateMap<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile, ent.NuclearRelatedItemsProfile>().ReverseMap()
             .ForMember(
                 dest => dest.Id,
                 opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.NRRCRelatedItemId.ToString())));
            CreateMap<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile, ent.RadiationGeneratorsProfile>().ReverseMap()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.EquipmentId.ToString())));
            CreateMap<dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile, ent.SafetyResponsibleOfficersProfile>().ReverseMap()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.SROId.ToString())));

            CreateMap<dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile, ent.LegalRepresentativesProfile>().ReverseMap()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LegalRepresentativesId.ToString())));
            CreateMap<dto.LicenseInventoryLimits.DTOLicenseInventoryLimits, LicenseInventoryLimits>().ReverseMap();
            //.ForMember(
            //    dest => dest.Id,
            //    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseInventoryId.ToString())));
            CreateMap<dto.LicenseProfile.DTOLicenseProfile, ent.LicenseProfile>().ReverseMap()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseId.ToString())))
           .ForMember(
               dest => dest.FacilityId,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FacilityId.ToString()))
               );

            CreateMap<dto.LicenseProfile.DTOLicenseNumber, ent.LicenseProfile>().ReverseMap()
            .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseId.ToString())));
            CreateMap<dto.LookupSet.DTOLookupSet, ent.LookupSet>().ReverseMap()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetId.ToString())));
            CreateMap<dto.LookupSet.DTOLookUpAdd, ent.LookupSet>().ReverseMap()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetId.ToString())));


            CreateMap<dto.LookupSetTerm.DTOLookupSetTerm, ent.LookupSetTerm>().ReverseMap()
           .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetTermId.ToString())));
            CreateMap<dto.PermitDetailsProfile.DTOPermitDetailsProfile, ent.PermitDetailsProfile>().ReverseMap()
           .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.PermitDetailsId.ToString())));
            //.ForMember(
            //    dest => dest.LicenseId,
            //    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LicenseMasterId.ToString()))
            //  );
            CreateMap<dto.PermitDetailsProfile.DTOPermitNumber, ent.PermitDetailsProfile>().ReverseMap()
           .ForMember(
             dest => dest.Id,
             opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.PermitDetailsId.ToString())));

            // CreateMap<dto.PermitInventoryLimits.DTOPermitInventoryLimits, PermitInventoryLimits>().ReverseMap()
            //.ForMember(
            //   dest => dest.Id,
            //   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.PermitInventoryId.ToString())));
            CreateMap<dto.PractiseProfile.DTOPractiseProfile, ent.PractiseProfile>().ReverseMap()
           .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.PractiseId.ToString())))
                       .ForMember(
              dest => dest.PermitDetailsId,
              opt => opt.MapFrom(src => src.PermitDetailsId));

            CreateMap<dto.LookupSetTerm.DTOLookupSetTermAction, ent.LookupSetTerm>().ReverseMap();

            CreateMap<dto.LookupSetTerm.DTOAllLookupSetTerm, ent.LookupSetTerm>().ReverseMap()
           .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetTermId.ToString())));


            CreateMap<dto.LookupSet.DTOAllLookUps, ent.LookupSet>().ReverseMap()
            .ForMember(
               dest => dest.Id,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetId.ToString())))
                .AfterMap((entity, model) =>
                {
                    foreach (var item in entity.LookupSetTerms)
                    {
                        item.LookupSet = entity;
                    }
                })
                .AfterMap((model, entity) =>
                {
                    foreach (var item in entity.LookupSetTerms)
                    {
                        item.LookupSetId = EncryptQueryURL.Encrypt(item.LookupSetId);
                    }
                }).ReverseMap();


            CreateMap<dto.Radionuclide.RadionuclideDto, ent.Radionuclides>().ReverseMap()
           .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RadionuclideId.ToString())))
             .ForMember(
            dest => dest.ExemptionValue,
            opt => opt.MapFrom(src => src.ExemptionValue ?? 0))
              .ForMember(
            dest => dest.HalfLife,
            opt => opt.MapFrom(src => src.HalfLife ?? 0))
           .ForMember(
            dest => dest.ExemptionValueUnit,
            opt => opt.MapFrom(src => src.ExemptionValueUnit != null ? src.ExemptionValueUnit.DisplayNameEn : null))
           .ForMember(
            dest => dest.HalfLifeUnit,
            opt => opt.MapFrom(src => src.HalfLifeUnit != null ? src.HalfLifeUnit.DisplayNameEn : null)); ;

            CreateMap<dto.Radionuclide.AddRadionuclideDto, ent.Radionuclides>().ReverseMap()
                      .ForMember(
                dest => dest.HalfLifeUnitId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.HalfLifeUnit.ToString())))
                      .ForMember(
                dest => dest.ExemptionValueUnitId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.ExemptionValueUnit.ToString())));

            CreateMap<dto.Radionuclide.UpdateRadionuclideDto, ent.Radionuclides>().ReverseMap();

            CreateMap<dto.Workers.DTOWorkers, ent.Workers>().ReverseMap()
            .ForMember(
             dest => dest.Id,
             opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Id.ToString())))
            .ForMember(
               dest => dest.FacilityId,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FacilityId.ToString())))
            .ForMember(
               dest => dest.Status,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Status.ToString())))
            .ForMember(
               dest => dest.Nationality,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Nationality.ToString())))
            .ForMember(
               dest => dest.JobPosition,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.JobPosition.ToString())))
            .ForMember(
               dest => dest.Gender,
               opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Gender.ToString())));

            CreateMap<dto.Workers.DTOWorkersDosimeters, ent.WorkersDosimeters>().ReverseMap()
           .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.Id.ToString())));

            CreateMap<dto.BillingServiceTrn.DTOBillingServiceTrnHeader, ent.BillingServiceTrn.BillingServiceTrnHeader>().ReverseMap()
              .ForMember(
                dest => dest.StatusFlag,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.StatusFlag.ToString())))
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TransactionID.ToString())));


            //          CreateMap<DTOInvoiceTransactionHeader, ent.BillingServiceTrn.BillingServiceTrnHeader>()
            //.ForMember(
            //  dest => dest.CustomerId,
            //  opt => opt.MapFrom(src => src.CustomerId.ToString()))
            //   .ForMember(
            //  dest => dest.CustomerName,
            //  opt => opt.MapFrom(src => src.CustomerName.ToString()))
            //        .ForMember(
            //  dest => dest.TrnRemarks,
            //  opt => opt.MapFrom(src => src.Remarks.ToString()))
            //                  .ForMember(
            //  dest => dest.TermsCode.ToString(),
            //  opt => opt.MapFrom(src => src.PaymentTerms.ToString()));



            CreateMap<dto.Workers.DTOWorkersExposuresDoses, ent.WorkersExposuresDoses>().ReverseMap()
            .ForMember(
            dest => dest.DosimeterType,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.DosimeterType.ToString())));

            #region Screen Management
            CreateMap<dto.InternalScreen.DTOInternalScreen, ent.InternalScreen>().ReverseMap()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ScreenId.ToString())))
               .ForMember(
                   dest => dest.InternalScreenFields,
                   opt => opt.MapFrom(src => src.InternalScreenFields));



            CreateMap<dto.InternalScreen.DTOInternalScreenField, ent.InternalScreenField>().ReverseMap()
             .ForMember(
             dest => dest.Id,
             opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FieldId.ToString())))
             .ForMember(
             dest => dest.ScreenNameAr,
             opt => opt.MapFrom(src => src.Screen.ScreenNameAr))
             .ForMember(
             dest => dest.ScreenNameEn,
             opt => opt.MapFrom(src => src.Screen.ScreenNameEn))
            .ForMember(
             dest => dest.LovNameAr,
             opt => opt.MapFrom(src => src.ListOfValue.LovNameAr))
             .ForMember(
             dest => dest.LovNameEn,
             opt => opt.MapFrom(src => src.ListOfValue.LovNameEn))
             .ForMember(
             dest => dest.ClassName,
             opt => opt.MapFrom(src => src.LookupSet.ClassName))
             .ForMember(
             dest => dest.FieldTypeEn,
             opt => opt.MapFrom(src => src.FieldType == 1 ? "Date" : src.FieldType == 2 ? "ListItem" : src.FieldType == 3 ? "Text" :
             src.FieldType == 4 ? "TextArea" : src.FieldType == 5 ? "RadioButon" : src.FieldType == 6 ? "CheckBox" : src.FieldType == 7 ? "Button" : "FileUpload"))
             .ForMember(
             dest => dest.FieldTypeAr,
             opt => opt.MapFrom(src => src.FieldType == 1 ? "تاريخ" : src.FieldType == 2 ? "قائمة منسدلة" : src.FieldType == 3 ? "حقل" : "حقل كبير"));

            CreateMap<dto.ListOfValues.DTOListOfValue, ent.ListOfValue>().ReverseMap()
            .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LovId.ToString())))
            .ForMember(
            dest => dest.CanDelete,
            opt => opt.MapFrom(src => !src.InternalScreenFields.Any())
            );

            #endregion
            #region Internal User Management
            CreateMap<dto.InternalRole.DTOInternalRole, ent.InternalRole>().ReverseMap()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RoleId.ToString())));

            CreateMap<dto.InternalRole.DTOInternalScreenRole, ent.InternalScreenRole>().ReverseMap();


            CreateMap<dto.InternalRole.DTOInternalScreenRole, ent.InternalScreen>().ReverseMap()
                .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ScreenId.ToString())))
              .ForMember(
                   dest => dest.Query,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().Query : false))
              .ForMember(
                   dest => dest.Insert,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().Insert : false))
              .ForMember(
                   dest => dest.Update,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().Update : false))
              .ForMember(
                   dest => dest.Delete,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().Delete : false))
                .ForMember(
                   dest => dest.ScreenOrder,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().ScreenOrder : 0))

                     .ForMember(
                   dest => dest.RoleId,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().RoleId : 0))
            .ForMember(
                   dest => dest.ScreenRoleId,
                   opt => opt.MapFrom(src => src.InternalScreenRoles.Any() ? src.InternalScreenRoles.First().ScreenRoleId : 0));


            CreateMap<dto.InternalScreen.DTOInternalFieldPermission, ent.InternalFieldPermission>()
                 .ForMember(
                   dest => dest.RoleId,
                   opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.RoleId)));
            ;

            CreateMap<ent.InternalFieldPermission, DTOInternalFieldPermission>()
                           .ForMember(
                   dest => dest.RoleId,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RoleId.ToString())));


            CreateMap<dto.InternalScreen.DTOInternalScreenFieldData, ent.InternalScreenField>().ReverseMap()
                  .ForMember(
                   dest => dest.LookupSetId,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LookupSetId.ToString())))
                  .ForMember(
                   dest => dest.LovId,
                   opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.LovId.ToString())))

                 .ForMember(
                    dest => dest.FieldTypeEn,
                    opt => opt.MapFrom(src => src.FieldType == 1 ? "Date" : src.FieldType == 2 ? "ListItem" : src.FieldType == 3 ? "Text" : "TextArea"))
                    .ForMember(
                    dest => dest.FieldTypeAr,
                    opt => opt.MapFrom(src => src.FieldType == 1 ? "تاريخ" : src.FieldType == 2 ? "قائمة منسدلة" : src.FieldType == 3 ? "حقل" : "حقل كبير"))
                .ForMember(
                    dest => dest.InternalFieldPermissions,
                    opt => opt.MapFrom(src => src.InternalFieldPermissions));
            #endregion
            CreateMap<dto.ItemHierarchyStructure.AddItemHierarchyStructureDto, ent.Billing.ItemHierarchyStructure>().ReverseMap();
            CreateMap<dto.ItemHierarchyStructure.ItemHierarchyStructureTreeNode, ent.Billing.ItemHierarchyStructure>().ReverseMap();
            CreateMap<dto.ItemHierarchyStructure.ItemHierarchyStructureDto, ent.Billing.ItemHierarchyStructure>().ReverseMap();
            CreateMap<dto.ItemHierarchyStructure.UpdateItemHierarchyStructureDto, ent.Billing.ItemHierarchyStructure>().ReverseMap();
            //CreateMap<dto.RelatedItems.DTOAddRelatedItems, ent.RelatedItemHierarchyStructure>().ReverseMap();
            //CreateMap<dto.RelatedItems.DTOEditRelatedItems, ent.RelatedItemsHierarchyStructure>().ReverseMap();
            //CreateMap<dto.DTORelatedItem, ent.RelatedItem>().ReverseMap();
            //CreateMap<DTORelatedItemsHierarchy, ent.RelatedItem>().ReverseMap();
            //CreateMap<RelatedItemsHierarchyTreeNode, ent.RelatedItem>().ReverseMap();
            CreateMap<dto.BillingServiceTrn.DTOBillingServiceTrnDetails, ent.BillingServiceTrn.BillingServiceTrnDetails>().ReverseMap();
            CreateMap<dto.BillingServiceTrn.DTOUpdateBillingServiceTrn, ent.BillingServiceTrn.BillingServiceTrnDetails>().ReverseMap();
            CreateMap<dto.BillingServiceTrn.DTOAddBillingServiceTrnHeader, ent.BillingServiceTrn.BillingServiceTrnDetails>().ReverseMap();


            CreateMap<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto, ent.Billing.ServiceItemProfile>().ReverseMap()
          .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ServiceItemId.ToString())));


            #region Related Items
            // CreateMap<dto.DTORelatedItem, ent.RelatedItem>().ReverseMap()
            //.ForMember(
            //    dest => dest.Id,
            //    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.RelatedItemCode.ToString())));


            // CreateMap<dto.DTORelatedItemsFiles, ent.RelatedItemFiles>().ReverseMap()
            //.ForMember(
            //    dest => dest.Id,
            //    opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.FileId.ToString())));

            #endregion

            CreateMap<dto.DTOCustomerProfile, ent.CustomerProfile>().ReverseMap()
          .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.CustomerId.ToString())));


        }
    }
}
