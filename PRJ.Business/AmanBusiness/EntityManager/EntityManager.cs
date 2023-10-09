using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Enums;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;

namespace PRJ.Business.AmanBusiness
{
    public class EntityManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public EntityManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;
            _mapper = mapper;
        }


        public async Task<ApiResponse> SaveEntity(dto.DTOEntityProfile body)
        {

            ent.EntityProfile entityprofile = null;
            ent.LookupSet entityType = null;
            ent.LookupSetTerm term = _mapper.Map<LookupSetTerm>(body.EntityType);

            using (var transaction = _manager.BeginTransaction())
            {
                try
                {
                    // Entity Type
                    #region Entity Type
                    entityType = await _manager.LookupSet.GetByQuery(_ => _.ClassName == LookupSetClass.EntityType.ToString()).Include(c => c.LookupSetTerms).FirstOrDefaultAsync();
                    if (entityType == null)
                    {
                        entityType = new ent.LookupSet();
                        entityType.IsActive = true;
                        entityType.ClassName = LookupSetClass.EntityType.ToString();
                        entityType.DisplayNameAr = LookupSetClass.EntityType.ToString();
                        entityType.DisplayNameEn = LookupSetClass.EntityType.ToString();
                        entityType.LookupSetTerms.Add(term);
                        await _manager.LookupSet.AddAsync(entityType);
                    }
                    else
                    {
                        var lookupTerm = entityType.LookupSetTerms.FirstOrDefault(x => body.EntityType.Id > 0 && x.AmanId == body.EntityType.Id);
                        if (lookupTerm == null)
                        {
                            term.LookupSetId = entityType.LookupSetId;
                            await _manager.LookupSetTerm.AddAsync(term);
                        }
                        else
                        {
                            entityType.LookupSetTerms.First(x => x.AmanId == body.EntityType.Id).DisplayNameAr = term.DisplayNameAr;
                            entityType.LookupSetTerms.First(x => x.AmanId == body.EntityType.Id).DisplayNameEn = term.DisplayNameEn;
                        }
                        _manager.LookupSet.Update(entityType);
                    }
                    await _manager.CompleteAsync();
                    #endregion



                    // Entity Profile
                    #region Entity
                    var data = _mapper.Map<ent.EntityProfile>(body);
                    data.EntityType = entityType.LookupSetTerms.FirstOrDefault(x => x.AmanId == body.EntityType.Id).LookupSetTermId;
                    entityprofile = await _manager.EntityProfile.GetByQuery(x => x.AmanId == body.EntityId.ToString()).FirstOrDefaultAsync();
                    if (entityprofile == null)
                        await _manager.EntityProfile.AddAsync(data);
                    else
                    {
                        entityprofile.EntityNameAr = data.EntityNameAr;
                        entityprofile.EntityNameEn = data.EntityNameEn;
                        entityprofile.EmailId = data.EmailId;
                        entityprofile.GovernmentID = data.GovernmentID;
                        entityprofile.AmanInsertedOn = data.AmanInsertedOn;
                        entityprofile.MobileNo = data.MobileNo;
                        _manager.EntityProfile.Update(entityprofile);
                    }
                    await _manager.CompleteAsync();
                    #endregion

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

    }
}
