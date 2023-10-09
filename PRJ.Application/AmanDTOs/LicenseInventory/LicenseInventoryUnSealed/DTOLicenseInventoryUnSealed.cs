namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseInventoryUnSealed
    {
        public AmanLookupDto Radionuclide { get; set; }
        public int NumberOfSources { get; set; }
        public AmanLookupDto PhysicalForm { get; set; }
        public double MaximumRadioactivity { get; set; }
        public AmanLookupDto SourceUsedIn { get; set; }
        public string PurposeOfUse { get; set; }
    }
}
