using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRJ.Domain.Common;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class NuclearRelatedItemsProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NRRCRelatedItemId { get; set; }
		[MaxLength(80)]
		public string RelatedItemDescAr { get; set; }
		[MaxLength(80)]
		public string RelatedItemDescEn { get; set; }
		[MaxLength(20)]
		public string NrrcID { get; set; }
		[MaxLength(30)]
		public string Status { get; set; }
		[MaxLength(30)]
		public string ManufacturerSerialNo { get; set; }
		public DateTime DateofManufacturing { get; set; }
		[MaxLength(30)]
		public string FacilityRelatedItemID { get; set; }
		[MaxLength(30)]
		public string Purpose { get; set; }
		[MaxLength(30)]
		public string ItemTypeNo { get; set; }
		[MaxLength(30)]
		public string ItemtypeName { get; set; }
		[MaxLength(30)]
		public string ModelNumber { get; set; }
		[MaxLength(30)]
		public string HSCode { get; set; }
		[MaxLength(30)]
		public string GovernmentCommitmentsFlag { get; set; }
		[MaxLength(30)]
		public string EndUserCertificateFlag { get; set; }
		[MaxLength(30)]
		public string Unit { get; set; }
		public int PermitInitialQty { get; set; }

		//1
		[Display(Name = "EntityProfile")]
		public virtual int? EntityId { get; set; }
		[ForeignKey("EntityId")]
		public virtual EntityProfile EntityProfile { get; set; }
		//2
		[Display(Name = "FacilityProfile")]
		public virtual int? FacilityId { get; set; }
		[ForeignKey("FacilityId")]
		public virtual FacilityProfile FacilityProfile { get; set; }
		//3
		[Display(Name = "LicenseProfile")]
		public virtual int? LicenseId { get; set; }
		[ForeignKey("LicenseId")]
		public virtual LicenseProfile LicenseProfile { get; set; }
		//4
		[Display(Name = "LicenseInventoryLimits")]
		public virtual int? LicenseInventoryId { get; set; }
		[ForeignKey("LicenseInventoryId")]
		public virtual LicenseInventoryLimits LicenseInventoryLimits { get; set; }
		//5
		[Display(Name = "PermitDetailsProfile")]
		public virtual int? PermitdetailsId { get; set; }
		[ForeignKey("PermitdetailsId")]
		public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
		//6
		[Display(Name = "PermitInventoryLimits")]
		public virtual int? PermitInventoryId { get; set; }
		[ForeignKey("PermitInventoryId")]
		public virtual PermitInventoryLimits PermitInventoryLimits { get; set; }
		//7
		[Display(Name = "PractiseProfile")]
		public virtual int? PractiseId { get; set; }
		[ForeignKey("PractiseId")]
		public virtual PractiseProfile PractiseProfile { get; set; }
		//8
		[Display(Name = "SafetyResponsibleOfficersProfile")]
		public virtual int? SROId { get; set; }
		[ForeignKey("SROId")]
		public virtual SafetyResponsibleOfficersProfile SafetyResponsibleOfficersProfile { get; set; }
		//9
		[Display(Name = "LegalRepresentativesProfile")]
		public virtual int? LegalRepresentativesId { get; set; }
		[ForeignKey("LegalRepresentativesId")]
		public virtual LegalRepresentativesProfile LegalRepresentativesProfile { get; set; }
		//10
		[Display(Name = "ManufacturerMaster")]
		public virtual int? ManufacturerId { get; set; }
		[ForeignKey("ManufacturerId")]
		public virtual ManufacturerMaster ManufacturerMaster { get; set; }
		//11
		[Display(Name = "BasCountries")]
		public virtual int? ManufactureCountryId { get; set; }
		[ForeignKey("ManufactureCountryId")]
		public virtual BasCountries BasCountries { get; set; }
		//12
		[Display(Name = "LookupSet")]
		public virtual int? ItemCategory { get; set; }
		[ForeignKey("ItemCategory")]
		public virtual LookupSet LookupSet { get; set; }
	}

}
