using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TrnItemSourceRepository : GenericRepository<ent.TrnItemSource>, ITrnItemSourceRepository
    {
        public TrnItemSourceRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
