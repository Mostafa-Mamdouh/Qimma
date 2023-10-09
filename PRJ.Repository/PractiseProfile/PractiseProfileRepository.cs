using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    internal class PractiseProfileRepository : GenericRepository<ent.PractiseProfile>, IPractiseProfileRepository
    {
        public PractiseProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
