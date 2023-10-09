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
    public class PermitUnSealedSources : AuditableBasedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int PermitInventoryId { get; set; }
  
        public string SerialNo { get; set; }
        public double CurrentActivity { get; set; }
        public int? ManufactuerName { get; set; }
        [ForeignKey("ManufactuerName")]
        public virtual LookupSetTerm ManufactuerNameLookup { get; set; }
        public int? Radionuclide { get; set; }
        [ForeignKey("Radionuclide")]
        public virtual LookupSetTerm RadionuclideLookup { get; set; }
        public int? PhysicalForm { get; set; }
        [ForeignKey("PhysicalForm")]
        public virtual LookupSetTerm PhysicalFormLookup { get; set; }
        public int? CurrentActivityUnit { get; set; }
        [ForeignKey("CurrentActivityUnit")]
        public virtual LookupSetTerm CurrentActivityUnitLookup { get; set; }


        #region Aman

        public string AmanId { get; set; }

        public string AmanPermitInventoryId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
