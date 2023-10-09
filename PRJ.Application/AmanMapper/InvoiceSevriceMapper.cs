using AutoMapper;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;


namespace PRJ.Application.AmanMapper
{
    public class InvoiceSevriceMapper :Profile
    {
        public InvoiceSevriceMapper()
        {
            CreateMap<dto.Billing.DTOInvoiceTransactionHeader, ent.BillingServiceTrn.BillingServiceTrnHeader>().ReverseMap()
        .ForMember(
        dest => dest.CustomerId,
        opt => opt.MapFrom(src => src.CustomerId))
        .ForMember(
        dest => dest.CustomerName,
        opt => opt.MapFrom(src => src.CustomerName))
        .ForMember(
          dest => dest.InvoiceDate,
          opt => opt.MapFrom(src => src.InvoiceDate ?? DateTime.Now))
        .ForMember(
          dest => dest.PaymentTerms,
          opt => opt.MapFrom(src => int.Parse(src.TermsCode)))
        .ForMember(
          dest => dest.Remarks,
          opt => opt.MapFrom(src => src.TrnRemarks.ToString()));
        //.ForMember(
        //  dest => dest.Remarks,
        //  opt => opt.MapFrom(src => src.BillingServiceTrnDetails..ToString()));


            CreateMap<dto.Billing.DTOInvoiceTransactionHeader, ent.BillingServiceTrn.BillingServiceTrnHeader>().ReverseMap()
          .ForMember(
            dest => dest.CustomerId,
             opt => opt.MapFrom(src => src.CustomerId))
              .ForMember(
           dest => dest.CustomerName,
             opt => opt.MapFrom(src => src.CustomerName))
          .ForMember(
            dest => dest.InvoiceDate,
            opt => opt.MapFrom(src => src.InvoiceDate ?? DateTime.Now))
          .ForMember(
            dest => dest.PaymentTerms,
            opt => opt.MapFrom(src => int.Parse(src.TermsCode)))
          .ForMember(
            dest => dest.Remarks,
            opt => opt.MapFrom(src => src.TrnRemarks.ToString()));
        }

    }
}
