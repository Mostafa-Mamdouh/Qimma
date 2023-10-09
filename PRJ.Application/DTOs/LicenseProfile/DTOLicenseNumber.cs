using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.LicenseProfile
{
    public class DTOLicenseNumber : BasedDtoEntity
    {
        public string LicenseVersionNumber { get; set; }

    }
}
