using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    internal class WorkersRepository : GenericRepository<ent.Workers>, IWorkersRepository
    {
        public WorkersRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    
    }
}
