using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class CustomerProfileRepository : GenericRepository<ent.CustomerProfile>, ICustomerProfileRepository
    {
        public CustomerProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
