using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;
namespace PRJ.Application.DTOs.NuclearRelatedItemsProfile
{
    public class DTONuclearRelatedItemsProfile : BasedDtoEntity
    {
        public string RelatedItemDescAr { get; set; }
        public string RelatedItemDescEn { get; set; }
        public string NrrcID { get; set; }
        public string Status { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public DateTime DateofManufacturing { get; set; }
        public string FacilityRelatedItemID { get; set; }
        public string Purpose { get; set; }
        public string ItemTypeNo { get; set; }
        public string ItemtypeName { get; set; }
        public string ModelNumber { get; set; }
        public string HSCode { get; set; }
        public string GovernmentCommitmentsFlag { get; set; }
        public string EndUserCertificateFlag { get; set; }
        public string Unit { get; set; }
        public int PermitInitialQty { get; set; }
        public virtual int? EntityId { get; set; }
        public virtual ent.EntityProfile EntityProfile { get; set; }
        public virtual int? FacilityId { get; set; }
        public virtual ent.FacilityProfile FacilityProfile { get; set; }
        public virtual int? LicenseId { get; set; }
        public virtual ent.LicenseProfile LicenseProfile { get; set; }
        public virtual int? LicenseInventoryId { get; set; }
        public virtual ent.AmanIntegrationEntities.LicenseInventoryLimits LicenseInventoryLimits { get; set; }
        public virtual int? PermitdetailsId { get; set; }
        public virtual ent.PermitDetailsProfile PermitDetailsProfile { get; set; }
        public virtual int? PermitInventoryId { get; set; }
        public virtual ent.AmanIntegrationEntities.PermitInventoryLimits PermitInventoryLimits { get; set; }
        public virtual int? PractiseId { get; set; }
        public virtual ent.PractiseProfile PractiseProfile { get; set; }
        public virtual int? SROId { get; set; }
        public virtual ent.SafetyResponsibleOfficersProfile SafetyResponsibleOfficersProfile { get; set; }
       
        public virtual int? LegalRepresentativesId { get; set; }
        public virtual ent.LegalRepresentativesProfile LegalRepresentativesProfile { get; set; }
       
        public virtual int? ManufacturerId { get; set; }
        public virtual ent.ManufacturerMaster ManufacturerMaster { get; set; }
        public virtual int? ManufactureCountryId { get; set; }
        public virtual ent.BasCountries BasCountries { get; set; }
        public virtual int? ItemCategory { get; set; }
        public virtual ent.LookupSet LookupSet { get; set; }
    }
}
