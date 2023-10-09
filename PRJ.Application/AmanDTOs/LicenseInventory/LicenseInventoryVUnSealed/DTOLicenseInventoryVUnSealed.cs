namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseInventoryVUnSealed
    {
        public AmanLookupDto Radionuclide { get; set; }
        public AmanLookupDto UnsealedSourceType { get; set; }
        public int NumberOfSources { get; set; }
        public AmanLookupDto PhysicalForm { get; set; }
        public double MaximumRadioactivityPerSource { get; set; }
        public AmanLookupDto MaximumRadioactivityPerSourceUnit { get; set; }
        public double MaximumRadioactivityInTheWorkPlace { get; set; }
        public AmanLookupDto MaximumRadioactivityInTheWorkPlaceUnit { get; set; }
        public AmanLookupDto SourceUsedIn { get; set; }
        public string PurposeOfUse { get; set; }
    }
}
