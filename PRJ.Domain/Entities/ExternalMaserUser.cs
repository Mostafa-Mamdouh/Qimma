using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace PRJ.Domain.Entities
{
	public class ExternalMaserUser : IdentityUser
	{

		[MaxLength(80)]
		[Column(Order = 1)]
		public string FirstNameAr { get; set; }
		[MaxLength(80)]
		[Column(Order = 2)]
		public string SecondNameAr { get; set; }
		[MaxLength(80)]
		[Column(Order = 3)]
		public string LastNameNameAr { get; set; }
		[MaxLength(80)]
		[Column(Order = 4)]
		public string FirstNameEn { get; set; }
		[MaxLength(80)]
		[Column(Order = 5)]
		public string SecondNameEn { get; set; }
		[Column(Order = 6)]
		[MaxLength(80)]
		public string LastNameNameEn { get; set; }
		[Column(Order = 7)]
		public bool IsActiveUser { get; set; }
		[Column(Order = 8)]
		public int UserTypeId { get; set; }  //ExternalUserTypes
		public byte[] Picture { get; set; }
		[Column(Order = 9)]
        [MaxLength(80)]
        public string PictureContentType { get; set; }
		[Column(Order = 10)]
        [MaxLength(80)]
        public string PictureName { get; set; }
		[MaxLength(15)]
		[Column(Order = 11)]
		public string PassportNo { get; set; }
		[Column(Order = 12)]
		public DateTime? GregorianBirthDate { get; set; }
		[Column(Order = 13)]
		public DateTime? HijriBirthDate { get; set; }
		[MaxLength(15)]
		[Column(Order = 14)]
		public string NationalID { get; set; }
		[MaxLength(15)]
		[Column(Order = 15)]
		public string IqamaID { get; set; }
		[MaxLength(15)]
		[Column(Order = 16)]
		public string UserAlternatePhone { get; set; }
		[Display(Name = "BasCountries")]
		[Column(Order = 17)]
		public virtual int? Nationality { get; set; }
		[ForeignKey("CountryID")]
		public virtual BasCountries BasCountries { get; set; }
		[MaxLength(50)]
		public string CreatedBy { get; set; }
		[MaxLength(50)]
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public DateTime ModifiedOn { get; set; } = DateTime.Now;
	}
}
