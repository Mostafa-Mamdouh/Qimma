using AutoMapper;
using PRJ.Application.DTOs;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;

namespace PRJ.Application.Mappings
{
    public class ItemSourceStatusHistoryProfile : Profile
    {
        public ItemSourceStatusHistoryProfile()
        {
            #region ItemSource Status History 
            CreateMap<DTOItemSourceStatusHistoryEditor, ItemSourceStatusHistory>()
             .ForMember(
              dest => dest.StatusId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.StatusId.ToString())))
             .ForMember(
              dest => dest.SourceId,
              opt => opt.MapFrom(src => EncryptQueryURL.Decrypt(src.SourceId.ToString())))
             ;

            CreateMap<ItemSourceStatusHistory, DTOItemSourceStatusHistory>()
             .ForMember(
              dest => dest.Id,
              opt => opt.MapFrom(src => EncryptQueryURL.Encrypt(src.StatusHistoryId.ToString())))
              .ForMember(
              dest => dest.EntryUser,
              opt => opt.MapFrom(src => src.CreatedBy))
             .ForMember(
              dest => dest.CreateDate,
              opt => opt.MapFrom(src => src.CreatedOn.ToString()))
              //.ForMember(
              //dest => dest.FromStateEn,
              //opt => opt.MapFrom(src =>src.ParentStatusId!=null?SourceStatus.InitiatedByFacility.ToString():src.ParentStatus.ItemSourceStatus.StatusNameEn ))
              // .ForMember(
              //dest => dest.FromStateAr,
              //opt => opt.MapFrom(src => src.ParentStatusId != null ? SourceStatus.InitiatedByFacility.ToString() : src.ParentStatus.ItemSourceStatus.StatusNameAr))
              .ForMember(
              dest => dest.ToStateEn,
              opt => opt.MapFrom(src => src.ItemSourceStatus.StatusNameEn))
             .ForMember(
              dest => dest.ToStateAr,
              opt => opt.MapFrom(src => src.ItemSourceStatus.StatusNameAr))
            ;
            #endregion
        }
    }
}
