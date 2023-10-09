using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.InternalScreen
{
    public class DTOInternalScreen : BasedDtoEntity
    {

        public DTOInternalScreen()
        {
            this.InternalScreenFields = new HashSet<DTOInternalScreenFieldData>();
        }
        public int ScreenId { get; set; }
        public string ScreenNameAr { get; set; }
        public string ScreenNameEn { get; set; }
        public string ScreenCode { get; set; }

        public ICollection<DTOInternalScreenFieldData> InternalScreenFields { get; set; }
    }
}
