using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceRadionulcidesEditor : BasedDtoEntity
    {
        public int? RadionuclideTrnId { get; set; }
        public string Radionuclide { get; set; }
        public string InitialActivityUnit { get; set; }
        public float? initialActivity { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }

    }
}
