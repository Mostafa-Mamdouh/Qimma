using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Enums;
using bus = PRJ.Business;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using PRJ.Domain.Entities;
using PRJ.Application.AmanDTOs;
using PRJ.GlobalComponents.Const;
using PRJ.Application.AmanDTOs.LicenseInventory;
using PRJ.Domain.Entities.AmanIntegrationEntities.LicenseInventoryEntities;
using PRJ.Domain.Entities.AmanIntegrationEntities.PermitInventoryEntities;

namespace PRJ.Business.AmanBusiness
{
    public class PermiteManager
    {

        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;

        public PermiteManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<ApiResponse> SavePermite(dto.DTOPermitMaster body)
        {

            using (var transaction = _manager.BeginTransaction())
            {
                try
                {

                    var permitMasterId = await InsertPermitMaster(body);
                    if (permitMasterId.Id == 0)
                        return new ApiResponse(400) { Message = permitMasterId.Message };
                    var permitInventoryId = await InsertPermitInventory(body.PermitInventory, permitMasterId.Id, permitMasterId.AmanId, permitMasterId.OtherId.Value, permitMasterId.OtherAmanId,body.PermitMasterId);
                    if(body.PermitInventory.SealedSources != null && body.PermitInventory.SealedSources.Count > 0)
                    {
                        await InsertSealedSources(body.PermitInventory.SealedSources, permitInventoryId.Id, permitInventoryId.AmanId, body.PermitMasterId);
                    }
                    if(body.PermitInventory.UnSealedSources !=null && body.PermitInventory.UnSealedSources.Count > 0){
                        await InsertUnSealedSources(body.PermitInventory.UnSealedSources, permitInventoryId.Id, permitInventoryId.AmanId, body.PermitMasterId);
                    }
                    if(body.PermitInventory.NuclearSources != null && body.PermitInventory.NuclearSources.Count > 0)
                    {
                        await InsertInventoryNuclearMaterial(body.PermitInventory.NuclearSources, permitInventoryId.Id, permitInventoryId.AmanId, body.PermitMasterId);
                    }
                    if(body.PermitInventory.Generators != null && body.PermitInventory.Generators.Count > 0)
                    {
                        await InsertInventoryGenerator(body.PermitInventory.Generators, permitInventoryId.Id, permitInventoryId.AmanId, body.PermitMasterId);
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





        private async Task InsertSealedSources(List<DTOPermitInventorySealed> permitInventorySealeds, int permitInventotyId, string AmanPermitInventotyId,Guid permiteMasterId)
        {
            var existing = await _manager.PermitSealedSources.GetByQuery(m => m.AmanId == permiteMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in permitInventorySealeds)
            {
                if (existing != null)
                {
                    //existing.PermitInventoryId = permitInventotyId;
                    //existing.AmanPermitInventoryId = AmanPermitInventotyId.ToString();
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.MaximumRadioactivity = source.MaximumRadioactivity;
                    //existing.Model = source.Model;
                    //existing.SerialNo = source.SerialNo;
                    existing.MaximumRadioactivityUnit = await SaveLookup(LookupSetClass.maximumRadioactivityUnit, source.MaximumRadioactivityUnit);
                    existing.Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide);
                    existing.ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName);

                    _manager.PermitSealedSources.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new PermitSealedSources
                    {
                        AmanId = permiteMasterId.ToString(),
                        PermitInventoryId = permitInventotyId,
                        AmanPermitInventoryId = AmanPermitInventotyId.ToString(),
                        AmanInsertedOn = DateTime.Now,
                        MaximumRadioactivity = source.MaximumRadioactivity,
                        Model = source.Model,
                        SerialNo = source.SerialNo,
                        MaximumRadioactivityUnit = await SaveLookup(LookupSetClass.maximumRadioactivityUnit, source.MaximumRadioactivityUnit),
                        Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide),
                        ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName),
                    };
                    await _manager.PermitSealedSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertUnSealedSources(List<DTOPermitInventoryUnSealed> permitInventoryUnSealeds, int permitInventotyId, string AmanPermitInventotyId,Guid permiteMasterId)
        {
            var existing = await _manager.PermitUnSealedSources.GetByQuery(m => m.AmanId == permiteMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in permitInventoryUnSealeds)
            {
                if (existing != null)
                {
                    //existing.PermitInventoryId = permitInventotyId;
                    //existing.AmanPermitInventoryId = AmanPermitInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.CurrentActivity = source.CurrentActivity;
                    //existing.SerialNo = source.SerialNo;
                    existing.ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName);
                    existing.PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm);
                    existing.Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide);
                    existing.CurrentActivityUnit = await SaveLookup(LookupSetClass.CurrentActivityUnit, source.CurrentActivityUnit);

                    _manager.PermitUnSealedSources.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new PermitUnSealedSources
                    {
                        AmanId = permiteMasterId.ToString(),
                        PermitInventoryId = permitInventotyId,
                        AmanPermitInventoryId = AmanPermitInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        CurrentActivity = source.CurrentActivity,
                        SerialNo = source.SerialNo,
                        ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName),
                        PhysicalForm = await SaveLookup(LookupSetClass.PhysicalForm, source.PhysicalForm),
                        Radionuclide = await SaveLookup(LookupSetClass.radionuclide, source.Radionuclide),
                        CurrentActivityUnit = await SaveLookup(LookupSetClass.CurrentActivityUnit, source.CurrentActivityUnit),
                    };
                    await _manager.PermitUnSealedSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertInventoryNuclearMaterial(List<DTOPermitInventoryNuclearMaterial> permitInventoryNuclearMaterials, int permitInventotyId, string AmanPermitInventotyId, Guid permiteMasterId)
        {
            var existing = await _manager.PermitNuclearSources.GetByQuery(m => m.AmanId == permiteMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in permitInventoryNuclearMaterials)
            {
                if (existing != null)
                {
                    //existing.PermitInventoryId = permitInventotyId;
                    //existing.AmanPermitInventoryId = AmanPermitInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.SerialNo = source.SerialNo;
                    //existing.Model = source.Model;
                    existing.ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName);
                    existing.EquipmentType = await SaveLookup(LookupSetClass.EquipmentType, source.EquipmentType);

                    _manager.PermitNuclearSources.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new PermitNuclearSources
                    {
                        AmanId = permiteMasterId.ToString(),
                        PermitInventoryId = permitInventotyId,
                        AmanPermitInventoryId = AmanPermitInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        SerialNo = source.SerialNo,
                        Model = source.Model,
                        ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName),
                        EquipmentType = await SaveLookup(LookupSetClass.EquipmentType, source.EquipmentType),
                    };
                    await _manager.PermitNuclearSources.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }
        private async Task InsertInventoryGenerator(List<DTOPermitInventoryGenerator> permitInventoryGenerators, int permitInventotyId, string AmanPermitInventotyId, Guid permiteMasterId)
        {
            var existing = await _manager.PermitGenerators.GetByQuery(m => m.AmanId == permiteMasterId.ToString()).FirstOrDefaultAsync();

            foreach (var source in permitInventoryGenerators)
            {
                if (existing != null)
                {
                    //existing.PermitInventoryId = permitInventotyId;
                    //existing.AmanPermitInventoryId = AmanPermitInventotyId;
                    //existing.AmanInsertedOn = DateTime.Now;
                    //existing.MaximumEnergy = source.MaximumEnergy;
                    //existing.MaximumTubeCurrent = source.MaximumTupeCurrent;
                    //existing.MaximumVoltage = source.MaximumVoltage;
                    existing.ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName);
                    existing.EquipmentType = await SaveLookup(LookupSetClass.EquipmentType, source.EquipmentType);


                    _manager.PermitGenerators.Update(existing);
                    await _manager.CompleteAsync();


                }
                else
                {
                    var objectOBeInserted = new PermitGenerators
                    {
                        AmanId = permiteMasterId.ToString(),
                        PermitInventoryId = permitInventotyId,
                        AmanPermitInventoryId = AmanPermitInventotyId,
                        AmanInsertedOn = DateTime.Now,
                        MaximumEnergy = source.MaximumEnergy,
                        MaximumTubeCurrent = source.MaximumTupeCurrent,
                        MaximumVoltage = source.MaximumVoltage,
                        ManufactuerName = await SaveLookup(LookupSetClass.ManufactuerName, source.ManufactuerName),
                        EquipmentType = await SaveLookup(LookupSetClass.EquipmentType, source.EquipmentType),
                    };
                    await _manager.PermitGenerators.AddAsync(objectOBeInserted);
                    await _manager.CompleteAsync();

                }
            }

        }







        private async Task<HelperRespnse> InsertPermitInventory(DTOPermitInventory permitInventory, int permitMasterId, string amanPermitMasterId, int licenseMasterId, string AmanLicenseMasterId,Guid permiteMasterId)
        {
            var existing = await _manager.PermitInventoryLimits.GetByQuery(m => m.AmanId == permiteMasterId.ToString()).FirstOrDefaultAsync();
            if (existing != null)
            {
                //existing.LicenseMasterId = licenseMasterId;
                //existing.AmanInsertedOn = DateTime.Now;
                //existing.AmanLicenseMasterId = AmanLicenseMasterId;
                //existing.PermitMasterId = permitMasterId;
                //existing.AmanPermitMasterId = amanPermitMasterId;
                _manager.PermitInventoryLimits.Update(existing);
                await _manager.CompleteAsync();

                return new HelperRespnse { Id = existing.PermitInventoryId, AmanId = existing.AmanId, Message = "" };

            }
            else
            {
                var objectOBeInserted = new ent.AmanIntegrationEntities.PermitInventoryLimits
                {
                    AmanId = permiteMasterId.ToString(),
                    LicenseMasterId = licenseMasterId,
                    AmanInsertedOn = DateTime.Now,
                    AmanLicenseMasterId = AmanLicenseMasterId,
                    PermitMasterId = permitMasterId,
                    AmanPermitMasterId = amanPermitMasterId,
                };
                await _manager.PermitInventoryLimits.AddAsync(objectOBeInserted);
                await _manager.CompleteAsync();

                return new HelperRespnse { Id = objectOBeInserted.PermitInventoryId, AmanId = objectOBeInserted.AmanId, Message = "" };
            }
        }


        private async Task<HelperRespnse> InsertPermitMaster(DTOPermitMaster permit)
        {

            var facility = await _manager.FacilityProfile.GetByQuery(m => m.AmanFacilityId == permit.FacilityId.ToString()).FirstOrDefaultAsync();
            if (facility == null)
                return new HelperRespnse { Id = 0, Message = ConstErrors.FacilityIdNotExist };


            var license = await _manager.LicenseProfile.GetByQuery(m => m.AmanId == permit.LicenseMasterId.ToString()).FirstOrDefaultAsync();
            if (license == null)
                return new HelperRespnse { Id = 0, Message = ConstErrors.LicenseIdNotExist };

            var existing = await _manager.PermitDetailsProfile.GetByQuery(m => m.AmanId == permit.PermitMasterId.ToString()).FirstOrDefaultAsync();
            if (existing != null)
            {
                //existing.LicenseId = license.LicenseId;
                //existing.AmanLicenseId = permit.LicenseMasterId.ToString();
                //existing.FacilityId = facility.FacilityId;
                //existing.AmanFacilityId = permit.FacilityId.ToString();
                //existing.PermitNumber = permit.PermitCode;
                //existing.EffectiveDate = permit.EffectiveDate;
                //existing.ExpirationDate = permit.ExpirationDate;
                //existing.AmanInsertedOn = DateTime.Now;
                existing.PermitType = await SaveLookup(LookupSetClass.PermitType, permit.PermitType);
                existing.ModifiedOn = DateTime.Now;

                _manager.PermitDetailsProfile.Update(existing);
                await _manager.CompleteAsync();

                return new HelperRespnse { Id = existing.PermitDetailsId, AmanId = existing.AmanId.ToString(), OtherId = license.LicenseId, OtherAmanId = permit.PermitCode.ToString(), Message = "" };

            }



            var objectOBeInserted = new ent.PermitDetailsProfile
            {
                AmanId = permit.PermitMasterId.ToString(),
                LicenseId = license.LicenseId,
                AmanLicenseId = permit.LicenseMasterId.ToString(),
                FacilityId = facility.FacilityId,
                AmanFacilityId = permit.FacilityId.ToString(),
                PermitNumber = permit.PermitCode,
                EffectiveDate = permit.EffectiveDate,
                ExpirationDate = permit.ExpirationDate,
                AmanInsertedOn = DateTime.Now,
                PermitType = await SaveLookup(LookupSetClass.PermitType, permit.PermitType)

            };
            await _manager.PermitDetailsProfile.AddAsync(objectOBeInserted);
            await _manager.CompleteAsync();

            return new HelperRespnse { Id = objectOBeInserted.PermitDetailsId, AmanId = objectOBeInserted.AmanId.ToString(), OtherId = license.LicenseId, OtherAmanId = permit.LicenseMasterId.ToString(), Message = "" };

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
                //_manager.LookupSet.Update(entityType);
                //lookupTermId = lookupTerm.LookupSetTermId;

            }
            await _manager.CompleteAsync();

            return lookupTermId;
            #endregion

        }
    }
}
