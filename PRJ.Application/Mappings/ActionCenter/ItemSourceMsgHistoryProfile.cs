using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Application.Enums;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class ItemSourceMsgHistoryProfile : Profile
    {
        public ItemSourceMsgHistoryProfile()
        {
            #region ItemSource Msg History 
              CreateMap<DTOItemSourceMsgHistoryEditor, ItemSourceMsgHistory>()
             .ForMember(
              dest => dest.SourceId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.SourceId.ToString())))
             ;

              CreateMap<ItemSourceMsgHistory, DTOItemSourceMsgHistory>()
             .ForMember(
              dest => dest.SentBy,
              opt => opt.MapFrom(src => src.CreatedBy.ToString()))
            ;
            #endregion
        }
    }
}
