using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Entities;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities;
using PRJ.Domain.Entities.Billing;
using PRJ.Domain.Entities.BillingServiceTrn;
using PRJ.Domain.Entities.NuclearMaterial;
using ent = PRJ.Domain.Entities;

namespace PRJ.DataAccess.MSSQL
{
    public class RepositoryContext : IdentityDbContext<ExternalMaserUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }
        public DbSet<ent.BasCountries> BasCountries { get; set; }
        public DbSet<ent.BasCities> BasCities { get; set; }
        public DbSet<ent.EntityProfile> EntityProfile { get; set; }
        public DbSet<ent.FacilityProfile> FacilityProfile { get; set; }
        public DbSet<ent.LegalRepresentativesProfile> LegalRepresentativesProfile { get; set; }
        public DbSet<LicenseInventoryLimits> LicenseInventoryLimits { get; set; }
        public DbSet<LicenseSealedSources> LicenseSealedSources { get; set; }
        public DbSet<LicenseUnSealedSources> LicenseUnSealedSources { get; set; }
        public DbSet<VUnsealedSources> LicenseVUnsealedSources { get; set; }
        public DbSet<LicenseGenerators> LicenseGenerators { get; set; }


        public DbSet<ent.LicenseProfile> LicenseProfile { get; set; }
        public DbSet<ent.LookupSet> LookupSet { get; set; }
        public DbSet<ent.LookupSetTerm> LookupSetTerm { get; set; }
        public DbSet<ent.ManufacturerMaster> ManufacturerMaster { get; set; }
        public DbSet<ent.NuclearRelatedItemsProfile> NuclearRelatedItemsProfile { get; set; }
        public DbSet<ent.PermitDetailsProfile> PermitDetailsProfile { get; set; }
        public DbSet<PermitInventoryLimits> PermitInventoryLimits { get; set; }
        public DbSet<PermitSealedSources> PermitSealedSources { get; set; }
        public DbSet<PermitUnSealedSources> PermitUnSealedSources { get; set; }
        public DbSet<PermitNuclearSources> PermitNuclearSources { get; set; }
        public DbSet<PermitGenerators> PermitGenerators { get; set; }

        public DbSet<ent.PractiseProfile> PractiseProfile { get; set; }
        public DbSet<ent.RadiationGeneratorsProfile> RadiationGeneratorsProfile { get; set; }
        public DbSet<ent.SafetyResponsibleOfficersProfile> SafetyResponsibleOfficersProfile { get; set; }
        public DbSet<ent.Radionuclides> Radionuclides { get; set; }
        public DbSet<ent.WorkersDosimeters> WorkersDosimeters { get; set; }
        public DbSet<ent.DssQuarterSetup> DssQuarterSetup { get; set; }
        public DbSet<ent.DssFiscalYears> DssFiscalYears { get; set; }
        public DbSet<ent.LicenseProfilePractice> LicenseProfilePractice { get; set; }




        #region ItemSourceProfile
        public DbSet<ItemSourceFiles> ItemSourceFiles { get; set; }
        public DbSet<ItemSourceProfile> ItemSourceProfile { get; set; }
        public DbSet<ItemSourceRadionulcides> ItemSourceRadionulcides { get; set; }
        public DbSet<ItemSourceStatusHistory> ItemSourceStatusHistory { get; set; }
        public DbSet<ItemSourceStatus> ItemSourceStatus { get; set; }
        public DbSet<ItemSourceMsgHistory> ItemSourceMsgHistory { get; set; }

        #endregion
        #region TrnItemSources
        public DbSet<TrnItemSource> TrnItemSource { get; set; }
        public DbSet<TransactionAttachments> TrnItemFiles { get; set; }
        public DbSet<TrnItemSourceRadionuclides> TrnItemSourceRadionuclides { get; set; }
        public DbSet<TransactionTypeMaster> TransactionTypeMaster { get; set; }
        public DbSet<TransactionHeader> TransactionHeader { get; set; }

        #endregion

        #region Screen Management
        public DbSet<ent.InternalScreen> InternalScreens { get; set; }
        public DbSet<ent.ListOfValue> ListOfValues { get; set; }
        public DbSet<ent.InternalScreenField> InternalScreenFields { get; set; }
        public DbSet<ent.InternalFieldPermission> InternalFieldPermissions { get; set; }
        #endregion
        #region Internal User Management
        public DbSet<ent.InternalRole> InternalRoles { get; set; }
        public DbSet<ent.InternalScreenRole> InternalScreenRoles { get; set; }
        #endregion
        public DbSet<ent.Workers> Workers { get; set; }
        public DbSet<TreeControl> TreeControl { get; set; }
        public DbSet<ItemHierarchyStructure> ItemHierarchyStructure { get; set; }
        public DbSet<ServiceItemProfile> ServiceItemProfile { get; set; }
        public DbSet<ServiceItemPrice> ServiceItemPrice { get; set; }
        public DbSet<ServiceEntryFees> ServiceEntryFees { get; set; }
        public DbSet<TrnItemSourceHistory> TrnItemSourceHistory { get; set; }

        #region NuclearMaterial
        public DbSet<NuclearMaterialRadionulcide> NuclearMaterialRadionulcides { get; set; }
        public DbSet<NuclearMaterialFiles> NuclearMaterialFiles { get; set; }
        public DbSet<NuclearMaterial> NuclearMaterials { get; set; }
        public DbSet<NuclearMaterialElement> NuclearMaterialElements { get; set; }
        #endregion

        #region RelatedItems
        public DbSet<ent.RelatedItem> RelatedItem { get; set; }
        public DbSet<ent.RelatedItemFiles> RelatedItemsFiles { get; set; }
        public DbSet<ent.RelatedItemsHierarchyStructure> RelatedItemsHierarchyStructure { get; set; }
        #endregion

        public DbSet<CustomerProfile> CustomerProfile { get; set; }

        public DbSet<BillingServiceTrnDetails> BillingServiceTrnDetails { get; set; }
        public DbSet<ent.BillingServiceTrn.BillingServiceTrnHeader> BillingServiceTrnHeader { get; set; }
        public DbSet<ent.NRRCLog> NRRCLogs { get; set; }

        public DbSet<ent.DssQuarterSetupDetails> DssQuarterSetupDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<BasCities>()
            .HasKey(e => new
            {
                e.CountryId,
                e.CityId
            });


            builder.Entity<LookupSetTerm>()
           .HasIndex(e => new { e.LookupSetId, e.AmanId })
           .IsUnique();

            builder.Entity<InternalScreen>(entity =>
            {
                entity.HasMany(x => x.InternalScreenFields).WithOne(d => d.Screen)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            builder.Entity<LookupSetTerm>(entity =>
            {
                entity.HasOne(d => d.LookupSet)
                    .WithMany(p => p.LookupSetTerms)
                    .HasForeignKey(d => d.LookupSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookupSetTerm_LookupSet");
            });

            builder.Entity<TreeControl>()
            .HasKey(e => new
            {
                e.TreeCode,
                e.LevelNum
            });

            builder.Entity<DssFiscalYears>()
             .HasNoKey();

          

            builder.Entity<WorkersDosimeters>()
            .HasKey(e => new
            {
                e.Id,
                e.LineNum
            });

            builder.Entity<WorkersExposuresDoses>()
            .HasKey(e => new
            {
                e.Id,
                e.LineNum
            });


            builder.Entity<NuclearMaterial>()
                    .HasMany(p => p.NuclearMaterialFiles)
                    .WithOne(d => d.NuclearMaterial)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NuclearMaterial>()
                .HasMany(p => p.NuclearMaterialElements)
                .WithOne(d => d.NuclearMaterial)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NuclearMaterialElement>()
                .HasMany(p => p.NuclearMaterialRadionulcides)
                .WithOne(d => d.NuclearMaterialElement)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<RelatedItemsHierarchyStructure>()
                .HasMany(p => p.RelatedItems)
                .WithOne(d => d.HierarchyStructure)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RelatedItem>()
               .HasMany(p => p.RelatedItemFiles)
               .WithOne(d => d.RelatedItem)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DssQuarterSetupDetails>()
                .HasKey(e => new
                {
                    e.PeriodNum,
                    e.QuarterCode,
                    e.CustomerID,
                    e.CmpNum
                });

            builder.Entity<TreeControl>()
            .HasKey(e => new
            {
                e.TreeCode,
                e.LevelNum
            });
            builder.Entity<DssQuarterSetup>()
            .HasKey(e => new
            {
                  e.CmpNum,
                  e.CustomerID,
                  e.QuarterCode
            });


            builder.Entity<ent.BillingServiceTrn.BillingServiceTrnDetails>()
            .HasKey(e => new
            {
                e.LineNum,
                e.TransactionID
            });


            //       builder.Entity<LookupSetTerm>()
            //.HasIndex(x => x.Value).IsUnique(); // This sets the unique index
            base.OnModelCreating(builder);

        }

    }
}


