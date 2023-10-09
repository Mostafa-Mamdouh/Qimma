using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class ItemSourceStatusRepository : GenericRepository<ent.ItemSourceStatus>, IItemSourceStatusRepository
    {
        public ItemSourceStatusRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
