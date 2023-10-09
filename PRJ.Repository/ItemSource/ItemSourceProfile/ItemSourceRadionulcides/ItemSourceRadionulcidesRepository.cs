using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    public class ItemSourceRadionulcidesRepository : GenericRepository<ent.ItemSourceRadionulcides>, IItemSourceRadionulcidesRepository
    {
        public ItemSourceRadionulcidesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
