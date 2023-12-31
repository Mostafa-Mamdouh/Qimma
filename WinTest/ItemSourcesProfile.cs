//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemSourcesProfile
    {
        public int SourceId { get; set; }
        public string SourceDescAr { get; set; }
        public string SourceDescEn { get; set; }
        public string NrrcID { get; set; }
        public string Status { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public string RadioNuclides { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public int IsoptopHalfLifeTime { get; set; }
        public string IspotopHalfLifeTimeUnit { get; set; }
        public string InitialActivity { get; set; }
        public string InitialActivityUnit { get; set; }
        public string CurrentActivity { get; set; }
        public string CurrentActivityUnit { get; set; }
        public string PhysicalForm { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public string Exemption { get; set; }
        public int RecommendedWorkingLifeTime { get; set; }
        public string RadioactiveSourceid { get; set; }
        public string ShieldMaterial { get; set; }
        public string ShieldNuclearMaterialCode { get; set; }
        public string Purpose { get; set; }
        public int NumberOfAcquiredSources { get; set; }
        public int NumberOfConsumedSources { get; set; }
        public int NumberOfAvailableSources { get; set; }
        public string NuclearMaterial { get; set; }
        public int WeightOfNuclearMaterial { get; set; }
        public string EnrichmentofU235 { get; set; }
        public string WeightOffIssileisotopes { get; set; }
        public string WeightOffIssileisotopesunit { get; set; }
        public string ChemicalForm { get; set; }
        public string ChemicalComposition { get; set; }
        public int ContainerVolume { get; set; }
        public string SourceActivity { get; set; }
        public string SourceModel { get; set; }
        public string AssociatedEquipment { get; set; }
        public string SourceCategory { get; set; }
        public int SourceLinkedFlag { get; set; }
        public Nullable<int> EntityId { get; set; }
        public Nullable<int> FacilityId { get; set; }
        public Nullable<int> LicenseId { get; set; }
        public Nullable<int> LicenseInventoryId { get; set; }
        public Nullable<int> PermitdetailsId { get; set; }
        public Nullable<int> PermitInventoryId { get; set; }
        public Nullable<int> PractiseId { get; set; }
        public Nullable<int> SROId { get; set; }
        public Nullable<int> LegalRepresentativesId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> ManufactureCountryId { get; set; }
        public Nullable<int> ItemTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual BasCountry BasCountry { get; set; }
        public virtual EntityProfile EntityProfile { get; set; }
        public virtual FacilityProfile FacilityProfile { get; set; }
        public virtual LegalRepresentativesProfile LegalRepresentativesProfile { get; set; }
        public virtual LicenseInventoryLimit LicenseInventoryLimit { get; set; }
        public virtual LicenseProfile LicenseProfile { get; set; }
        public virtual LookupSet LookupSet { get; set; }
        public virtual ManufacturerMaster ManufacturerMaster { get; set; }
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
        public virtual PermitInventoryLimit PermitInventoryLimit { get; set; }
        public virtual PractiseProfile PractiseProfile { get; set; }
        public virtual SafetyResponsibleOfficersProfile SafetyResponsibleOfficersProfile { get; set; }
    }
}
