using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class PermitGeneratorsRepository : GenericRepository<PermitGenerators>, IPermitGeneratorsRepository
        {
            public PermitGeneratorsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
            {

            }
        }
}
