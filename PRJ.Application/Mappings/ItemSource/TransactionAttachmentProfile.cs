using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using System.Text;

namespace PRJ.Application.Mappings
{
    public class TransactionAttachmentProfile : Profile
    {
        public TransactionAttachmentProfile()
        {
            CreateMap<DTOSourceAttachments, TransactionAttachments>()
                 .ForMember(
                  dest => dest.TrnItemSourceFileId,
                  opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Id) ? null : EncryptQueryURL.Decrypt(src.Id.ToString())))
                  .ForMember(
                  dest => dest.FileOriginalName,
                  opt => opt.MapFrom(src => src.Name))
                     .ForMember(
                  dest => dest.FileBytes,
                  opt => opt.MapFrom(src => Encoding.ASCII.GetBytes(src.Base64)))
                  .ForMember(
                  dest => dest.FileName,
                  opt => opt.MapFrom(src => new Random().Next(DateTime.Now.DayOfYear, DateTime.Now.DayOfYear + 100).ToString() + "-" + String.Format(src.Name + "-" + "{0:yyyy-MM-dd_hh-mm-ssss-fff-tt}", DateTime.Now)))
            ;

            CreateMap<TransactionAttachments, DTOSourceAttachments>()
                 .ForMember(
                  dest => dest.Base64,
                  opt => opt.MapFrom(src => Encoding.ASCII.GetString(src.FileBytes)))

                 .ForMember(
                  dest => dest.Id,
                  opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.TrnItemSourceFileId.ToString())));
        }
    }
}
