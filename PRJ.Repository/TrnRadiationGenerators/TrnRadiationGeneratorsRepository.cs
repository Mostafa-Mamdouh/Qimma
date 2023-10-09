using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TrnRadiationGeneratorsRepository : GenericRepository<ent.TrnRadiationGenerators>, ITrnRadiationGeneratorsRepository
    {
        public TrnRadiationGeneratorsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
