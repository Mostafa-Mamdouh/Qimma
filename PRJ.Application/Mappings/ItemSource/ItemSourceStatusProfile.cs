using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class ItemSourceStatusProfile : Profile
    {
        public ItemSourceStatusProfile()
        {
            #region Transaction Type Master
            CreateMap<ItemSourceStatus, DTOItemSourceStatus>()
             .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.ItemSourceStatusId.ToString())));
            #endregion
        }
    }
}
