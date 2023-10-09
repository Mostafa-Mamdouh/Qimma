﻿using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Common;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    [Index(nameof(TrnItemSource.ManufacturerSerialNo), IsUnique = true)]
    public class TrnItemSource : AuditableBasedEntity
    {
        public TrnItemSource()
        {
            this.TransactionAttactcments = new HashSet<TransactionAttachments>();
            this.TrnItemSourceRadionuclides = new HashSet<TrnItemSourceRadionuclides>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrnSourceId { get; set; }
        public string? NrrcId { get; set; }// Generated By The System
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public int? Quantity { get; set; }
        public string ShieldNuclearMaterialCode { get; set; }
        public double? InitialMass { get; set; }
        public double? SourceActivity { get; set; }
        public bool IsShieldDU { get; set; }
        public string SourceModel { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }

        #region Navigation Properties

        public int? SourceType { get; set; }
        [ForeignKey("SourceType")]
        public virtual LookupSetTerm SourceTypeLookup { get; set; }
        public int? Status { get; set; }
        [ForeignKey("Status")]
        public virtual LookupSetTerm StatusLookup { get; set; }
        public int? PhysicalForm { get; set; }
        [ForeignKey("PhysicalForm")]
        public virtual LookupSetTerm PhysicalFormLookup { get; set; }

        public int? AssociatedEquipment { get; set; }
        [ForeignKey("AssociatedEquipment")]
        //[ForeignKey("AssociatedEquipment, value")]
        public virtual LookupSetTerm AssociatedEquipmentLookup { get; set; }

        public int? NuclearMaterial { get; set; }
        [ForeignKey("NuclearMaterial")]
        public virtual LookupSetTerm NuclearMaterialLookup { get; set; }

        public int? InitialMassUnit { get; set; }
        [ForeignKey("InitialMassUnit")]
        public virtual LookupSetTerm InitialMassUnitLookup { get; set; }

        public int? SourceActivityUnit { get; set; }
        [ForeignKey("SourceActivityUnit")]
        public virtual LookupSetTerm SourceActivityLookup { get; set; }

        public int? SourceCategory { get; set; }
        [ForeignKey("SourceCategory")]
        public virtual LookupSetTerm SourceCategoryLookup { get; set; }

        public int? TransactionHeaderId { get; set; }
        [ForeignKey("TransactionHeaderId")]
        public virtual TransactionHeader TransactionsHeader { get; set; }
        public int? EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityProfile EntityProfile { get; set; }
        public int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }
        public int? LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        public int? LicenseInventoryId { get; set; }
        [ForeignKey("LicenseInventoryId")]
        public virtual LicenseInventoryLimits LicenseInventoryLimits { get; set; }
        public int? PermitdetailsId { get; set; }
        [ForeignKey("PermitdetailsId")]
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
        public int? PermitInventoryId { get; set; }
        [ForeignKey("PermitInventoryId")]
        public virtual PermitInventoryLimits PermitInventoryLimits { get; set; }
        public int? PractiseId { get; set; }
        [ForeignKey("PractiseId")]
        public virtual PractiseProfile PractiseProfile { get; set; }
        public int? SROId { get; set; }
        [ForeignKey("SROId")]
        public virtual SafetyResponsibleOfficersProfile SafetyResponsibleOfficersProfile { get; set; }
        public int? LegalRepresentativesId { get; set; }
        [ForeignKey("LegalRepresentativesId")]
        public virtual LegalRepresentativesProfile LegalRepresentativesProfile { get; set; }
        public int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual LookupSetTerm ManufacturerLookup { get; set; }

        public int? ManufacturerCountryId { get; set; }
        [ForeignKey("ManufacturerCountryId")]
        public virtual LookupSetTerm ManufacturerCountryLookup { get; set; }
        public virtual ItemSourceProfile ItemSourceProfile { get; set; }
        public virtual ICollection<TransactionAttachments> TransactionAttactcments { get; set; }
        public virtual ICollection<TrnItemSourceRadionuclides> TrnItemSourceRadionuclides { get; set; }


        #endregion
    }
}
