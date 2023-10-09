using PRJ.Application.AmanDTOs.LicenseInventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs
{
    public class DTOPermitMaster
    {
        [Required]
        public Guid EntityId { get; set; }
        [Required]
        public Guid FacilityId { get; set; }
        [Required]
        public Guid LicenseMasterId{ get; set; }
        [Required]
        public Guid PermitMasterId { get; set; }
        public string PermitCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? AmanInsertedOn { get; set; }
        public AmanLookupDto PermitType { get; set; }
        public DTOPermitInventory PermitInventory { get; set; }


    }
}
