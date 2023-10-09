using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;
using enc = PRJ.GlobalComponents.Encryption;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.LookupSet
{
	public class LookupSetManager
	{
		private readonly rep.IUnitOfWork _manager;
		public readonly IMapper _mapper;
		private readonly IMemoryCache _memoryCache;


		public LookupSetManager(rep.IUnitOfWork manager, IMapper mapper, IMemoryCache memoryCache)
		{
			this._manager = manager;
			this._memoryCache = memoryCache;
			_mapper = mapper;
		}

		#region Get LookUps
		public async Task<wap.Response<bool>> Add(dto.LookupSet.DTOLookUpAdd lookUp)
		{

			var _afterMaps = _mapper.Map<ent.LookupSet>(lookUp);

			_afterMaps.IsActive = true;

			return new wap.Response<bool>()
			{
				Data = await _manager.LookupSet.AddAsync(_afterMaps)
			};


		}
		public async Task<wap.Response<bool>> Update(dto.LookupSet.DTOLookUpAdd lookUp)
		{

			var obj = await _manager.LookupSet.GetEncryptByIdAsync(lookUp.Id);

			if (obj != null)
			{
				obj.ModifiedOn = DateTime.Now;
				obj.IsActive = lookUp.IsActive;
				obj.ModifiedBy = lookUp.ModifiedBy;
				obj.ExtraData2 = lookUp.ExtraData2;
				obj.ExtraData1 = lookUp.ExtraData1;
				obj.ClassName = lookUp.ClassName;
				obj.DisplayNameAr = lookUp.DisplayNameAr;
				obj.DisplayNameEn = lookUp.DisplayNameEn;

				_manager.LookupSet.Update(obj);
				await _manager.CompleteAsync();

				return new wap.Response<bool>() { Data = true };
			}
			else
			{
				return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
			}


		}
		public async Task<wap.Response<bool>> Delete(string Id)
		{

			var obj = await _manager.LookupSet.GetEncryptByIdAsync(Id);

			if (obj != null)
			{
				await _manager.LookupSet.DeleteAsync(obj);
				await _manager.CompleteAsync();
				return new wap.Response<bool>() { Data = true };
			}
			else
			{
				return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
			}

		}
		public async Task<wap.Response<List<dto.LookupSet.DTOAllLookUps>>> GetAll()
		{
			List<dto.LookupSet.DTOAllLookUps> response;
			bool isExist = _memoryCache.TryGetValue(CashingType.Lookups.ToString(), out response);

			if (!isExist)
			{
				var res = await _manager.LookupSet.GetAll().Include(_ => _.LookupSetTerms).OrderBy(_ => _.LookupSetId).ToListAsync();
				response = _mapper.Map<List<dto.LookupSet.DTOAllLookUps>>(res);
				var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
				_memoryCache.Set(CashingType.Lookups.ToString(), response, cacheEntryOptions);

			}
			return new wap.Response<List<dto.LookupSet.DTOAllLookUps>>()
			{
				Data = response
			};
		}
		public async Task<wap.Response<List<dto.LookupSet.DTOAllLookUps>>> GetSourceFormsLookup()
		{
			var lookupClassList = new[] { "ActivityUnit", "Associated Equipment", "Boolean", "DoseEquivalentTypes", "DoseRateUnit\r\n", "DosimeterType", "EnergyUnit", "EquipmentType", "Manufacturer", "ManufacturerCountry", "NuclearMaterial", "PhysicalForm", "Purpose", "ShieldingMaterial", "SourceType", "Status", "TimeUnits\r\n", "TransactionType", "WeightUnit", "QuantityUnits" };

			List<dto.LookupSet.DTOAllLookUps> response;
			var res = await _manager.LookupSet.GetByQuery(x => lookupClassList.Contains(x.ClassName)).Include(_ => _.LookupSetTerms).OrderBy(_ => _.LookupSetId).ToListAsync();
			response = _mapper.Map<List<dto.LookupSet.DTOAllLookUps>>(res);

			return new wap.Response<List<dto.LookupSet.DTOAllLookUps>>()
			{
				Data = response
			};
		}

		public async Task<wap.Response<List<dto.LookupSet.DTOAllLookUps>>> GetRelatedItemsSetupLookup()
		{
			var lookupClassList = new[] { "Boolean", "Manufacturer", "ManufacturerCountry", "Status" };

			List<dto.LookupSet.DTOAllLookUps> response;
			var res = await _manager.LookupSet.GetByQuery(x => lookupClassList.Contains(x.ClassName)).Include(_ => _.LookupSetTerms).OrderBy(_ => _.LookupSetId).ToListAsync();
			response = _mapper.Map<List<dto.LookupSet.DTOAllLookUps>>(res);

			return new wap.Response<List<dto.LookupSet.DTOAllLookUps>>()
			{
				Data = response
			};
		}
		public async Task<wap.Response<dto.LookupSet.DTOAllLookUps>> GetById(string Id)
		{

			return new wap.Response<dto.LookupSet.DTOAllLookUps>()
			{
				Data = _mapper.Map<dto.LookupSet.DTOAllLookUps>(await _manager.LookupSet.GetEncryptByIdAsync(Id))
			};

		}
		public async Task<wap.Response<dto.LookupSet.DTOAllLookUps>> GetLookupsByClassName(string ClassName)
		{

			return new wap.Response<dto.LookupSet.DTOAllLookUps>()
			{
				Data = _mapper.Map<dto.LookupSet.DTOAllLookUps>(await _manager.LookupSet.GetByQuery(_ => _.ClassName == ClassName).Include(_ => _.LookupSetTerms).FirstOrDefaultAsync())
			};

		}
        public async Task<wap.Response<ent.LookupSet>> GetLookupsByClassNameForExcel(string ClassName)
        {

			return new wap.Response<ent.LookupSet>()
			{
				Data = await _manager.LookupSet.GetByQuery(_ => _.ClassName == ClassName).Include(_ => _.LookupSetTerms).FirstOrDefaultAsync()
            };

        }

        public async Task<wap.PagingResponse<List<dto.LookupSet.DTOAllLookUps>>> GetAllFunctional(wap.PagingRequest param)
		{
			// Initialize response
			var response = new PagingResponse<List<ent.LookupSet>>()
			{
				Draw = param.Draw
			};

			IQueryable<ent.LookupSet> dataList = _manager.LookupSet.GetAll();

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
			IQueryable<ent.LookupSet> query = null;
			if (!string.IsNullOrEmpty(param.Search.Value))
			{
				query = dataList
					.Where(r =>
					r.LookupSetId.ToString().Contains(param.Search.Value) ||
					r.ClassName.Contains(param.Search.Value) ||
					r.DisplayNameAr.Contains(param.Search.Value) ||
					r.DisplayNameEn.Contains(param.Search.Value) ||
					r.IsActive.ToString().Contains(param.Search.Value));
			}
			else if (searchByColumn) // search by column
			{
				foreach (var search in param.Columns)
				{
					if (!string.IsNullOrEmpty(search.Search.Value))
					{
						switch (search.Data)
						{
							case "lookupSetId":
								query = dataList.Where(u => u.LookupSetId.ToString().Contains(search.Search.Value));
								break;
							case "className":
								query = dataList.Where(u => u.ClassName.Contains(search.Search.Value));
								break;
							case "displayNameAr":
								query = dataList.Where(u => u.DisplayNameAr.Contains(search.Search.Value));
								break;
							case "displayNameEn":
								query = dataList.Where(u => u.DisplayNameEn.Contains(search.Search.Value));
								break;
							case "isActive":
								query = dataList.Where(u => u.IsActive.ToString().Contains(search.Search.Value));
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
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LookupSetId) : query.OrderByDescending(r => r.LookupSetId);
					break;
				case 1:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ClassName) : query.OrderByDescending(r => r.ClassName);
					break;
				case 2:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.DisplayNameAr) : query.OrderByDescending(r => r.DisplayNameAr);
					break;
				case 3:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.DisplayNameEn) : query.OrderByDescending(r => r.DisplayNameEn);
					break;
				case 4:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.IsActive) : query.OrderByDescending(r => r.IsActive);
					break;
			}

			response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
			var recordsTotal = query.Count();
			response.RecordsTotal = recordsTotal;
			response.RecordsFiltered = recordsTotal;
			response.Succeeded = true;

			return new PagingResponse<List<dto.LookupSet.DTOAllLookUps>>()
			{
				Succeeded = true,
				Data = _mapper.Map<List<dto.LookupSet.DTOAllLookUps>>(response.Data),
				Draw = response.Draw,
				RecordsTotal = response.RecordsTotal,
				RecordsFiltered = response.RecordsFiltered
			};

		}
		#endregion


		#region Insert / Update LookUps
		public async Task<wap.Response<List<dto.LookupSetTerm.DTOAllLookupSetTerm>>> GetLookupsBySetId(string Id)
		{

			int _Id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));

			return new wap.Response<List<dto.LookupSetTerm.DTOAllLookupSetTerm>>()
			{
				Data = _mapper.Map<List<dto.LookupSetTerm.DTOAllLookupSetTerm>>(await _manager.LookupSetTerm.GetByQuery(_ => _.LookupSetId == _Id).OrderBy(_ => _.LookupSetId).ToListAsync())
			};


		}
		//
		public async Task<wap.Response<bool>> UpdateLookUp(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{

			var _getById = await _manager.LookupSetTerm.GetEncryptByIdAsync(lookUp.LookupSetTermId);
			if (_getById != null)
			{

				_getById.DisplayNameAr = lookUp.DisplayNameAr;
				_getById.DisplayNameEn = lookUp.DisplayNameEn;
				_getById.ExtraData1 = lookUp.ExtraData1;
				_getById.ExtraData2 = lookUp.ExtraData2;
				_getById.Value = lookUp.Value;
				_getById.ModifiedOn = DateTime.Now;

				_manager.LookupSetTerm.Update(_getById);
				_manager.Complete();

				return new wap.Response<bool>()
				{
					Data = true
				};
			}
			else
			{
				return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
			}


		}
		public async Task<wap.Response<bool>> AddLookUp(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{

			return new wap.Response<bool>()
			{
				Data = await _manager.LookupSetTerm.AddAsync(
					new ent.LookupSetTerm
					{
						LookupSetId = int.Parse(enc.EncryptQueryURL.Decrypt(lookUp.LookupSetId)),
						CreatedOn = DateTime.Now,
						DisplayNameAr = lookUp.DisplayNameAr,
						DisplayNameEn = lookUp.DisplayNameEn,
						ExtraData1 = lookUp.ExtraData1,
						ExtraData2 = lookUp.ExtraData2,
						IsActive = lookUp.IsActive,
						Value = lookUp.Value,
						ModifiedOn = DateTime.Now
					})
			};

		}
		public async Task<wap.Response<bool>> DeleteLookUp(string Id)
		{

			var obj = await _manager.LookupSetTerm.GetEncryptByIdAsync(Id);

			if (obj != null)
			{
				await _manager.LookupSetTerm.DeleteAsync(obj);
				await _manager.CompleteAsync();
				return new wap.Response<bool>() { Data = true };
			}
			else
			{
				return new wap.Response<bool>() { Succeeded = false, Message = "NOTEXIST" };
			}

		}
		private string GetClassName(Application.Enums.LookUpSetClassType classType)
		{
			string _className = "";
			switch (classType)
			{
				case Application.Enums.LookUpSetClassType.Status:
					_className = "Status";
					break;
				case Application.Enums.LookUpSetClassType.AssociatedEquipment:
					_className = "AssociatedEquipment";
					break;
				case Application.Enums.LookUpSetClassType.RecommendedWorkingLifetime:
					_className = "RecommendedWorkingLifetime";
					break;
				case Application.Enums.LookUpSetClassType.ShieldingMaterial:
					_className = "ShieldingMaterial";
					break;
			}

			return _className;
		}
		private async Task<ent.LookupSet> GetLookUpObjectByClassName(string ClassName)
		{
			return await _manager.LookupSet.GetByQuery(_ => _.ClassName == ClassName).FirstOrDefaultAsync();
		}
		public async Task<wap.PagingResponse<List<dto.LookupSetTerm.DTOLookupSetTerm>>> GetAllLookUpsTermFunctional(wap.PagingRequest param)
		{
			// Initialize response
			var response = new PagingResponse<List<ent.LookupSetTerm>>()
			{
				Draw = param.Draw
			};

			var _lookup = await _manager.LookupSet.GetByQuery(_ => _.LookupSetId == int.Parse(enc.EncryptQueryURL.Decrypt(param.extra))).FirstOrDefaultAsync();

			// Get all
			IQueryable<ent.LookupSetTerm> dataList = _manager.LookupSetTerm.GetByQuery(_ => _.LookupSetId == _lookup.LookupSetId);

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
			IQueryable<ent.LookupSetTerm> query = null;
			if (!string.IsNullOrEmpty(param.Search.Value))
			{
				query = dataList
					.Where(r =>
					r.DisplayNameAr.Contains(param.Search.Value) ||
					r.DisplayNameEn.Contains(param.Search.Value) ||
					r.Value.Contains(param.Search.Value));
			}
			else if (searchByColumn) // search by column
			{
				foreach (var search in param.Columns)
				{
					if (!string.IsNullOrEmpty(search.Search.Value))
					{
						switch (search.Data)
						{
							case "displayNameAr":
								query = dataList.Where(u => u.DisplayNameAr.Contains(search.Search.Value));
								break;
							case "displayNameEn":
								query = dataList.Where(u => u.DisplayNameEn.Contains(search.Search.Value));
								break;
							case "value":
								query = dataList.Where(u => u.Value.Contains(search.Search.Value));
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
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.DisplayNameAr) : query.OrderByDescending(r => r.DisplayNameAr);
					break;
				case 1:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.DisplayNameEn) : query.OrderByDescending(r => r.DisplayNameEn);
					break;
				case 2:
					query = colOrder.Dir == "asc" ? query.OrderBy(r => r.Value) : query.OrderByDescending(r => r.Value);
					break;
			}

			response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
			var recordsTotal = query.Count();
			response.RecordsTotal = recordsTotal;
			response.RecordsFiltered = recordsTotal;
			response.Succeeded = true;

			return new PagingResponse<List<dto.LookupSetTerm.DTOLookupSetTerm>>()
			{
				Succeeded = true,
				Data = _mapper.Map<List<dto.LookupSetTerm.DTOLookupSetTerm>>(response.Data),
				RecordsTotal = response.RecordsTotal,
				RecordsFiltered = response.RecordsFiltered

			};

		}
		#endregion

	}
}
