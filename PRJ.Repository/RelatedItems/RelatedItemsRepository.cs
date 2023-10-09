using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class RelatedItemsRepository : GenericRepository<ent.RelatedItem>, IRelatedItemsRepository
    {
        public RelatedItemsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
