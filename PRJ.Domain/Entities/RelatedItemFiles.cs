using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class RelatedItemFiles : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }
        [MaxLength(450)]
        public string RelatedItemCode { get; set; }
        [ForeignKey("RelatedItemCode")]
        public virtual RelatedItem RelatedItem { get; set; }
        [MaxLength(300)]
        public string FileName { get; set; }
        [MaxLength(300)]
        public string FileOriginalName { get; set; }
        public int AttachmentType { get; set; }
        public int FileSize { get; set; }
        [MaxLength(4000)]
        public string FilePath { get; set; }
        public byte[] FileBytes { get; set; }
        [MaxLength(200)]
        public string ContentType { get; set; }
    }
}
