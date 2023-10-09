using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class TrnItemSourceRadionuclidesProfile : Profile
    {
        public TrnItemSourceRadionuclidesProfile()
        {
            CreateMap<DTOItemSourceRadionulcidesEditor, TrnItemSourceRadionuclides>()
           .ForMember(
            dest => dest.RadionulcideId,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Radionuclide) ? null : EncryptQueryURL.Decrypt(src.Radionuclide.ToString())))
           .ForMember(
            dest => dest.InitialActivityUnit,
            opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Radionuclide) ? null : EncryptQueryURL.Decrypt(src.InitialActivityUnit.ToString())))
           ;


           

        }
    }
}
