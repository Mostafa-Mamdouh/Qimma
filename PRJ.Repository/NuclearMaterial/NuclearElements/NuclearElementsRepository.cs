using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository.NuclearMaterial.NuclearElements
{
    public class NuclearElementsRepository : GenericRepository<ent.NuclearMaterial.NuclearMaterialElement>, INuclearElementsRepository
    {
        public NuclearElementsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
