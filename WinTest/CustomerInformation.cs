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
    
    public partial class CustomerInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerInformation()
        {
            this.Memberships = new HashSet<Membership>();
        }
    
        public int Id { get; set; }
        public string ContactGuid { get; set; }
        public string AccountGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string MobileNumberContact { get; set; }
        public string EmailId { get; set; }
        public string EmailIdContact { get; set; }
        public string Address { get; set; }
        public Nullable<int> CityCode { get; set; }
        public string CityNameOutSideSaudi { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostal { get; set; }
        public Nullable<int> CountryCode { get; set; }
        public Nullable<int> CategoryCode { get; set; }
        public Nullable<int> SubCategoryCode { get; set; }
        public Nullable<int> CustomerType { get; set; }
        public string CompanyNameAR { get; set; }
        public string CompanyNameEN { get; set; }
        public string IndustrialLicenseNumber { get; set; }
        public Nullable<System.DateTime> IndustrialLicenseExpiryDate { get; set; }
        public string CRNumber { get; set; }
        public Nullable<System.DateTime> CRExpiryDate { get; set; }
        public Nullable<bool> IsMember { get; set; }
        public string MembershipNumber { get; set; }
        public string MembershipGUID { get; set; }
        public string MembershipRequestGUID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsMigrated { get; set; }
        public Nullable<System.DateTime> MigratedOn { get; set; }
        public string MigratedBy { get; set; }
        public Nullable<bool> Published { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}