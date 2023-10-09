using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ent = PRJ.Domain.Entities;
namespace PRJ.Repository.DssQuarterSetupDetails
{
    public class DssQuarterSetupDetailsRepository : GenericRepository<ent.DssQuarterSetupDetails>, IDssQuarterSetupDetailsRepository
    {
        public DssQuarterSetupDetailsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
