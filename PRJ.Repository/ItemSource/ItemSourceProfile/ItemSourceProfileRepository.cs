using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class ItemSourceProfileRepository : GenericRepository<ent.ItemSourceProfile>, IItemSourceProfileRepository
    {
        public ItemSourceProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
