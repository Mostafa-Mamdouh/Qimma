using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.NuclearMaterial
{
    public class NuclearMaterialFiles : AuditableBasedEntity
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

        #region Navigation Properties
        public int NuclearMaterialId { get; set; }

        [ForeignKey("NuclearMaterialId")]
        public virtual NuclearMaterial NuclearMaterial { get; set; }
        #endregion
    }
}
