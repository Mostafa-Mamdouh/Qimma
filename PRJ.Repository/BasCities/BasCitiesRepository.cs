using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class BasCitiesRepository : GenericRepository<ent.BasCities>, IBasCitiesRepository
    {
        public BasCitiesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
