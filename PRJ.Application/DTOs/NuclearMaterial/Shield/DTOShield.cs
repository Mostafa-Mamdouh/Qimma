using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialFiles;

namespace PRJ.Application.DTOs.NuclearMaterial.Shield
{
    public class DTOShield
    {
        public string ManufacturerSerialNo { get; set; }
        public string SourceModel { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }
        public double? InitialMass { get; set; }
        public bool? IsShield { get; set; }
        public string InitialMassUnit { get; set; }
        public string EntityId { get; set; }
        public string FacilityId { get; set; }
        public string LicenseId { get; set; }
        public string PermitdetailsId { get; set; }
        public virtual ICollection<DTONuclearMaterialFiles> NuclearMaterialFiles { get; set; }

    }
}
