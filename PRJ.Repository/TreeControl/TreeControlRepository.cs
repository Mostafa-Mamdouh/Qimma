using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class TreeControlRepository : GenericRepository<ent.TreeControl>, ITreeControlRepository
    {
        public TreeControlRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
