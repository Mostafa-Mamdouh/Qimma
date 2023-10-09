namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseInventoryGenerator
    {
        public double MaximumVoltage { get; set; }
        public double MaximumTubeCurrent { get; set; }
        public double MaximumEnergy { get; set; }
        public int MaximumNumberOfEquipment { get; set; }
        public AmanLookupDto SourceUsedIn { get; set; }
        public string PurposeOfUse { get; set; }
    }
}
