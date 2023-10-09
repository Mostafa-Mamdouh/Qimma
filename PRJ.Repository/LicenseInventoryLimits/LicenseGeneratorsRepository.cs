using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LicenseGeneratorsRepository : GenericRepository<LicenseGenerators>, ILicenseGeneratorsRepository
        {
            public LicenseGeneratorsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
            {

            }
        }
}
