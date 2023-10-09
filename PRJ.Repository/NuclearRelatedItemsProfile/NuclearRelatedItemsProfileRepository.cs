using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class NuclearRelatedItemsProfileRepository : GenericRepository<ent.NuclearRelatedItemsProfile>, INuclearRelatedItemsProfileRepository
    {
        public NuclearRelatedItemsProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
