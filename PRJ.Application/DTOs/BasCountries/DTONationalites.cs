using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.BasCountries
{
	public class DTONationalites : BasedDtoEntity
	{
		public string CountryCodeISO { get; set; }
		public string NationalityNameFrn { get; set; }
		public string NationalityNameNtv { get; set; }
	}
}
