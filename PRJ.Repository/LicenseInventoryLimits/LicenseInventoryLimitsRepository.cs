using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LicenseInventoryLimitsRepository : GenericRepository<LicenseInventoryLimits>, ILicenseInventoryLimitsRepository
        {
            public LicenseInventoryLimitsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
            {

            }
        }
}
