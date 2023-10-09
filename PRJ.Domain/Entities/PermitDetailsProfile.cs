using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class PermitDetailsProfile : AuditableBasedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermitDetailsId { get; set; }
        public string PermitNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }


        public virtual int? LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public virtual LicenseProfile LicenseProfile { get; set; }

        public virtual int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }

        public int? PermitType { get; set; }
        [ForeignKey("PermitType")]
        public virtual LookupSetTerm PermitTypeLookup { get; set; }


        #region Aman

        public string AmanId { get; set; }

        public string AmanLicenseId { get; set; }

        public string AmanFacilityId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion

    }
}
