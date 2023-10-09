using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities
{
    public class PermitGenerators : AuditableBasedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int PermitInventoryId { get; set; }
        public string SerialNo { get; set; }
        public double MaximumVoltage { get; set; }
        public double MaximumTubeCurrent { get; set; }
        public double MaximumEnergy { get; set; }
        public int? EquipmentType { get; set; }
        [ForeignKey("EquipmentType")]
        public virtual LookupSetTerm EquipmentTypeLookup { get; set; }
        public int? ManufactuerName { get; set; }
        [ForeignKey("ManufactuerName")]
        public virtual LookupSetTerm ManufactuerNameLookup { get; set; }

        #region Aman

        public string AmanId { get; set; }

        public string AmanPermitInventoryId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
