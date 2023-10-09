using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using System.Reflection.Emit;

namespace PRJ.Domain.Entities.AmanIntegrationEntities
{
    public class LicenseInventoryLimits : AuditableBasedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LicenseInventoryId { get; set; }

        public int LicenseMasterId { get; set; }

        [ForeignKey("LicenseMasterId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        public List<LicenseSealedSources> SealedSources { get; set; }
        public List<LicenseUnSealedSources> UnSealedSources { get; set; }
        public List<VUnsealedSources> VUnSealedSources { get; set; }
        public List<LicenseGenerators> Generators { get; set; }


        #region Aman

        public string AmanId { get; set; }

        public string AmanLicenseMasterId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
