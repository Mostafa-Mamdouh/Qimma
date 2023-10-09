using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class NuclearMaterialRadionuclideRepository : GenericRepository<ent.NuclearMaterial.NuclearMaterialRadionulcide>, INuclearMaterialRadionuclideRepository
    {
        public NuclearMaterialRadionuclideRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
