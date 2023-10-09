using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOAddTrnItemSourceHistory : BasedDtoEntity
    {
        public string ItemSourceProfileId { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }
        public int TransactionAttribute { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Remarks { get; set; }
    }
}
