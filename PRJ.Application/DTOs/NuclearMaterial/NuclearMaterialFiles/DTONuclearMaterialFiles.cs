using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialFiles
{
    public class DTONuclearMaterialFiles : BasedDtoEntity
    {

        public string FileId { get; set; }
        public string Name { get; set; }
        public int? Size { get; set; }
        public string Base64 { get; set; }
        public string ContentType { get; set; }
        public string AttachmentType { get; set; }
        public string NuclearMaterialId { get; set; }
    }
}
