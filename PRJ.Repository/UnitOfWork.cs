using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Repository.DssFiscalYears;
using PRJ.Repository.DssQuarterSetup;
using PRJ.Repository.DssQuarterSetupDetails;
using PRJ.Repository.NuclearMaterial.NuclearElements;
using PRJ.Repository.ServiceEntryFees;

namespace PRJ.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RepositoryContext _db;
        private readonly ILogger _logger;
        public IDatabaseTransaction BeginTransaction()
        {
            return new DatabaseTransaction(_db);
        }
        public IEntityProfileRepository EntityProfile { get; private set; }
        public IBasCountriesRepository BasCountries { get; private set; }
        public IBasCitiesRepository BasCities { get; private set; }
        public IFacilityProfileRepository FacilityProfile { get; private set; }
        public ILegalRepresentativesProfileRepository LegalRepresentativesProfile { get; private set; }
        public ILicenseInventoryLimitsRepository LicenseInventoryLimits { get; private set; }
        public ILicenseSealedSourcesRepository LicenseSealedSources { get; private set; }
        public ILicenseUnSealedSourcesRepository LicenseUnSealedSources { get; private set; }
        public ILicenseGeneratorsRepository LicenseGenerators { get; private set; }
        public IVUnsealedSourcesRepository VUnsealedSources { get; private set; }
        public IDssQuarterSetupDetailsRepository DssQuarterSetupDetails { get; private set; }

        public ILicenseProfileRepository LicenseProfile { get; private set; }
        public ILookupSetRepository LookupSet { get; private set; }
        public ILookupSetTermRepository LookupSetTerm { get; private set; }
        public IManufacturerMasterRepository ManufacturerMaster { get; private set; }
        public INuclearRelatedItemsProfileRepository NuclearRelatedItemsProfile { get; private set; }
        public IPermitDetailsProfileRepository PermitDetailsProfile { get; private set; }
        public IPermitInventoryLimitsRepository PermitInventoryLimits { get; private set; }
        public IPermitSealedSourcesRepository PermitSealedSources { get; private set; }
        public IPermitUnSealedSourcesRepository PermitUnSealedSources { get; private set; }
        public IPermitGeneratorsRepository PermitGenerators { get; private set; }
        public IPermitNuclearSourcesRepository PermitNuclearSources { get; private set; }
        public IPractiseProfileRepository PractiseProfile { get; private set; }
        public IRadiationGeneratorsProfileRepository RadiationGeneratorsProfile { get; private set; }
        public ISafetyResponsibleOfficersProfileRepository SafetyResponsibleOfficersProfile { get; private set; }
        public IRadionuclideRepository Radionuclides { get; private set; }

        public ITrnNuclearRelatedItemsRepository TrnNuclearRelatedItems { get; private set; }
        public ITrnRadiationGeneratorsRepository TrnRadiationGenerators { get; private set; }
        public IWFTransactionsFlowRepository WFTransactionsFlow { get; private set; }
        public IWorkFlowRejectReasonsRepository WorkFlowRejectReasons { get; private set; }
        public IWorkFlowStatusRepository WorkFlowStatus { get; private set; }
        public IWorkersRepository Workers { get; private set; }
        public ITreeControlRepository TreeControl { get; private set; }
        public IItemHierarchyStructureRepository ItemHierarchyStructure { get; private set; }
        public IServiceItemProfileReopsitory ServiceItemProfile { get; private set; }
        public IWorkersDosimetersRepository WorkersDosimeters { get; private set; }
        public IWorkersExposuresDosesRepository WorkersExposuresDoses { get; private set; }

        #region Related Items
        public IRelatedItemsRepository RelatedItems { get; private set; }
        public IRelatedItemsFilesRepository RelatedItemsFiles { get; private set; }
        public IRelatedItemHierarchyRepository RelatedItemHierarchy { get; private set; }
        #endregion

        public ICustomerProfileRepository customerProfile { get; private set; }
        public IBillingServiceTrnHeaderRepository BillingServiceTrnHeader { get; private set; }
        public IBillingServiceTrnDetailsRepository BillingServiceTrnDetails { get; private set; }
        public IServiceEntryFeesRepository ServiceEntryFees { get; private set; }
        #region Screen Management
        public IInternalScreenRepository InternalScreen { get; private set; }
        public IInternalScreenFieldRepository InternalScreenField { get; private set; }
        public IListOfValueRepository ListOfValues { get; private set; }
        public IInternalFieldPermissionRepository InternalFieldPermission { get; private set; }
        #endregion
        #region User Management
        public IInternalRoleRepository InternalRole { get; private set; }
        public IInternalScreenRoleRepository InternalScreenRole { get; private set; }
        #endregion
        #region Item Source
        public ITrnItemSourceRepository TrnItemSource { get; private set; }
        public ITransactionHeaderRepository TransactionHeader { get; private set; }
        public ITransactionTypeMasterRepository TransactionTypeMaster { get; private set; }
        public ITrnItemSourceRadionuclidesRepository TrnItemSourceRadionuclides { get; private set; }
        public ITrnItemSourceFilesRepository TrnItemSourceFiles { get; private set; }
        public ITrnItemSourceHistoryRepository TrnItemSourceHistory { get; private set; }


        public IItemSourceProfileRepository ItemSourceProfile { get; private set; }
        public IItemSourceFilesRepository ItemSourceFiles { get; private set; }
        public IItemSourceRadionulcidesRepository ItemSourceRadionulcides { get; private set; }
        public IItemSourceStatusHistoryRepository ItemSourceStatusHistory { get; private set; }
        public IItemSourceStatusRepository ItemSourceStatus { get; private set; }
        public IItemSourceMsgHistoryRepository ItemSourceMsgHistory { get; private set; }

        #endregion

        #region NuclearMaterial
        public INuclearMaterialFilesRepository NuclearMaterialFiles { get; private set; }
        public INuclearMaterialRadionuclideRepository NuclearMaterialRadionuclide { get; private set; }
        public INuclearMaterialRepository NuclearMaterial { get; private set; }
        public INuclearElementsRepository NuclearElements { get; private set; }
        #endregion

        public ICustomerProfileRepository CustomerProfile { get; private set; }

        public IDssQuarterSetupRepository DssQuarterSetup { get; private set; }

        public IDssFiscalYearsRepository DssFiscalYears { get; private set; }

        public UnitOfWork(
            RepositoryContext db,
            ILoggerFactory loggerFactory
        )
        {
            _db = db;
            _logger = loggerFactory.CreateLogger("logs");


            EntityProfile = new EntityProfileRepository(db, _logger);
            BasCountries = new BasCountriesRepository(db, _logger);
            BasCities = new BasCitiesRepository(db, _logger);
            FacilityProfile = new FacilityProfileRepository(db, _logger);
            LegalRepresentativesProfile = new LegalRepresentativesProfileRepository(db, _logger);
            LicenseInventoryLimits = new LicenseInventoryLimitsRepository(db, _logger);
            DssFiscalYears = new DssFiscalYearsRepository(db, _logger);
            DssQuarterSetup = new DssQuarterSetupRepository (db, _logger);
            LicenseSealedSources = new LicenseSealedSourcesRepository(db, _logger);
            LicenseUnSealedSources = new LicenseUnSealedSourcesRepository(db, _logger);
            LicenseGenerators = new LicenseGeneratorsRepository(db, _logger);
            VUnsealedSources = new VUnsealedSourcesRepository(db, _logger);
            DssQuarterSetupDetails = new DssQuarterSetupDetailsRepository(db, _logger);


            LicenseProfile = new LicenseProfileRepository(db, _logger);
            LookupSet = new LookupSetRepository(db, _logger);
            LookupSetTerm = new LookupSetTermRepository(db, _logger);
            ManufacturerMaster = new ManufacturerMasterRepository(db, _logger);
            NuclearRelatedItemsProfile = new NuclearRelatedItemsProfileRepository(db, _logger);
            PermitDetailsProfile = new PermitDetailsProfileRepository(db, _logger);
            PermitInventoryLimits = new PermitInventoryLimitsRepository(db, _logger);
            PermitSealedSources = new PermitSealedSourcesRepository(db, _logger);
            PermitUnSealedSources = new PermitUnSealedSourcesRepository(db, _logger);
            PermitGenerators = new PermitGeneratorsRepository(db, _logger);
            PermitNuclearSources = new PermitNuclearSourcesRepository(db, _logger);
            PractiseProfile = new PractiseProfileRepository(db, _logger);
            RadiationGeneratorsProfile = new RadiationGeneratorsProfileRepository(db, _logger);
            SafetyResponsibleOfficersProfile = new SafetyResponsibleOfficersProfileRepository(db, _logger);
            Radionuclides = new RadionuclideRepository(db, _logger);
            EntityProfile = new EntityProfileRepository(db, _logger);
            BasCountries = new BasCountriesRepository(db, _logger);
            BasCities = new BasCitiesRepository(db, _logger);
            FacilityProfile = new FacilityProfileRepository(db, _logger);
            LegalRepresentativesProfile = new LegalRepresentativesProfileRepository(db, _logger);


            TrnNuclearRelatedItems = new TrnNuclearRelatedItemsRepository(db, _logger);
            TrnRadiationGenerators = new TrnRadiationGeneratorsRepository(db, _logger);
            WFTransactionsFlow = new WFTransactionsFlowRepository(db, _logger);
            WorkFlowRejectReasons = new WorkFlowRejectReasonsRepository(db, _logger);
            WorkFlowStatus = new WorkFlowStatusRepository(db, _logger);

            customerProfile = new CustomerProfileRepository(db, _logger);
            #region Screen MAnagement
            InternalScreen = new InternalScreenRepository(db, _logger);
            InternalScreenField = new InternalScreenFieldRepository(db, _logger);
            ListOfValues = new ListOfValueRepository(db, _logger);
            InternalFieldPermission = new InternalFieldPermissionRepository(db, _logger);
            #endregion

            #region User Management
            InternalRole = new InternalRoleRepository(db, _logger);
            InternalScreenRole = new InternalScreenRoleRepository(db, _logger);
            #endregion
            #region Item Source
            TrnItemSource = new TrnItemSourceRepository(db, _logger);
            TransactionHeader = new TransactionHeaderRepository(db, _logger);
            TransactionTypeMaster = new TransactionTypesMasterRepository(db, _logger);
            TrnItemSourceRadionuclides = new TrnItemSourceRadionuclidesRepository(db, _logger);
            TrnItemSourceFiles = new TrnItemSourcesFilesRepository(db, _logger);
            TrnItemSourceHistory = new TrnItemSourceHistoryRepository(db, _logger);

            ItemSourceProfile = new ItemSourceProfileRepository(db, _logger);
            ItemSourceFiles = new ItemSourceFilesRepository(db, _logger);
            ItemSourceRadionulcides = new ItemSourceRadionulcidesRepository(db, _logger);
            ItemSourceStatusHistory = new ItemSourceStatusHistoryRepository(db, _logger);
            ItemSourceStatus = new ItemSourceStatusRepository(db, _logger);
            ItemSourceMsgHistory = new ItemSourceMsgHistoryRepository(db, _logger);
            #endregion

            TrnNuclearRelatedItems = new TrnNuclearRelatedItemsRepository(db, _logger);
            TrnRadiationGenerators = new TrnRadiationGeneratorsRepository(db, _logger);
            WFTransactionsFlow = new WFTransactionsFlowRepository(db, _logger);
            WorkFlowRejectReasons = new WorkFlowRejectReasonsRepository(db, _logger);
            WorkFlowStatus = new WorkFlowStatusRepository(db, _logger);
            Workers = new WorkersRepository(db, _logger);
            TreeControl = new TreeControlRepository(db, _logger);
            ItemHierarchyStructure = new ItemHierarchyStructureRepository(db, _logger);
            ServiceItemProfile = new ServiceItemProfileReopsitory(db, _logger);
            TrnNuclearRelatedItems = new TrnNuclearRelatedItemsRepository(db, _logger);
            TrnRadiationGenerators = new TrnRadiationGeneratorsRepository(db, _logger);
            WFTransactionsFlow = new WFTransactionsFlowRepository(db, _logger);
            WorkFlowRejectReasons = new WorkFlowRejectReasonsRepository(db, _logger);
            WorkFlowStatus = new WorkFlowStatusRepository(db, _logger);
            NuclearMaterialFiles = new NuclearMaterialFilesRepository(db, _logger);
            NuclearMaterialRadionuclide = new NuclearMaterialRadionuclideRepository(db, _logger);
            NuclearMaterial = new NuclearMaterialRepository(db, _logger);
            WorkersDosimeters = new WorkersDosimetersRepository(db, _logger);
            WorkersExposuresDoses = new WorkersExposuresDosesRepository(db, _logger);


            BillingServiceTrnHeader = new BillingServiceTrnHeaderRepository(db, _logger);
            BillingServiceTrnDetails = new BillingServiceTrnDetailsRepository(db, _logger);
            ServiceEntryFees = new ServiceEntryFeesRepository(db, _logger);

            NuclearMaterialFiles = new NuclearMaterialFilesRepository(db, _logger);
            NuclearMaterialRadionuclide = new NuclearMaterialRadionuclideRepository(db, _logger);
            NuclearMaterial = new NuclearMaterialRepository(db, _logger);
            NuclearElements = new NuclearElementsRepository(db, _logger);
            #region Related Items
            RelatedItems = new RelatedItemsRepository(db, _logger);
            RelatedItemsFiles = new RelatedItemsFilesRepository(db, _logger);
            RelatedItemHierarchy = new RelatedItemHierarchyRepository(db, _logger);
            #endregion

        }

        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Complete()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
