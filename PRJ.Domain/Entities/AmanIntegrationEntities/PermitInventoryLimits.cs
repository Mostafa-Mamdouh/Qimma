using PRJ.Domain.Common;
using PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.AmanIntegrationEntities
{
    public class PermitInventoryLimits : AuditableBasedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermitInventoryId { get; set; }

        public int LicenseMasterId { get; set; }
        [ForeignKey("LicenseMasterId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        public int PermitMasterId { get; set; }
        [ForeignKey("PermitMasterId")]
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }

        public List<PermitSealedSources> SealedSources { get; set; }
        public List<PermitUnSealedSources> UnSealedSources { get; set; }
        public List<PermitNuclearSources> NuclearSources { get; set; }
        public List<PermitGenerators> Generators { get; set; }


        #region Aman
        public string AmanId { get; set; }

        public string AmanPermitMasterId { get; set; }

        public string AmanLicenseMasterId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
