using PRJ.Domain.Common;
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
    public class CustomerProfile : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public string CustomerNameAr { get; set; }
        [MaxLength(200)]

        public string CustomerNameEn { get; set; }
        [MaxLength(200)]

        public string RefCode { get; set; }
        [MaxLength(50)]

        public bool ActiveFlag { get; set; }
    }
}
