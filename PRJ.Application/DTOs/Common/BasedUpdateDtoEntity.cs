namespace PRJ.Application.DTOs.Common
{
    public class BasedUpdateDtoEntity
    {
        public string Id { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
    }
}
