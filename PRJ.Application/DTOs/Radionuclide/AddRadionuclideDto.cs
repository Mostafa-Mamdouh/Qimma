using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.Radionuclide
{
    public class AddRadionuclideDto : BasedAddDtoEntity
    {
        public string Isotop { get; set; }
        public string DisplayName { get; set; }
        public float HalfLife { get; set; }
        public string HalfLifeUnitId { get; set; }
        //public float InitialActivity { get; set; }
        //public string ActivityUnit { get; set; }
        public float ExemptionValue { get; set; }
        public string ExemptionValueUnitId { get; set; }
        public bool IsActive { get; set; }
    }
}
