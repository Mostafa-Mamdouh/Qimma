using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRJ.Application.AmanDTOs;
using PRJ.Application.Enums;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using PRJ.GlobalComponents.Const;
using System.ComponentModel;
using bus = PRJ.Business;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;


namespace PRJ.Business.AmanBusiness
{
    public class LicenseManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;

        public LicenseManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }


        public async Task<ApiResponse> SaveLicense(dto.DTOLicenseMaster body)
        {
            using (var transaction = _manager.BeginTransaction())
            {
                try
                {

                    var licenseMasterId = await InsertLicenseMaster(body);
                    if (licenseMasterId.Id == 0)
                        return new ApiResponse(400) { Message = licenseMasterId.Message };
                    var licenseInventoryId = await InsertLicenseInventory(body.LicenseInventory, licenseMasterId.Id, licenseMasterId.AmanId,body.LicenseMasterId);

                    if(body.LicenseInventory.SealedSources !=null && body.LicenseInventory.SealedSources.Count > 0)
                    {
                        await InsertSealedSources(body.LicenseInventory.SealedSources, licenseInventoryId.Id, licenseInventoryId.AmanId, body.LicenseMasterId);
                    }
                    if(body.LicenseInventory.UnSealedSources !=null && body.LicenseInventory.UnSealedSources.Count > 0)
                    {
                        await InsertUnSealedSources(body.LicenseInventory.UnSealedSources, licenseInventoryId.Id, licenseInventoryId.AmanId, body.LicenseMasterId);
                    }
                    if(body.LicenseInventory.VUnSealedSources !=null && body.LicenseInventory.VUnSealedSources.Count > 0)
                    {
                        await InsertVUnSealedSources(body.LicenseInventory.VUnSealedSources, licenseInventoryId.Id, licenseInventoryId.AmanId, body.LicenseMasterId);
                    }
                    if(body.LicenseInventory.Generators !=null && body.LicenseInventory.Generators.Count > 0)
                    {
                        await InsertInventoryGenerator(body.LicenseInventory.Generators, licenseInventoryId.Id, licenseInventoryId.AmanId, body.LicenseMasterId);
                    }


                    transaction.Commit();
                    return new ApiResponse(200) { Message = ReponseMsg.success.ToString() };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }
        private async Task InsertSealedSources(List<DTOLicenseInventorySealed> licenseInventorySealeds, int licenseInventotyId, string AmanlicenseInventotyId,Guid licenseMasterId)
        {
            var existing = await _manager.LicenseSealedSources.GetByQuery(m => m.AmanId == licenseMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in licenseInventorySealeds)
            {
                if (existing != null)
                {
                    //existing.LicenseInventoryId = licenseInventotyId;
                    //existing.AmanLicenseInventoryId = AmanlicenseInventotyId.ToString();
                    //existing.MaximumRadioactivity = source.MaximumRadioactivity;
                    //existing.NumberOfSources = source.NumberOfSources;
                    //existing.PurposeOfUse = source.PurposeOfUse;
                    existing.MaximumRadioactivityUnit = await SaveLookup(LookupSetClass.maximumRadioactivityUnit, source.MaximumRadioactivityUnit);
                    existing.Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide);
                    existing.SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn);

                    _manager.LicenseSealedSources.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new LicenseSealedSources
                    {
                        AmanId = licenseMasterId.ToString(),
                        LicenseInventoryId = licenseInventotyId,
                        AmanLicenseInventoryId = AmanlicenseInventotyId,
                        MaximumRadioactivity = source.MaximumRadioactivity,
                        NumberOfSources = source.NumberOfSources,
                        PurposeOfUse = source.PurposeOfUse,
                        MaximumRadioactivityUnit = await SaveLookup(LookupSetClass.maximumRadioactivityUnit, source.MaximumRadioactivityUnit),
                        Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide),
                        SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn),
                    };
                    await _manager.LicenseSealedSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertUnSealedSources(List<DTOLicenseInventoryUnSealed> licenseInventoryUnSealeds, int licenseInventotyId, string AmanlicenseInventotyId,Guid licenseMasterId)
        {
            var existing = await _manager.LicenseUnSealedSources.GetByQuery(m => m.AmanId == licenseMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in licenseInventoryUnSealeds)
            {
                if (existing != null)
                {
                    return;

                    //existing.LicenseInventoryId = licenseInventotyId;
                    //existing.AmanLicenseInventoryId = AmanlicenseInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.MaximumRadioactivity = source.MaximumRadioactivity;
                    //existing.NumberOfSources = source.NumberOfSources;
                    //existing.PurposeOfUse = source.PurposeOfUse;
                    existing.PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm);
                    existing.Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide);
                    existing.SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn);

                    _manager.LicenseUnSealedSources.Update(existing);
                    await _manager.CompleteAsync();
                }
                else
                {
                    var objectOBeInserted = new LicenseUnSealedSources
                    {
                        AmanId = licenseMasterId.ToString(),
                        LicenseInventoryId = licenseInventotyId,
                        AmanLicenseInventoryId = AmanlicenseInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        MaximumRadioactivity = source.MaximumRadioactivity,
                        NumberOfSources = source.NumberOfSources,
                        PurposeOfUse = source.PurposeOfUse,
                        PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm),
                        Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide),
                        SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn),
                    };
                    await _manager.LicenseUnSealedSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertVUnSealedSources(List<DTOLicenseInventoryVUnSealed> licenseInventoryVUnSealeds, int licenseInventotyId, string AmanlicenseInventotyId, Guid licenseMasterId)
        {
            var existing = await _manager.VUnsealedSources.GetByQuery(m => m.AmanId == licenseMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in licenseInventoryVUnSealeds)
            {
                if (existing != null)
                {
                    //existing.LicenseInventoryId = licenseInventotyId;
                    //existing.AmanLicenseInventoryId = AmanlicenseInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.MaximumRadioactivityPerSource = source.MaximumRadioactivityPerSource;
                    //existing.MaximumRadioactivityInTheWorkPlace = source.MaximumRadioactivityInTheWorkPlace;
                    //existing.NumberOfSources = source.NumberOfSources;
                    //existing.PurposeOfUse = source.PurposeOfUse;
                    existing.PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm);
                    existing.Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide);
                    existing.SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn);
                    existing.UnsealedSourceType = await SaveLookup(LookupSetClass.UnsealedSourceType, source.UnsealedSourceType);
                    existing.MaximumRadioactivityPerSourceUnit = await SaveLookup(LookupSetClass.MaximumRadioactivityPerSourceUnit, source.MaximumRadioactivityPerSourceUnit);
                    existing.MaximumRadioactivityInTheWorkPlaceUnit = await SaveLookup(LookupSetClass.MaximumRadioactivityInTheWorkPlaceUnit, source.MaximumRadioactivityInTheWorkPlaceUnit);

                    _manager.VUnsealedSources.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new VUnsealedSources
                    {
                        AmanId = licenseMasterId.ToString(),
                        LicenseInventoryId = licenseInventotyId,
                        AmanLicenseInventoryId = AmanlicenseInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        MaximumRadioactivityPerSource = source.MaximumRadioactivityPerSource,
                        MaximumRadioactivityInTheWorkPlace = source.MaximumRadioactivityInTheWorkPlace,
                        NumberOfSources = source.NumberOfSources,
                        PurposeOfUse = source.PurposeOfUse,
                        PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm),
                        Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide),
                        SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn),
                        UnsealedSourceType = await SaveLookup(LookupSetClass.UnsealedSourceType, source.UnsealedSourceType),
                        MaximumRadioactivityPerSourceUnit = await SaveLookup(LookupSetClass.MaximumRadioactivityPerSourceUnit, source.MaximumRadioactivityPerSourceUnit),
                        MaximumRadioactivityInTheWorkPlaceUnit = await SaveLookup(LookupSetClass.MaximumRadioactivityInTheWorkPlaceUnit, source.MaximumRadioactivityInTheWorkPlaceUnit),
                    };
                    await _manager.VUnsealedSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertInventoryGenerator(List<DTOLicenseInventoryGenerator> licenseInventoryGenerators, int licenseInventotyId, string AmanlicenseInventotyId, Guid licenseMasterId)
        {
            var existing = await _manager.LicenseGenerators.GetByQuery(m => m.AmanId == licenseMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in licenseInventoryGenerators)
            {
                if (existing != null)
                {
                    //existing.LicenseInventoryId = licenseInventotyId;
                    //existing.AmanLicenseInventoryId = AmanlicenseInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.MaximumEnergy = source.MaximumEnergy;
                    //existing.MaximumNumberOfEquipment = source.MaximumNumberOfEquipment;
                    //existing.MaximumTubeCurrent = source.MaximumTubeCurrent;
                    existing.SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn);

                    _manager.LicenseGenerators.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new LicenseGenerators
                    {
                        AmanId = licenseMasterId.ToString(),
                        LicenseInventoryId = licenseInventotyId,
                        AmanLicenseInventoryId = AmanlicenseInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        MaximumEnergy = source.MaximumEnergy,
                        MaximumNumberOfEquipment = source.MaximumNumberOfEquipment,
                        MaximumTubeCurrent = source.MaximumTubeCurrent,
                        SourceUsedIn = await SaveLookup(LookupSetClass.sourceUsedIn, source.SourceUsedIn),
                    };
                    await _manager.LicenseGenerators.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task<HelperRespnse> InsertLicenseInventory(DTOLicenseInventory licenseInventory, int licenseMasterId, string AmanLicenseMasterId,Guid licenseMasteId)
        {
            var existing = await _manager.LicenseInventoryLimits.GetByQuery(m => m.AmanId == licenseMasteId.ToString()).FirstOrDefaultAsync();
            if (existing != null)
            {
                //existing.LicenseMasterId = licenseMasterId;
                //existing.AmanInsertedOn = DateTime.Now;
                //existing.AmanLicenseMasterId = AmanLicenseMasterId;
                //existing.AmanId = licenseMasteId;
                //_manager.LicenseInventoryLimits.Update(existing);
                //await _manager.CompleteAsync();

                return new HelperRespnse { Id = existing.LicenseInventoryId, AmanId = existing.AmanId, Message = "" };

            }
            else
            {
                var objectOBeInserted = new ent.AmanIntegrationEntities.LicenseInventoryLimits
                {
                    LicenseMasterId = licenseMasterId,
                    AmanInsertedOn = DateTime.Now,
                    AmanLicenseMasterId = AmanLicenseMasterId,
                    AmanId = licenseMasteId.ToString(),
                    
                };
                await _manager.LicenseInventoryLimits.AddAsync(objectOBeInserted);
                await _manager.CompleteAsync();

                return new HelperRespnse { Id = objectOBeInserted.LicenseInventoryId, AmanId = objectOBeInserted.AmanId, Message = "" };
            }
        }
        private async Task<HelperRespnse> InsertLicenseMaster(DTOLicenseMaster license)
        {

            var facility = await _manager.FacilityProfile.GetByQuery(m => m.AmanFacilityId == license.FacilityId.ToString()).FirstOrDefaultAsync();
            if (facility == null)
                return new HelperRespnse { Id = 0, Message = ConstErrors.FacilityIdNotExist };


            var entity = await _manager.EntityProfile.GetByQuery(m => m.AmanId == license.EntityId.ToString()).FirstOrDefaultAsync();
            if (entity == null)
                return new HelperRespnse { Id = 0, Message = ConstErrors.EntityIdNotExist };

            var existing = await _manager.LicenseProfile.GetByQuery(m => m.AmanId == license.LicenseMasterId.ToString()).FirstOrDefaultAsync();
            if (existing != null)
            {
                //existing.EntityId = entity.EntityId;
                //existing.AmanEntityId = license.EntityId.ToString();
                //existing.FacilityId = facility.FacilityId;
                //existing.AmanFacilityId = license.FacilityId.ToString();
                //existing.LicenseCode = license.LicenseCode;
                //existing.LicenseVersionNumber = license.LicenseVersionNumber;
                //existing.EffectiveDate = license.EffectiveDate;
                //existing.ExpirationDate = license.ExpirationDate;
                //existing.AmanInsertedOn = DateTime.Now;
                //   existing.PractiseSector = await SaveLookup(LookupSetClass.PractiseSector, license.PractiseSector);
                //   foreach(var x in license.LicensePractices)
                //   {
                ////       existing.LicensePractices = await Sav;

                //   }
                existing.LicensePractices = new List<LicenseProfilePractice>();
                foreach (var x in license.LicensePractices)
                {
                    var result = await SaveLookup(LookupSetClass.licensePractices, x);

                    var LicensePracticeData = new LicenseProfilePractice {
                        PracticeLookup = result,
                                              
                    };
                   

                    existing.LicensePractices.Add(LicensePracticeData);
                }
               
                _manager.LicenseProfile.Update(existing);
                await _manager.CompleteAsync();

                return new HelperRespnse { Id = existing.LicenseId, AmanId = existing.AmanId.ToString(), Message = "" };

            }



            var objectOBeInserted = new ent.LicenseProfile
            {
                AmanId = license.LicenseMasterId.ToString(),
                EntityId = entity.EntityId,
                AmanEntityId = license.EntityId.ToString(),
                FacilityId = facility.FacilityId,
                AmanFacilityId = license.FacilityId.ToString(),
                LicenseCode = license.LicenseCode,
                LicenseVersionNumber = license.LicenseVersionNumber,
                EffectiveDate = license.EffectiveDate,
                ExpirationDate = license.ExpirationDate,
                AmanInsertedOn = DateTime.Now,
                PractiseSector = await SaveLookup(LookupSetClass.PractiseSector, license.PractiseSector),

            };

            objectOBeInserted.LicensePractices = new List<LicenseProfilePractice>();

            foreach (var x in license.LicensePractices)
            {
                var result = await SaveLookup(LookupSetClass.licensePractices, x);
                var licenseprofilePractice =  new LicenseProfilePractice
                {
                    PracticeLookup = result,
                };
                licenseprofilePractice.LicenseProfile = objectOBeInserted;
                objectOBeInserted.LicensePractices.Add(
                    licenseprofilePractice
                ) ;

            }

            await _manager.LicenseProfile.AddAsync(objectOBeInserted);
            await _manager.CompleteAsync();

            return new HelperRespnse { Id = objectOBeInserted.LicenseId, AmanId = objectOBeInserted.AmanId, Message = "" };

        }
        private async Task<int> SaveLookup(LookupSetClass lookupSetClass, AmanLookupDto lookupDto)
        {

            ent.LookupSet entityType = null;
            ent.LookupSetTerm term = _mapper.Map<LookupSetTerm>(lookupDto);

            int lookupTermId = 0;

            #region Entity Type
            entityType = await _manager.LookupSet.GetByQuery(_ => _.ClassName == lookupSetClass.ToString())
                .Include(c => c.LookupSetTerms).FirstOrDefaultAsync();
            if (entityType == null)
            {
                entityType = new ent.LookupSet();
                entityType.IsActive = true;
                entityType.ClassName = lookupSetClass.ToString();
                entityType.DisplayNameAr = lookupSetClass.ToString();
                entityType.DisplayNameEn = lookupSetClass.ToString();
                entityType.LookupSetTerms.Add(term);
                await _manager.LookupSet.AddAsync(entityType);
                lookupTermId = term.LookupSetTermId;
            }
            else
            {
                var lookupTerm = entityType.LookupSetTerms.FirstOrDefault(x => lookupDto.Id > 0 && x.AmanId == lookupDto.Id);
                if (lookupTerm == null)
                {
                    term.LookupSetId = entityType.LookupSetId;
                    await _manager.LookupSetTerm.AddAsync(term);
                    lookupTermId = term.LookupSetTermId;
                }
                else
                {
                    var obj = entityType.LookupSetTerms.First(x => x.AmanId == lookupDto.Id);
                    obj.DisplayNameAr = term.DisplayNameAr;
                    obj.DisplayNameEn = term.DisplayNameEn;

                    lookupTermId = lookupTerm.LookupSetTermId;
                    _manager.LookupSetTerm.Update(obj);

                }
               // _manager.LookupSet.Update(entityType);

            }
            await _manager.CompleteAsync();

            return lookupTermId;
            #endregion

        }

    }
}
