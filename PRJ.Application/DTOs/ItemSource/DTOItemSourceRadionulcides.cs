using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceRadionulcides : BasedDtoEntity
    {
        public string Radionuclide { get; set; }
        public float DValue { get; set; }
        public string DValueUnit { get; set; }
        public float HalfLife { get; set; }
        public string HalfLifeUnit { get; set; }
        public float InitialActivity { get; set; }
        public string ActivityUnit { get; set; }

    }
}
