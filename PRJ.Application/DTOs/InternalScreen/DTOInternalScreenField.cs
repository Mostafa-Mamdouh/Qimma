using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalScreen
{
    public class DTOInternalScreenField : BasedDtoEntity
    {
        public int FieldId { get; set; }
        public string FieldDescAr { get; set; }
        public string FieldDescEn { get; set; }
        public int FieldType { get; set; }
        public string FieldTypeEn { get; set; }
        public string FieldTypeAr { get; set; }
        public string FieldFormat { get; set; }
        public int? ScreenId { get; set; }
        public string ScreenIdValue { get; set; }
        public string ScreenNameAr { get; set; }
        public string ScreenNameEn { get; set; }
        public int? LookupSetId { get; set; }
        public string ClassName { get; set; }
        public int? LovId { get; set; }
        public string LovNameAr { get; set; }
        public string LovNameEn { get; set; }
    }
}