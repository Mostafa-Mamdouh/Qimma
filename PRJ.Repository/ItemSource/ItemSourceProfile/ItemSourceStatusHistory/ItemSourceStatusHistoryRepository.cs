using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class ItemSourceStatusHistoryRepository : GenericRepository<ent.ItemSourceStatusHistory>, IItemSourceStatusHistoryRepository
    {
        public ItemSourceStatusHistoryRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
