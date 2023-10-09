using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalRole
{
    public class DTOInternalRole : BasedDtoEntity
    {
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public string RoleCode { get; set; }
    }
}
