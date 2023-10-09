using PRJ.Application.DTOs.LookupSetTerm;

namespace PRJ.Application.DTOs
{
    public class DTOGetTrnItemSourceRadionuclides
    {
        public int TrnRadionuclideId { get; set; }
        public float? InitialActivity { get; set; }
        public int? InitialActivityUnit { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }
        public virtual DTOLookupSetTerm InitialActivityUnitLookup { get; set; }
        public int? RadionulcideId { get; set; }
        public int? TrnSourceID { get; set; }
    }
}
