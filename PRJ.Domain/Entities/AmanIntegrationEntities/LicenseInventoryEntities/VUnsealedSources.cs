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
    public class VUnsealedSources : AuditableBasedEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Id { get; set; }
        public int LicenseInventoryId { get; set; }

        public int NumberOfSources { get; set; }
        public double MaximumRadioactivityInTheWorkPlace { get; set; }
        public double MaximumRadioactivityPerSource { get; set; }
        public string PurposeOfUse { get; set; }


        public int? SourceUsedIn { get; set; }
        [ForeignKey("SourceUsedIn")]
        public virtual LookupSetTerm SourceUsedInLookup { get; set; }
        public int? Radionuclide { get; set; }
        [ForeignKey("Radionuclide")]
        public virtual LookupSetTerm RadionuclideLookup { get; set; }
        public int? PhysicalForm { get; set; }
        [ForeignKey("PhysicalForm")]
        public virtual LookupSetTerm PhysicalFormLookup { get; set; }
        public int? UnsealedSourceType { get; set; }
        [ForeignKey("UnsealedSourceType")]
        public virtual LookupSetTerm UnsealedSourceTypeLookup { get; set; }
        public int? MaximumRadioactivityPerSourceUnit { get; set; }
        [ForeignKey("MaximumRadioactivityPerSourceUnit")]
        public virtual LookupSetTerm MaximumRadioactivityPerSourceUnitLookup { get; set; }
        public int? MaximumRadioactivityInTheWorkPlaceUnit { get; set; }
        [ForeignKey("MaximumRadioactivityInTheWorkPlaceUnit")]
        public virtual LookupSetTerm MaximumRadioactivityInTheWorkPlaceUnitLookup { get; set; }

        #region Aman
        public string AmanId { get; set; }
        public string AmanLicenseInventoryId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion


    }
}
