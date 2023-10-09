using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
namespace PRJ.Business.RadiationGeneratorsProfile
{
    public class RadiationGeneratorsProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public RadiationGeneratorsProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>>> GetAll()
        {
            
                var RadiationGeneratorsProfile = await _manager.RadiationGeneratorsProfile.GetAllAsync();

                return new wap.Response<List<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>>()
                {
                    Data = _mapper.Map<List<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>>(RadiationGeneratorsProfile)
                };
           
        }


        public async Task<wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>> GetRadiationGeneratorsProfileById(string Id)
        {
            
                var getById = await _manager.RadiationGeneratorsProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>()
                    {
                        Data = _mapper.Map<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
          
        }

        public async Task<wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>> UpdateRadiationGeneratorsProfile( dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile body)
        {
            
                var RadiationGeneratorsProfile = await _manager.RadiationGeneratorsProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.RadiationGeneratorsProfile>(body);
                _manager.RadiationGeneratorsProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>();
            
          
        }


        public async Task<wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>> AddRadiationGeneratorsProfile(dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile body)
        {
            
                var RadiationGeneratorsProfile = new ent.RadiationGeneratorsProfile();
                RadiationGeneratorsProfile.EquipmentDescAr = body.EquipmentDescAr;
                RadiationGeneratorsProfile.EquipmentDescEn = body.EquipmentDescEn;
                RadiationGeneratorsProfile.NrrcID = body.NrrcID;
                RadiationGeneratorsProfile.Status = body.Status;
                RadiationGeneratorsProfile.ManufacturerSerialNo = body.ManufacturerSerialNo;
                RadiationGeneratorsProfile.DateofManufacturing = body.DateofManufacturing;
                RadiationGeneratorsProfile.FacilitySerialNo = body.FacilitySerialNo;
                RadiationGeneratorsProfile.Purpose = body.Purpose;
                RadiationGeneratorsProfile.ModelNumber = body.ModelNumber;
                RadiationGeneratorsProfile.XRayTubeSerialNo = body.XRayTubeSerialNo;
                RadiationGeneratorsProfile.ModelNumber = body.ModelNumber;
                RadiationGeneratorsProfile.MaxEnergy = body.MaxEnergy;
                RadiationGeneratorsProfile.EnergyUnit = body.EnergyUnit;
                RadiationGeneratorsProfile.MaxDoseRate = body.MaxDoseRate;
                RadiationGeneratorsProfile.Unit = body.Unit;
                RadiationGeneratorsProfile.DoseUnit = body.DoseUnit;
                RadiationGeneratorsProfile.EntityId = body.EntityId;
                RadiationGeneratorsProfile.EntityProfile = body.EntityProfile;
                RadiationGeneratorsProfile.FacilityId = body.FacilityId;
                RadiationGeneratorsProfile.FacilityProfile = body.FacilityProfile;
                RadiationGeneratorsProfile.LicenseId = body.LicenseId;
                RadiationGeneratorsProfile.LicenseProfile = body.LicenseProfile;
                RadiationGeneratorsProfile.LicenseInventoryId = body.LicenseInventoryId;
                RadiationGeneratorsProfile.LicenseInventoryLimits = body.LicenseInventoryLimits;
                RadiationGeneratorsProfile.PermitdetailsId = body.PermitdetailsId;
                RadiationGeneratorsProfile.PermitDetailsProfile = body.PermitDetailsProfile;
                RadiationGeneratorsProfile.PermitInventoryId = body.PermitInventoryId;
                RadiationGeneratorsProfile.PermitInventoryLimits = body.PermitInventoryLimits;
                RadiationGeneratorsProfile.PractiseId = body.PractiseId;
                RadiationGeneratorsProfile.PractiseProfile = body.PractiseProfile;
                RadiationGeneratorsProfile.SROId = body.SROId;
                RadiationGeneratorsProfile.SafetyResponsibleOfficersProfile = body.SafetyResponsibleOfficersProfile;
                RadiationGeneratorsProfile.LegalRepresentativesId = body.LegalRepresentativesId;
                RadiationGeneratorsProfile.LegalRepresentativesProfile = body.LegalRepresentativesProfile;
                RadiationGeneratorsProfile.ManufacturerId = body.ManufacturerId;
                RadiationGeneratorsProfile.ManufacturerMaster = body.ManufacturerMaster;
                RadiationGeneratorsProfile.SheildMaterial = body.SheildMaterial;
                RadiationGeneratorsProfile.MaxCurrent = body.MaxCurrent;
                RadiationGeneratorsProfile.ShieldNuclearMaterialCode = body.ShieldNuclearMaterialCode;
                RadiationGeneratorsProfile.LookupSet = body.LookupSet;


                await _manager.RadiationGeneratorsProfile.AddAsync(RadiationGeneratorsProfile);

                return new wap.Response<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>()
                {
                    Data = _mapper.Map<dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile>(RadiationGeneratorsProfile)
                };
            
        }
    }
}

