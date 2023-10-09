using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class NuclearMaterialRepository : GenericRepository<ent.NuclearMaterial.NuclearMaterial>, INuclearMaterialRepository
    {
        public NuclearMaterialRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
