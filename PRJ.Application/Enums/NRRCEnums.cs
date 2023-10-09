namespace PRJ.Application.Enums
{
    public enum ExternalUserTypes
    {
        LegalRepresentativesProfile = 1,
        SafetyResponsibleOfficersProfile = 2,
    }

    public enum LookUpSetClassType
    {
        Status = 1,
        AssociatedEquipment = 2,
        ShieldingMaterial = 3,
        RecommendedWorkingLifetime = 4
    }

    public enum TransactionType
    {
        NewSourceFromImport = 1,
        NewSourceFromTransfer = 2,
        ChangeFacilityId = 3,
        ChangeAssociatedEquipment = 4,
        ChangeSourceShield = 5,
        ChangeSourcePrimaryData = 6,
        ChangeSourceStatus = 7,
        ChangeQuantity = 1001,
    }

    public enum TrnSourceStatus
    {
        Draft = 1,
        Submit = 2
    }

    public enum SourceStatus
    {
        InitiatedByFacility = 1,
        LicensingDepartmentOnHold = 2,
        LicensingDepartmentRejected = 3,
        LicensingDepartmentApproved = 4,
        NuclearingDepartmentOnHold = 5,
        NuclearingDepartmentRejected = 6,
        NuclearingDepartmentApproved = 7

    }

    public enum CashingType
    {
        TransactionTypeMaster = 1,
        ItemSourceStatus = 2,
        Lookups = 3,
        Radionuclides = 4,
        SourceFormsLookups = 5
    }


    public enum Language
    {
        En = 1,
        Ar = 2
    }

    public enum SourceType
    {
        SealedSource = 104,
        UnsealedSource = 105,
        VariableUnsealed = 106,
    }

    public enum AttachmentsTypes
    {
        manufacturerCertificate = 1,
        customImportPermits = 2,
        customExportPermits = 3,
        shipperImportPermits = 4,
        shipperExportPermits = 5,
        otherDocumnets = 6,
        images = 7,
        charCertificates = 8,
        sourceTagImage = 9,
    }

    public enum TransactionAttributes
    {
        status = 1,
        facilitySourceId = 2,
        associatedEquipment = 3,
        shield = 4,
        quantity = 5
    }

    public enum TreeControlValues
    {
        RelatedItem = 1,
        Billing = 2
    }
}