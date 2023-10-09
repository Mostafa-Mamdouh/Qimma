using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    internal class PermitInventoryLimitsRepository : GenericRepository<PermitInventoryLimits>, IPermitInventoryLimitsRepository
    {
        public PermitInventoryLimitsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
