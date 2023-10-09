using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalScreen
{
    public class DTOInternalScreenFieldData : BasedDtoEntity
    {
        public DTOInternalScreenFieldData()
        {
            this.InternalFieldPermissions = new HashSet<DTOInternalFieldPermission>();
        }
        public int FieldId { get; set; }
        public string FieldDescAr { get; set; }
        public string FieldDescEn { get; set; }
        public int FieldType { get; set; }
        public string LovId { get; set; }
        public string LookupSetId { get; set; }
        public string FieldTypeEn { get; set; }
        public string FieldTypeAr { get; set; }

        public ICollection<DTOInternalFieldPermission> InternalFieldPermissions { get; set; }
    }
}