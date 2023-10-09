using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;


namespace PRJ.Application.Mappings.ItemSource
{
    public class EditSubmittedTransactionProfile : Profile
    {
        public EditSubmittedTransactionProfile()
        {
            CreateMap<DTOAddTrnItemSourceHistory, TrnItemSourceHistory>()
                .ForMember(
                    dest => dest.ItemSourceProfileId,
                    opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.ItemSourceProfileId))
                    );
            CreateMap<TrnItemSourceHistory, DTOGetTrnItemSourceHistory>();
        }
    }
}
