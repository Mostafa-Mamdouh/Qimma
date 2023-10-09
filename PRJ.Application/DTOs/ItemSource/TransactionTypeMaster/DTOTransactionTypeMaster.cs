using PRJ.Application.DTOs.Common;
namespace PRJ.Application.DTOs
{
    public class DTOTransactionTypeMaster : BasedDtoEntity
    {
        public int TransactionType { get; set; }
        public string TCode { get; set; }
        public string TransactionTypeDesFrn { get; set; }
        public string TransactionTypeDesNtv { get; set; }
    }
}
