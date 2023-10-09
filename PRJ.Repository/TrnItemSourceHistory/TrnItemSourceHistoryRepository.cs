using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class TrnItemSourceHistoryRepository : GenericRepository<ent.TrnItemSourceHistory>, ITrnItemSourceHistoryRepository
    {
        public TrnItemSourceHistoryRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
