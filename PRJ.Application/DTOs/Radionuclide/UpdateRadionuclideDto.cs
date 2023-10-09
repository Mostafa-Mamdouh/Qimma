using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.Radionuclide
{
    public class UpdateRadionuclideDto : BasedUpdateDtoEntity
    {
        public int RadionuclideId { get; set; }
        public string Isotop { get; set; }
        public string DisplayName { get; set; }
        public float HalfLife { get; set; }
        public string HalfLifeUnit { get; set; }
        public float InitialActivity { get; set; }
        public string ActivityUnit { get; set; }
        public float ExemptionValue { get; set; }
        public string ExemptionValueUnit { get; set; }
        public bool IsActive { get; set; }
    }
}
