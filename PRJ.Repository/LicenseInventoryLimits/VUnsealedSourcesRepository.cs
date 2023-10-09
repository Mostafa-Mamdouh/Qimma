using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class VUnsealedSourcesRepository : GenericRepository<VUnsealedSources>, IVUnsealedSourcesRepository
        {
            public VUnsealedSourcesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
            {

            }
        }
}
