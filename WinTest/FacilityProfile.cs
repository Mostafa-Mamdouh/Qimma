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
    
    public partial class FacilityProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacilityProfile()
        {
            this.ItemSourcesProfiles = new HashSet<ItemSourcesProfile>();
            this.LicenseProfiles = new HashSet<LicenseProfile>();
            this.NuclearRelatedItemsProfiles = new HashSet<NuclearRelatedItemsProfile>();
            this.RadiationGeneratorsProfiles = new HashSet<RadiationGeneratorsProfile>();
        }
    
        public int FacilityId { get; set; }
        public string FacilityNameAr { get; set; }
        public string FacilityNameEn { get; set; }
        public string OragnizationName { get; set; }
        public string FacilityCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public Nullable<int> EntityId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> AmanInsertedOn { get; set; }
    
        public virtual EntityProfile EntityProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemSourcesProfile> ItemSourcesProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LicenseProfile> LicenseProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NuclearRelatedItemsProfile> NuclearRelatedItemsProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadiationGeneratorsProfile> RadiationGeneratorsProfiles { get; set; }
    }
}
