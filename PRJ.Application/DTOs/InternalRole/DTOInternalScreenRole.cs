using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalRole
{
    public class DTOInternalScreenRole : BasedDtoEntity
    {
        public int? ScreenRoleId { get; set; }
        public string ScreenNameAr { get; set; }
        public string ScreenNameEn { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int ScreenId { get; set; }
        public int? RoleId { get; set; }
        public string RoleIdValue { get; set; }
        public int? ScreenOrder { get; set; }
        public bool Insert { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Query { get; set; } = false;


    }
}
