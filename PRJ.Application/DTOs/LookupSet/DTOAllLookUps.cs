using PRJ.Application.DTOs.LookupSetTerm;

namespace PRJ.Application.DTOs.LookupSet
{
	public class DTOAllLookUps
	{
		public string Id { get; set; }
		public int LookupSetId { get; set; }
		public string ClassName { get; set; }
		public string DisplayNameAr { get; set; }
		public string DisplayNameEn { get; set; }
		public string ExtraData1 { get; set; }
		public bool IsActive { get; set; }
		public string ExtraData2 { get; set; }
		public virtual List<DTOAllLookupSetTerm> LookupSetTerms { get; set; }
		public int LookupSetTermId { get; set; }

	}
}
