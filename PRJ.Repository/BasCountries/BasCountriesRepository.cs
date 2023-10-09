using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class BasCountriesRepository : GenericRepository<ent.BasCountries>, IBasCountriesRepository
	{
        public BasCountriesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
