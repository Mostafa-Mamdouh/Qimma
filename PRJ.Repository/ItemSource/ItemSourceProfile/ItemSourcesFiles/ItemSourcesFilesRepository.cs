using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class ItemSourceFilesRepository : GenericRepository<ent.ItemSourceFiles>, IItemSourceFilesRepository
    {
        public ItemSourceFilesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
