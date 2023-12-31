﻿using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceEditor : BasedDtoEntity
    {
        public DTOItemSourceEditor()
        {
            this.SourceAttachments = new List<DTOSourceAttachments>();
            this.ShieldAttachments = new List<DTOSourceAttachments>();
            this.Radionuclides = new List<DTOItemSourceRadionulcidesEditor>();
        }
        public string NrrcId { get; set; }////Generated By the System
        public int TransactionType { get; set; }
        public string Entity { get; set; }
        public string Facility { get; set; }
        public string LicenseNumber { get; set; }
        public string PermitNumber { get; set; }
        public string SourceType { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerCountry { get; set; }
        public string Status { get; set; }
        public string FacilitySourceID { get; set; }
        public string AssociatedEquipment { get; set; }
        public string RecommendedWorkingLifeTime { get; set; }// calculated
        public string CurrentActivity { get; set; }// calculated
        public string SourceCategory { get; set; }// calculated
        public List<DTOSourceAttachments> SourceAttachments { get; set; }
        public List<DTOItemSourceRadionulcidesEditor> Radionuclides { get; set; }
        public string ShieldNuclearMaterialCode { get; set; }
        public int? InitialMass { get; set; }
        public string InitialMassUnit { get; set; }
        public List<DTOSourceAttachments> ShieldAttachments { get; set; }
        public string PhysicalForm { get; set; }
        public int? Quantity { get; set; }
        public bool IsShieldDU { get; set; }
        public double? SourceActivity { get; set; }
        public string SourceActivityUnit { get; set; }
        public int? SourceStatus { get; set; }
        public string SourceModel { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }
        public string TransactionHeaderId { get; set; }
    }
}
