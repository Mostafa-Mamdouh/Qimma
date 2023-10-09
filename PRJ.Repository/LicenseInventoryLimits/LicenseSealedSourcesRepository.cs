﻿using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LicenseSealedSourcesRepository : GenericRepository<LicenseSealedSources>, ILicenseSealedSourcesRepository
    {
        public LicenseSealedSourcesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
