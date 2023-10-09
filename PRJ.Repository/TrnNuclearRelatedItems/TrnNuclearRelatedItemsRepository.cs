using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TrnNuclearRelatedItemsRepository : GenericRepository<ent.TrnNuclearRelatedItems>, ITrnNuclearRelatedItemsRepository
    {
        public TrnNuclearRelatedItemsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
