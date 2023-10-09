using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTORelatedItemsFiles : BasedDtoEntity
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public int? Size { get; set; }
        public string Base64 { get; set; }
        public string ContentType { get; set; }
        public string AttachmentType { get; set; }
        public string RelatedItemCode { get; set; }

    }
}
