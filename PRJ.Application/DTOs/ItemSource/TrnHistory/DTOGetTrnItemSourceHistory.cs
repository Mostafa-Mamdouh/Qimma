namespace PRJ.Application.DTOs
{
    public class DTOGetTrnItemSourceHistory
    {
        public int TrnHistoryId { get; set; }
        public string TransactionCode { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Remarks { get; set; }
        public int? ItemSourceProfileId { get; set; }
        public int TransactionTypeId { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
    }
}
