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

namespace PRJ.Business.NuclearRelatedItemsProfile
{
    public class NuclearRelatedItemsProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public NuclearRelatedItemsProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>>> GetAll()
        {
            
                var NuclearRelatedItemsProfile = await _manager.NuclearRelatedItemsProfile.GetAllAsync();

                return new wap.Response<List<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>>()
                {
                    Data = _mapper.Map<List<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>>(NuclearRelatedItemsProfile)
                };
           
        }


        public async Task<wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>> GetNuclearRelatedItemsProfileById(string Id)
        {
            
                var getById = await _manager.NuclearRelatedItemsProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>()
                    {
                        Data = _mapper.Map<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
           
        }

        public async Task<wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>> UpdateNuclearRelatedItemsProfile(dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile body)
        {
            
                var NuclearRelatedItemsProfile = await _manager.NuclearRelatedItemsProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.NuclearRelatedItemsProfile>(body);
                _manager.NuclearRelatedItemsProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>();
            
        }


        public async Task<wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>> AddNuclearRelatedItemsProfile(dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile body)
        {
            
                var NuclearRelatedItemsProfile = new ent.NuclearRelatedItemsProfile();
                NuclearRelatedItemsProfile.RelatedItemDescAr = body.RelatedItemDescAr;
                NuclearRelatedItemsProfile.RelatedItemDescEn = body.RelatedItemDescEn;
                NuclearRelatedItemsProfile.NrrcID = body.NrrcID;
                NuclearRelatedItemsProfile.Status = body.Status;
                NuclearRelatedItemsProfile.ManufacturerSerialNo = body.ManufacturerSerialNo;
                NuclearRelatedItemsProfile.DateofManufacturing = body.DateofManufacturing;
                NuclearRelatedItemsProfile.FacilityRelatedItemID = body.FacilityRelatedItemID;
                NuclearRelatedItemsProfile.Purpose = body.Purpose;
                NuclearRelatedItemsProfile.ItemTypeNo = body.ItemTypeNo;
                NuclearRelatedItemsProfile.ItemtypeName = body.ItemtypeName;
                NuclearRelatedItemsProfile.ModelNumber = body.ModelNumber;
                NuclearRelatedItemsProfile.HSCode = body.HSCode;
                NuclearRelatedItemsProfile.GovernmentCommitmentsFlag = body.GovernmentCommitmentsFlag;
                NuclearRelatedItemsProfile.EndUserCertificateFlag = body.EndUserCertificateFlag;
                NuclearRelatedItemsProfile.Unit = body.Unit;
                NuclearRelatedItemsProfile.PermitInitialQty = body.PermitInitialQty;
                NuclearRelatedItemsProfile.EntityId = body.EntityId;
                NuclearRelatedItemsProfile.EntityProfile = body.EntityProfile;
                NuclearRelatedItemsProfile.FacilityId = body.FacilityId;
                NuclearRelatedItemsProfile.FacilityProfile = body.FacilityProfile;
                NuclearRelatedItemsProfile.LicenseId = body.LicenseId;
                NuclearRelatedItemsProfile.LicenseProfile = body.LicenseProfile;
                NuclearRelatedItemsProfile.LicenseInventoryId = body.LicenseInventoryId;
                NuclearRelatedItemsProfile.LicenseInventoryLimits = body.LicenseInventoryLimits;
                NuclearRelatedItemsProfile.PermitdetailsId = body.PermitdetailsId;
                NuclearRelatedItemsProfile.PermitDetailsProfile = body.PermitDetailsProfile;
                NuclearRelatedItemsProfile.PermitInventoryId = body.PermitInventoryId;
                NuclearRelatedItemsProfile.PermitInventoryLimits = body.PermitInventoryLimits;
                NuclearRelatedItemsProfile.PractiseId = body.PractiseId;
                NuclearRelatedItemsProfile.PractiseProfile = body.PractiseProfile;
                NuclearRelatedItemsProfile.SROId = body.SROId;
                NuclearRelatedItemsProfile.SafetyResponsibleOfficersProfile = body.SafetyResponsibleOfficersProfile;
                NuclearRelatedItemsProfile.LegalRepresentativesId = body.LegalRepresentativesId;
                NuclearRelatedItemsProfile.LegalRepresentativesProfile = body.LegalRepresentativesProfile;
                NuclearRelatedItemsProfile.ManufacturerId = body.ManufacturerId;
                NuclearRelatedItemsProfile.ManufacturerMaster = body.ManufacturerMaster;
                NuclearRelatedItemsProfile.ManufactureCountryId = body.ManufactureCountryId;
                NuclearRelatedItemsProfile.BasCountries = body.BasCountries;
                NuclearRelatedItemsProfile.ItemCategory = body.ItemCategory;
                NuclearRelatedItemsProfile.LookupSet = body.LookupSet;


                await _manager.NuclearRelatedItemsProfile.AddAsync(NuclearRelatedItemsProfile);
                return new wap.Response<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>()
                {
                    Data = _mapper.Map<dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile>(NuclearRelatedItemsProfile)
                };
            
        }
    }
}
