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
    
    public partial class EntityProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntityProfile()
        {
            this.FacilityProfiles = new HashSet<FacilityProfile>();
            this.ItemSourcesProfiles = new HashSet<ItemSourcesProfile>();
            this.LicenseProfiles = new HashSet<LicenseProfile>();
            this.NuclearRelatedItemsProfiles = new HashSet<NuclearRelatedItemsProfile>();
            this.RadiationGeneratorsProfiles = new HashSet<RadiationGeneratorsProfile>();
        }
    
        public int EntityId { get; set; }
        public string EntityNameAr { get; set; }
        public string EntityNameEn { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string GovernmentID { get; set; }
        public string EntityType { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> AmanInsertedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacilityProfile> FacilityProfiles { get; set; }
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
