using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
	public class InternalScreenRepository : GenericRepository<ent.InternalScreen>, IInternalScreenRepository
	{
		public InternalScreenRepository(RepositoryContext db, ILogger logger) : base(db, logger)
		{

		}
	}
}