using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class WorkersDosimeters : AuditableBasedEntity
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public int LineNum { get; set; }
        [MaxLength(50)]
        public string DosimeterType { get; set; }
        [MaxLength(100)]
        public string DosimeterID { get; set; }
        public bool? ActiveFlag { get; set; }
        public Nullable<DateTime> StartWearDate { get; set; }
        public Nullable<DateTime> EndWearDate { get; set; }


    }
}
