namespace PRJ.Application.DTOs
{
    public class DTOGetTransactionAttachments
    {
        public int TrnItemSourceFileId { get; set; }
        public int FileNum { get; set; }
        public string FileName { get; set; }
        public string FileOriginalName { get; set; }
        public int FileSize { get; set; }
        public int AttachmentType { get; set; }
        public string FilePath { get; set; }
        public string FileBytes { get; set; }
        public string ContentType { get; set; }
        public int TrnSourceID { get; set; }
        public bool? ForShield { get; set; }
    }
}
