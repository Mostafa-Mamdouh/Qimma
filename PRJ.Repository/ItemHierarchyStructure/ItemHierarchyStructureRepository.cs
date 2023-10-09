using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class ItemHierarchyStructureRepository : GenericRepository<ent.Billing.ItemHierarchyStructure>, IItemHierarchyStructureRepository
    {
        public ItemHierarchyStructureRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
