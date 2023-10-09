namespace PRJ.Application.AmanDTOs
{
    public class DTOPermitInventorySealed
    {
        public string SerialNo { get; set; }
        public AmanLookupDto ManufactuerName { get; set; }
        public AmanLookupDto Radionuclide { get; set; }
        public string Model { get; set; }
        public double MaximumRadioactivity { get; set; }
        public AmanLookupDto MaximumRadioactivityUnit { get; set; }

    }
}
