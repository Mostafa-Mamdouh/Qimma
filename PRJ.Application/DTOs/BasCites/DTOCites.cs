using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.BasCites
{
	public class DTOCites : BasedDtoEntity
	{
		public string CountryId { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string CityAbbrCode { get; set; }

	}
}
