using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
	public class EntityProfileRepository : GenericRepository<ent.EntityProfile>, IEntityProfileRepository
	{
		public EntityProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
		{

		}
	}
}