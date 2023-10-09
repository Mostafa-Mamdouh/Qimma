using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalScreen
{
    public class DTOInternalFieldPermission : BasedDtoEntity
    {
        public int FieldPermissionId { get; set; }
        public int AttributeId { get; set; }
        public int FieldId { get; set; }
        public string RoleId { get; set; }


    }
}
