using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class TreeControl
    {
        [Key]
        [Column(Order = 0)]
        public string TreeCode { get; set; }
        [Key]
        [Column(Order = 1)]
        public int LevelNum { get; set; }
        public int? LevelInterval { get; set; }
        public int? LevelLength { get; set; }

        [MaxLength(20)]
        public string LevelPadding { get; set; }

        [MaxLength(100)]
        public string LevelTitleFRN { get; set; }

        [MaxLength(100)]
        public string LevelTitleNTV { get; set; }

        public int? StartingNum { get; set; }
    }
}
