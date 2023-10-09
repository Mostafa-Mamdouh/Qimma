using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities
{
    public class PermitSealedSources : AuditableBasedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int PermitInventoryId { get; set; }

        public string SerialNo { get; set; }
        public string Model { get; set; }
        public double MaximumRadioactivity { get; set; }
        public int? ManufactuerName { get; set; }
        [ForeignKey("ManufactuerName")]
        public virtual LookupSetTerm ManufactuerNameLookup { get; set; }
        public int? Radionuclide { get; set; }
        [ForeignKey("Radionuclide")]
        public virtual LookupSetTerm RadionuclideLookup { get; set; }
        public int? MaximumRadioactivityUnit { get; set; }
        [ForeignKey("MaximumRadioactivityUnit")]
        public virtual LookupSetTerm MaximumRadioactivityUnitLookup { get; set; }

        #region Aman

        public string AmanId { get; set; }

        public string AmanPermitInventoryId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion

    }
}
