namespace PRJ.Application.AmanDTOs
{
    public class DTOPermitInventoryGenerator
    {
        public string SerialNo { get; set; }
        public AmanLookupDto ManufactuerName { get; set; }
        public AmanLookupDto EquipmentType { get; set; }
        public double MaximumVoltage { get; set; }
        public double MaximumTupeCurrent { get; set; }
        public double MaximumEnergy { get; set; }

    }
}
