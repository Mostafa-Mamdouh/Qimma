using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class PermitUnSealedSourcesRepository : GenericRepository<PermitUnSealedSources>, IPermitUnSealedSourcesRepository
    {
        public PermitUnSealedSourcesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
