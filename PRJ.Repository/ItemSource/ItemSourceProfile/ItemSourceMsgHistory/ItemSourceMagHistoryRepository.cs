using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class ItemSourceMsgHistoryRepository : GenericRepository<ent.ItemSourceMsgHistory>, IItemSourceMsgHistoryRepository
    {
        public ItemSourceMsgHistoryRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
