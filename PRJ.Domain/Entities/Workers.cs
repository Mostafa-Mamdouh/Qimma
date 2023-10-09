using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;


namespace PRJ.Domain.Entities
{
    public class Workers : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(80)] 
        public string WorkerNameAr { get; set; }
        [MaxLength(80)]
        public string WorkerNameEn { get; set; }
        [MaxLength(200)]
        public DateTime BirthDate { get; set; }
        [MaxLength(80)]
        public int JobPosition { get; set; }
        [ForeignKey("JobPosition")]
        public virtual LookupSetTerm JobPositionLookup { get; set; }
        public virtual int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }
        public virtual int? Nationality { get; set; }
        [ForeignKey("Nationality")]
        public virtual BasCountries BasCountries { get; set; }
        [MaxLength(80)]
        public string NationalityId { get; set; }
        [MaxLength(80)]
        [Display(Name = "LookupSetTerm")]
        public virtual int? Status { get; set; }
        [ForeignKey("Status")]
        public virtual LookupSetTerm LookupSetTerm { get; set; }
        [MaxLength(80)]
        public string PassportNo { get; set; }
        [MaxLength(50)] 
        public string CurrentDosimeterType { get; set; }
        [MaxLength(100)]
        public string CurrentDosimeterID { get; set; }
        [MaxLength(20)]
        public string MobileNo { get; set; }
        public int Gender { get; set; }
        [ForeignKey("Gender")]
        public virtual LookupSetTerm GenderLookup { get; set; }
        public virtual ICollection<WorkersExposuresDoses> WorkersExposuresDoses { get; set; }
        [NotMapped]
        public virtual WorkersExposuresDoses LastWorkersExposuresDose { get; set; }

        public int CurrentLicense { get; set; }
        public int CurrentPractise { get; set; }


    }
}
