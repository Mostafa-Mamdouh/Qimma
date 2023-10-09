using PRJ.Application.DTOs.EntityProfile;
using PRJ.Application.DTOs.FacilityProfile;
using PRJ.Application.DTOs.LicenseProfile;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialElement;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialFiles;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialRadionuclide;
using PRJ.Application.DTOs.PermitDetailsProfile;

namespace PRJ.Application.DTOs.NuclearMaterial
{
    public class DTOUpdateNuclearMaterial
    {
        public DTOUpdateNuclearMaterial()
        {
            this.NuclearMaterialFiles = new HashSet<DTONuclearMaterialFiles>();
            this.NuclearMaterialElements = new HashSet<DTONuclearMaterialElement>();
            this.NuclearMaterialRadionulcides = new HashSet<DTONuclearMaterialRadionuclide>();
        }
        public string SourceId { get; set; } // Main id
        public string NrrcId { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string SourceModel { get; set; }
        public string FacilitySerialNo { get; set; }
        public string ChemicalForm { get; set; }
        public bool? IsShield { get; set; }
        public int Status { get; set; }
        public int PhysicalForm { get; set; }
        public int EntityId { get; set; }
        public DTOEntityProfile EntityProfile { get; set; }
        public int FacilityId { get; set; }
        public DTOFacilityProfile FacilityProfile { get; set; }
        public int LicenseId { get; set; }
        public DTOLicenseProfile License { get; set; }
        public int LicenseInventoryId { get; set; }
        public int PermitdetailsId { get; set; }
        public DTOPermitDetailsProfile Permit { get; set; }
        public int PermitInventoryId { get; set; }
        public int PractiseId { get; set; }
        public int ManufacturerId { get; set; }
        public int ManufacturerCountryId { get; set; }
        public int QuantityUnitId { get; set; }
        public int Quantity { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }
        public virtual ICollection<DTONuclearMaterialElement> NuclearMaterialElements { get; set; }
        public virtual ICollection<DTONuclearMaterialFiles> NuclearMaterialFiles { get; set; }
        public virtual ICollection<DTONuclearMaterialRadionuclide> NuclearMaterialRadionulcides { get; set; }
    }
}
