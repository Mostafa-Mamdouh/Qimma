using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    internal class LookupSetTermRepository : GenericRepository<ent.LookupSetTerm>, ILookupSetTermRepository
    {
        public LookupSetTermRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
