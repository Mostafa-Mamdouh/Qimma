using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class ExternalMaserUserRepository : GenericRepository<ent.ExternalMaserUser>, IExternalMaserUserRepository
	{
        public ExternalMaserUserRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
