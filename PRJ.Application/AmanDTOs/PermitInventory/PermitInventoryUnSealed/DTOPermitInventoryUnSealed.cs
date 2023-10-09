namespace PRJ.Application.AmanDTOs
{
    public class DTOPermitInventoryUnSealed
    {
        public string SerialNo { get; set; }
        public AmanLookupDto ManufactuerName { get; set; }
        public AmanLookupDto Radionuclide { get; set; }
        public AmanLookupDto PhysicalForm { get; set; }
        public double CurrentActivity { get; set; }
        public AmanLookupDto CurrentActivityUnit { get; set; }
    }
}
