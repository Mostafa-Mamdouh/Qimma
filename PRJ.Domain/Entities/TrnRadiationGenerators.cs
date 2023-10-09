﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;
using PRJ.Domain.Entities.AmanIntegrationEntities;

namespace PRJ.Domain.Entities
{
    public class TrnRadiationGenerators : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrnEquipmentID { get; set; }
        [MaxLength(80)]
        public string EquipmentDescFrn { get; set; }
        [MaxLength(80)]
        public string EquipmentDescNtv { get; set; }
        [MaxLength(30)]
        public string Status { get; set; }
        [MaxLength(30)]
        public string ManufacturerSerialNo { get; set; }
        public Nullable<DateTime> DateofManufacturing { get; set; }
        [MaxLength(30)]
        public string FacilitySerialNo { get; set; }
        [MaxLength(30)]
        public string Purpose { get; set; }
        [MaxLength(30)]
        public string ModelNumber { get; set; }
        [MaxLength(30)]
        public string XRayTubeSerialNo { get; set; }
        [MaxLength(30)]
        public string MaxEnergy { get; set; }
        [MaxLength(30)]
        public string EnergyUnit { get; set; }
        [MaxLength(30)]
        public string MaxDoseRate { get; set; }
        [MaxLength(30)]
        public string DoseUnit { get; set; }
        [MaxLength(30)]
        public string MaxCurrent { get; set; }
        [MaxLength(30)]
        public string Unit { get; set; }
        [MaxLength(30)]
        public string SheildMaterial { get; set; }
        [MaxLength(30)]
        public string ShieldNuclearMaterialCode { get; set; }

        //1
        [Display(Name = "TransactionsHeader")]
        public virtual int? TransactionID { get; set; }
        [ForeignKey("TransactionID")]
        public virtual TransactionHeader TransactionsHeader { get; set; }
        //2
        [Display(Name = "LookupSet")]
        public virtual int? EquipmentType { get; set; }
        [ForeignKey("EquipmentType")]
        public virtual LookupSet LookupSet { get; set; }
        //3
        [Display(Name = "EntityProfile")]
        public virtual int? EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityProfile EntityProfile { get; set; }
        //4
        [Display(Name = "FacilityProfile")]
        public virtual int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }
        //5
        [Display(Name = "LicenseProfile")]
        public virtual int? LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        //6
        [Display(Name = "LicenseInventoryLimits")]
        public virtual int? LicenseInventoryId { get; set; }
        [ForeignKey("LicenseInventoryId")]
        public virtual LicenseInventoryLimits LicenseInventoryLimits { get; set; }
        //7
        [Display(Name = "PermitDetailsProfile")]
        public virtual int? PermitdetailsId { get; set; }
        [ForeignKey("PermitdetailsId")]
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
        //8
        [Display(Name = "PermitInventoryLimits")]
        public virtual int? PermitInventoryId { get; set; }
        [ForeignKey("PermitInventoryId")]
        public virtual PermitInventoryLimits PermitInventoryLimits { get; set; }
        //9
        [Display(Name = "PractiseProfile")]
        public virtual int? PractiseId { get; set; }
        [ForeignKey("PractiseId")]
        public virtual PractiseProfile PractiseProfile { get; set; }
        //10
        [Display(Name = "ManufacturerMaster")]
        public virtual int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual ManufacturerMaster ManufacturerMaster { get; set; }
        //11
        [Display(Name = "SafetyResponsibleOfficersProfile")]
        public virtual int? SROId { get; set; }
        [ForeignKey("SROId")]
        public virtual SafetyResponsibleOfficersProfile SafetyResponsibleOfficersProfile { get; set; }
        //12
        [Display(Name = "LegalRepresentativesProfile")]
        public virtual int? LegalRepresentativesId { get; set; }
        [ForeignKey("LegalRepresentativesId")]
        public virtual LegalRepresentativesProfile LegalRepresentativesProfile { get; set; }

    }
}

