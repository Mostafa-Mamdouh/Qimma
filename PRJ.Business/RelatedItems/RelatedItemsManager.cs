using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.RelatedItems.Items;
using PRJ.Application.Warppers;
using PRJ.Domain.Entities;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.RelatedItems
{
    public class RelatedItemsManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public RelatedItemsManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }


        public async Task<String> GetMaxItemCode()
        {
            string nextSerialNum = null;

            var RelatedItems = await _manager.RelatedItems.GetByQuery(t => t.RelatedItemCode == t.RelatedItemCode).OrderByDescending(i => i.RelatedItemCode).FirstOrDefaultAsync();
            if (RelatedItems != null)
            {
                nextSerialNum = (Convert.ToInt32(RelatedItems.RelatedItemCode) + 1).ToString();
            }
            else
            {
                nextSerialNum = "1";
            }

            return nextSerialNum;
        }

        public async Task<wap.Response<dto.DTORelatedItem>> GetRelatedItemsById(string relatedItemCode)
        {

            var getById = _manager.RelatedItems.GetByQuery(x => x.RelatedItemCode == relatedItemCode)
                .Include(x => x.RelatedItemFiles)
                .FirstOrDefault();

            if (getById != null)
            {
                return new wap.Response<dto.DTORelatedItem>()
                {
                    Data = _mapper.Map<dto.DTORelatedItem>(getById)
                };
            }
            else
            {
                return new wap.Response<dto.DTORelatedItem>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };
            }

        }


        public async Task<wap.Response<dto.DTORelatedItem>> UpdateRelatedItems(dto.DTORelatedItem body)
        {

            var data = _mapper.Map<RelatedItem>(body);

            if (data == null)
            {
                return new wap.Response<dto.DTORelatedItem>
                {
                    Succeeded = false,
                };
            }

            _manager.RelatedItems.Update(data);

            // Files
            _manager.RelatedItemsFiles.DeleteByParentId(x => x.RelatedItemCode == data.RelatedItemCode);

            foreach (var file in body.RelatedItemFiles)
            {
                _mapper.Map<RelatedItemFiles>(file);
            }
            // ************
            await _manager.CompleteAsync();

            return new wap.Response<dto.DTORelatedItem>() { Succeeded = true };
        }


        public async Task<wap.Response<dto.DTORelatedItem>> AddRelatedItems(dto.DTORelatedItem body)
        {

            body.RelatedItemCode = await GetMaxItemCode();
            var Data = _mapper.Map<ent.RelatedItem>(body);
            await _manager.RelatedItems.AddAsync(Data);
            await _manager.CompleteAsync();

            return new wap.Response<dto.DTORelatedItem>()
            {
                Succeeded = true,
            };
        }

        public async Task<wap.Response<bool>> DeleteRelatedItems(string relatedItemCode)
        {

            var getById = await _manager.RelatedItems.GetByIdAsync(relatedItemCode);

            await _manager.RelatedItems.DeleteAsync(getById);
            await _manager.CompleteAsync();
            return new wap.Response<bool>() { Succeeded = true };

        }

        public async Task<wap.PagingResponse<List<DTOPaginRelatedItems>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<DTOPaginRelatedItems>>()
            {
                Draw = param.Draw
            };

            // Get all
            IQueryable<ent.RelatedItem> dataList = _manager.RelatedItems.GetAll()
                .Include(x => x.ManufacturerLookup)
                .Include(x => x.ManufacturerCountryLookup)
                .Include(x => x.HierarchyStructure);
            ;

            // Check if search is on specific column
            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            // General Search
            IQueryable<ent.RelatedItem> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.Description.Contains(param.Search.Value) ||
                    r.ManufacturerSerialNom.Contains(param.Search.Value)
                    );
            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "manufacturerSerialNom":
                                query = dataList.Where(u => u.ManufacturerSerialNom.Contains(search.Search.Value));
                                break;
                            case "hierarchyStructureCode":
                                query = dataList.Where(u => u.HierarchyStructureCode.Contains(search.Search.Value));
                                break;
                            case "isTechnologyAndSoftware":
                                query = dataList.Where(u => u.IsTechnologyAndSoftware.ToString().Contains(search.Search.Value));
                                break;
                            case "description":
                                query = dataList.Where(u => u.Description.Contains(search.Search.Value));
                                break;
                        }
                    }
                }
            }
            else
            {
                query = dataList;
            }

            var colOrder = param.Order[0];

            switch (colOrder.Column)
            {

                case 0:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNom) : query.OrderByDescending(r => r.ManufacturerSerialNom);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.HierarchyStructureCode) : query.OrderByDescending(r => r.HierarchyStructureCode);
                    break;
                case 2:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.IsTechnologyAndSoftware) : query.OrderByDescending(r => r.IsTechnologyAndSoftware);
                    break;
                case 3:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.Description) : query.OrderByDescending(r => r.Description);
                    break;
                default:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                    break;
            }
            var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
            response.Data = _mapper.Map<List<DTOPaginRelatedItems>>(data);
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;
            return response;
        }

        public async Task<wap.Response<List<dto.DTORelatedItem>>> GetAll()
        {

            var RelatedItem = await _manager.RelatedItems.GetAllAsync();
            return new wap.Response<List<dto.DTORelatedItem>>()
            {
                Data = _mapper.Map<List<dto.DTORelatedItem>>(RelatedItem)
            };

        }
        public async Task<PagingResponse<List<dto.DTORelatedItem>>> GetFunctionalwithStructureCode(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.RelatedItem>>()
            {
                Draw = param.Draw
            };

            IQueryable<ent.RelatedItem> dataList = (IQueryable<ent.RelatedItem>)await _manager.RelatedItems.GetAllAsync();
            if (!string.IsNullOrEmpty(param.extra))
            {
                dataList = dataList.Where(obj => EF.Functions.Like(obj.HierarchyStructureCode, param.extra + "%"));
            }
            // Check if search is on specific column
            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            // General Search
            IQueryable<ent.RelatedItem> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.ManufacturerSerialNom.Contains(param.Search.Value) ||
                    r.Description.Contains(param.Search.Value) ||
                    r.HierarchyStructure.HSCode.Contains(param.Search.Value));

            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "manufacturerSerialNom":
                                query = dataList.Where(u => u.ManufacturerSerialNom.Contains(search.Search.Value));
                                break;
                            case "description":
                                query = dataList.Where(u => u.Description.Contains(search.Search.Value));
                                break;

                            case "hsCode":
                                query = dataList.Where(u => u.HierarchyStructure.HSCode.Contains(search.Search.Value));
                                break;
                        }
                    }
                }
            }
            else
            {
                query = dataList;
            }

            var colOrder = param.Order[0];

            switch (colOrder.Column)
            {
                case 0:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.HierarchyStructure.HSCode) : query.OrderByDescending(r => r.HierarchyStructure.HSCode);
                    break;
                case 1:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNom) : query.OrderByDescending(r => r.ManufacturerSerialNom);
                    break;
                case 2:
                    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.Description) : query.OrderByDescending(r => r.Description);
                    break;
            }

            response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;

            return new PagingResponse<List<dto.DTORelatedItem>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<dto.DTORelatedItem>>(response.Data),
                Draw = response.Draw,
                RecordsTotal = response.RecordsTotal,
                RecordsFiltered = response.RecordsFiltered
            };

        }

    }
}
