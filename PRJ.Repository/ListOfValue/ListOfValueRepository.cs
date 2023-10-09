using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class ListOfValueRepository : GenericRepository<ent.ListOfValue>, IListOfValueRepository
    {
        public ListOfValueRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
