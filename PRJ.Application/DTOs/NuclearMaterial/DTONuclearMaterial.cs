using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialElement;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialFiles;

namespace PRJ.Application.DTOs.NuclearMaterial
{
    public class DTONuclearMaterial : BasedDtoEntity
    {
        public DTONuclearMaterial()
        {
            this.NuclearMaterialFiles = new HashSet<DTONuclearMaterialFiles>();
            this.NuclearMaterialElements = new HashSet<DTONuclearMaterialElement>();
        }
        public string SourceId { get; set; }
        public string NrrcId { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public string ChemicalForm { get; set; }
        public bool? IsShield { get; set; }
        public string SourceModel { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string PhysicalForm { get; set; }
        public string EntityId { get; set; }
        public string FacilityId { get; set; }
        public string LicenseId { get; set; }
        public string LicenseInventoryId { get; set; }
        public string PermitdetailsId { get; set; }
        public string PermitInventoryId { get; set; }
        public string PractiseId { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerCountryId { get; set; }
        public string QuantityUnitId { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }
        public virtual ICollection<DTONuclearMaterialElement> NuclearMaterialElements { get; set; }
        public virtual ICollection<DTONuclearMaterialFiles> NuclearMaterialFiles { get; set; }
    }
}
