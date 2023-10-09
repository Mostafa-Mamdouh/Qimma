namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseInventorySealed
    {
        public AmanLookupDto Radionuclide { get; set; }
        public int NumberOfSources { get; set; }
        public double MaximumRadioactivity { get; set; }
        public AmanLookupDto MaximumRadioactivityUnit { get; set; }
        public AmanLookupDto SourceUsedIn { get; set; }
        public string PurposeOfUse { get; set; }
    }
}
