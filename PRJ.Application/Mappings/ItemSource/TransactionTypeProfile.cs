using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class TransactionTypeProfile : Profile
    {
        public TransactionTypeProfile()
        {
            #region Transaction Type Master
            CreateMap<DTOTransactionTypeMaster, TransactionTypeMaster>()
             .ForMember(
              dest => dest.TransactionTypeId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.Id.ToString())));

            CreateMap<TransactionTypeMaster, DTOTransactionTypeMaster>()
             .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TransactionTypeId.ToString())))
             .ForMember(
              dest => dest.TransactionType,
              opt => opt.MapFrom(src => src.TransactionTypeId));
            #endregion
        }
    }
}
