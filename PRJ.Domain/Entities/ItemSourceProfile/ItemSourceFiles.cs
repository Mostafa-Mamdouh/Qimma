using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class ItemSourceFiles : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemSourceFileId { get; set; }
        public int FileNum { get; set; }
        public string FileName { get; set; }
        public string FileOriginalName { get; set; }
        public int FileSize { get; set; }
        public int AttachmentType { get; set; }
        public string FilePath { get; set; }
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }
        public bool? ForShield { get; set; }

        #region Navigation Properties
        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual ItemSourceProfile ItemSourcesProfile { get; set; }

        #endregion
    }
}
