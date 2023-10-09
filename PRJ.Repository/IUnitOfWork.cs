using PRJ.Domain.Entities;
using PRJ.Repository.DssFiscalYears;
using PRJ.Repository.DssQuarterSetup;
using PRJ.Repository.DssQuarterSetupDetails;
using PRJ.Repository.NuclearMaterial.NuclearElements;
using PRJ.Repository.ServiceEntryFees;

namespace PRJ.Repository
{
    public interface IUnitOfWork
    {
        IDatabaseTransaction BeginTransaction();
        IEntityProfileRepository EntityProfile { get; }
        IBasCountriesRepository BasCountries { get; }
        IBasCitiesRepository BasCities { get; }
        IFacilityProfileRepository FacilityProfile { get; }

        ILegalRepresentativesProfileRepository LegalRepresentativesProfile { get; }
        ILicenseInventoryLimitsRepository LicenseInventoryLimits { get; }

        ILicenseSealedSourcesRepository LicenseSealedSources { get; }
        ILicenseUnSealedSourcesRepository LicenseUnSealedSources { get; }
        ILicenseGeneratorsRepository LicenseGenerators { get; }
        IVUnsealedSourcesRepository VUnsealedSources { get; }

        IDssQuarterSetupRepository DssQuarterSetup { get; }

        ILicenseProfileRepository LicenseProfile { get; }
        ILookupSetRepository LookupSet { get; }
        ILookupSetTermRepository LookupSetTerm { get; }
        IManufacturerMasterRepository ManufacturerMaster { get; }
        INuclearRelatedItemsProfileRepository NuclearRelatedItemsProfile { get; }
        IPermitDetailsProfileRepository PermitDetailsProfile { get; }
        IPermitInventoryLimitsRepository PermitInventoryLimits { get; }

        IPermitSealedSourcesRepository PermitSealedSources { get; }
        IPermitUnSealedSourcesRepository PermitUnSealedSources { get; }
        IPermitGeneratorsRepository PermitGenerators { get; }
        IPermitNuclearSourcesRepository PermitNuclearSources { get; }
        IDssFiscalYearsRepository DssFiscalYears { get; }
        IDssQuarterSetupDetailsRepository DssQuarterSetupDetails { get; }
        IPractiseProfileRepository PractiseProfile { get; }
        IRadiationGeneratorsProfileRepository RadiationGeneratorsProfile { get; }
        ISafetyResponsibleOfficersProfileRepository SafetyResponsibleOfficersProfile { get; }
        IRadionuclideRepository Radionuclides { get; }
        ITrnNuclearRelatedItemsRepository TrnNuclearRelatedItems { get; }
        ITrnRadiationGeneratorsRepository TrnRadiationGenerators { get; }
        IWFTransactionsFlowRepository WFTransactionsFlow { get; }
        IWorkFlowRejectReasonsRepository WorkFlowRejectReasons { get; }
        IWorkFlowStatusRepository WorkFlowStatus { get; }
        ITreeControlRepository TreeControl { get; }
        IItemHierarchyStructureRepository ItemHierarchyStructure { get; }

        IWorkersDosimetersRepository WorkersDosimeters { get; }
        IWorkersExposuresDosesRepository WorkersExposuresDoses { get; }
        IServiceItemProfileReopsitory ServiceItemProfile { get; }

        #region Related Items
        IRelatedItemsRepository RelatedItems { get; }
        IRelatedItemsFilesRepository RelatedItemsFiles { get; }
        IRelatedItemHierarchyRepository RelatedItemHierarchy { get; }
        #endregion
        ICustomerProfileRepository customerProfile { get; }

        #region Screen Management
        IInternalScreenRepository InternalScreen { get; }
        IInternalScreenFieldRepository InternalScreenField { get; }
        IListOfValueRepository ListOfValues { get; }
        IInternalFieldPermissionRepository InternalFieldPermission { get; }

        #endregion       
        #region User Management
        IInternalRoleRepository InternalRole { get; }
        IInternalScreenRoleRepository InternalScreenRole { get; }
        #endregion

        #region Item Source
        ITrnItemSourceRepository TrnItemSource { get; }
        ITransactionHeaderRepository TransactionHeader { get; }
        ITransactionTypeMasterRepository TransactionTypeMaster { get; }
        ITrnItemSourceRadionuclidesRepository TrnItemSourceRadionuclides { get; }
        ITrnItemSourceFilesRepository TrnItemSourceFiles { get; }
        ITrnItemSourceHistoryRepository TrnItemSourceHistory { get; }


        IItemSourceProfileRepository ItemSourceProfile { get; }
        IItemSourceFilesRepository ItemSourceFiles { get; }
        IItemSourceRadionulcidesRepository ItemSourceRadionulcides { get; }
        IItemSourceStatusHistoryRepository ItemSourceStatusHistory { get; }
        IItemSourceStatusRepository ItemSourceStatus { get; }
        IItemSourceMsgHistoryRepository ItemSourceMsgHistory { get; }

        #endregion
        #region NuclearMaterial
        INuclearMaterialFilesRepository NuclearMaterialFiles { get; }
        INuclearMaterialRadionuclideRepository NuclearMaterialRadionuclide { get; }
        INuclearMaterialRepository NuclearMaterial { get; }
        INuclearElementsRepository NuclearElements { get; }
        #endregion
        ICustomerProfileRepository CustomerProfile { get; }

        IBillingServiceTrnHeaderRepository BillingServiceTrnHeader { get; }
        IBillingServiceTrnDetailsRepository BillingServiceTrnDetails { get; }
        IServiceEntryFeesRepository ServiceEntryFees { get; }

        IWorkersRepository Workers { get; }

        Task CompleteAsync();
        void Complete();
    }
}

