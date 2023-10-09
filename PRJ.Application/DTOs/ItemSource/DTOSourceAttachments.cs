using PRJ.Application.DTOs.Common;
namespace PRJ.Application.DTOs
{
    public class DTOSourceAttachments : BasedDtoEntity
    {
        public string FileSourceID { get; set; }
        public string Name { get; set; }
        public int? Size { get; set; }
        public string Base64 { get; set; }
        public string ContentType { get; set; }
        public string AttachmentType { get; set; }
        public bool? ForShield { get; set; }
    }
}
