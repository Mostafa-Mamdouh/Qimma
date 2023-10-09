using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities
{
    public class LicenseGenerators : AuditableBasedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Id { get; set; }

        public int LicenseInventoryId { get; set; }
        public double MaximumVoltage { get; set; }
        public double MaximumTubeCurrent { get; set; }
        public double MaximumEnergy { get; set; }
        public double MaximumNumberOfEquipment { get; set; }
        public string PurposeOfUse { get; set; }
        public int? SourceUsedIn { get; set; }
        [ForeignKey("SourceUsedIn")]
        public virtual LookupSetTerm SourceUsedInLookup { get; set; }

        #region Aman

        public string AmanId { get; set; }

        public string AmanLicenseInventoryId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
