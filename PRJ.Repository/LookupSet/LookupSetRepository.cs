using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LookupSetRepository : GenericRepository<ent.LookupSet>, ILookupSetRepository
    {
        public LookupSetRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
