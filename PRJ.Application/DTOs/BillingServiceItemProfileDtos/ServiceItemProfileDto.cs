using PRJ.Application.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.DTOs.BillingServiceItemProfileDtos
{
    public class ServiceItemProfileDto : BasedDtoEntity
    {
        public int ServiceItemId { get; set; }
        public string ItemDesFrn { get; set; }
        public string ItemDesNtv { get; set; }
        public string ItemStructureCode { get; set; }
        public decimal CurrentPrice { get; set; }
        public string IssueAccountCode { get; set; }
        public string ItemType { get; set; }
        public string ItemRefCode { get; set; }
        public bool ActiveFlag { get; set; }
        public decimal ItmQty { get; set; }
        public bool MultiStage { get; set; }
        public string LicenseTerm { get; set; }
    }
}
