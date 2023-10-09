using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.RadiationGeneratorsProfile
{
    public class RadiationGeneratorsProfile : BasedDtoEntity
    {
        public string EquipmentDescAr { get; set; }
        public string EquipmentDescEn { get; set; }
        public string NrrcID { get; set; }
        public string Status { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public DateTime DateofManufacturing { get; set; }
        public string FacilitySerialNo { get; set; }
        public string Purpose { get; set; }
        public string ModelNumber { get; set; }
        public string XRayTubeSerialNo { get; set; }
        public string MaxEnergy { get; set; }
        public string EnergyUnit { get; set; }
        public string MaxDoseRate { get; set; }
        public string DoseUnit { get; set; }
        public string MaxCurrent { get; set; }
        public string Unit { get; set; }
        public string SheildMaterial { get; set; }
        public string ShieldNuclearMaterialCode { get; set; }
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
        public virtual int? EquipmentType { get; set; }
        public virtual ent.LookupSet LookupSet { get; set; }
    }
}
