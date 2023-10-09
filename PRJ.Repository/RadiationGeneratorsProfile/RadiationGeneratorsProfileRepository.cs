using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class RadiationGeneratorsProfileRepository : GenericRepository<ent.RadiationGeneratorsProfile>, IRadiationGeneratorsProfileRepository
    {
        public RadiationGeneratorsProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
