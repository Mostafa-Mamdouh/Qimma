using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialElement;

namespace PRJ.Application.DTOs.NuclearMaterial
{
    public class DTOPaginatedNuclearMaterials : BasedDtoEntity
    {
        public DTOPaginatedNuclearMaterials()
        {
            this.NuclearMaterialTypeAr = new List<DTONuclearMaterialElement>();
            this.NuclearMaterialTypeEn = new List<DTONuclearMaterialElement>();
        }

        public string SourceId { get; set; }
        public string NrrcId { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }
        public string Purpose { get; set; }
        public double? InitialMass { get; set; }
        public string ChemicalForm { get; set; }
        public bool? IsShield { get; set; }
        public string StatusAr { get; set; }
        public string StatusEn { get; set; }
        public string PhysicalFormAr { get; set; }
        public string PhysicalFormEn { get; set; }
        public List<DTONuclearMaterialElement> NuclearMaterialTypeAr { get; set; }
        public List<DTONuclearMaterialElement> NuclearMaterialTypeEn { get; set; }
        public string InitialMassUnitAr { get; set; }
        public string InitialMassUnitEn { get; set; }
        public string EntityAr { get; set; }
        public string EntityEn { get; set; }
        public string FacilityAr { get; set; }
        public string FacilityEn { get; set; }
        public string License { get; set; }
        public string Permit { get; set; }
        public string ManufacturerAr { get; set; }
        public string ManufacturerEn { get; set; }
        public string SourceModel { get; set; }
    }
}
