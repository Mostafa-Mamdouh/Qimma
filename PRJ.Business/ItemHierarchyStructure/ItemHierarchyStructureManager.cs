using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.ItemHierarchyStructure;
using PRJ.Application.DTOs.Tree;
using PRJ.Application.Enums;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.ItemHierarchyStructure
{
    public class ItemHierarchyStructureManager
    {
        private readonly Business.TreeControl.TreeControlManager _tree;
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public ItemHierarchyStructureManager(rep.IUnitOfWork manager, IMapper mapper, TreeControl.TreeControlManager tree)
        {
            _manager = manager;
            _mapper = mapper;
            _tree = tree;
        }

        private ItemHierarchyStructureTreeNode GetTreeNode(ent.Billing.ItemHierarchyStructure parent, List<ent.Billing.ItemHierarchyStructure> originalList)
        {
            return new ItemHierarchyStructureTreeNode()
            {
                Data = new Data()
                {
                    DefaultIssueAccountCode = parent.DefaultIssueAccountCode,
                    ItemStructureCode = parent.ItemStructureCode.ToString(),
                    ItemStructureDesFrn = parent.ItemStructureDesFrn,
                    ItemStructureDesNtv = parent.ItemStructureDesNtv,
                    ParentStructure = parent.ParentStructure,
                    StructureGeneralDetailFlag = parent.StructureGeneralDetailFlag,
                    StructureLevelNum = parent.StructureLevelNum,
                },

                Children = originalList.Where(x => x.ParentStructure == parent.ItemStructureCode.ToString())
                .Select(x => GetTreeNode(x, originalList))
                .OrderBy(x => x.Data.ItemStructureCode)
                .ToList()
            };
        }

        public List<ItemHierarchyStructureTreeNode> GetItemHierarchyStructureTreeNode()
        {
            IQueryable<ent.Billing.ItemHierarchyStructure> allItems = _manager.ItemHierarchyStructure.GetAll();
            var itemsList = _mapper.Map<List<ent.Billing.ItemHierarchyStructure>>(allItems);
            List<ItemHierarchyStructureTreeNode> treeNode = new List<ItemHierarchyStructureTreeNode>();
            treeNode = itemsList.Where(x => string.IsNullOrWhiteSpace(x.ParentStructure))
                .Select(x => GetTreeNode(x, itemsList))
                .OrderBy(x => x.Data.ItemStructureCode)
                .ToList();
            return treeNode;
        }

        public async Task<wap.Response<string>> AddItem(dto.ItemHierarchyStructure.AddItemHierarchyStructureDto body)
        {
            
                GetNextLevelNumDto node = new GetNextLevelNumDto();
                node.TreeCode = TreeControlValues.Billing.ToString();
                node.LevelNum = body.StructureLevelNum;
                node.ParentCode = body.ParentStructure;
                /* get new node code */
                var res = await _tree.GetNextLevelNum(node);
                body.ItemStructureCode = res;
                var newItem = _mapper.Map<ent.Billing.ItemHierarchyStructure>(body);
                await _manager.ItemHierarchyStructure.AddAsync(newItem);
                await _manager.CompleteAsync();

                return new wap.Response<string>()
                {
                    Succeeded = true,
                    Data = body.ItemStructureCode
                };
            
           
        }

        public async Task<wap.Response<bool>> Delete(string code)
        {
           
                var item = _manager.ItemHierarchyStructure.GetById(code);
                if (item == null)
                {
                    return new wap.Response<bool>()
                    {
                        Succeeded = false,
                        Data = false,
                        Message = cns.ConstErrors.NotFound
                    };
                }

                var isParent = _manager.ItemHierarchyStructure.GetByQuery(i => i.ParentStructure == code).Count();

                if (isParent > 0)
                {
                    return new wap.Response<bool>()
                    {
                        Succeeded = false,
                        Data = false,
                        Message = cns.ConstErrors.NodeHasChildren
                    };
                }
                await _manager.ItemHierarchyStructure.DeleteAsync(item);
                await _manager.CompleteAsync();

                return new wap.Response<bool>()
                {
                    Succeeded = true,
                    Data = true,
                };
            
          
        }

        public async Task<wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>> Update(dto.ItemHierarchyStructure.ItemHierarchyStructureDto body)
        {
            
                var item = await _manager.ItemHierarchyStructure.GetByIdAsync(body.ItemStructureCode);
                if (item == null)
                {
                    return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                    {
                        Succeeded = false,
                        Data = null,
                        Message = cns.ConstErrors.NotFound
                    };
                }
                //var Data = _mapper.Map<ent.Billing.ItemHierarchyStructure>(body);
                //_manager.ItemHierarchyStructure.Update(Data);
                item.StructureGeneralDetailFlag = body.StructureGeneralDetailFlag;
                item.StructureLevelNum = body.StructureLevelNum;
                item.ItemStructureDesFrn = body.ItemStructureDesFrn;
                item.ItemStructureDesNtv = body.ItemStructureDesNtv;
                item.DefaultIssueAccountCode = body.DefaultIssueAccountCode;
                _manager.Complete();

                return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                {
                    Succeeded = true,
                };
            
            
        }
        public async Task<wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>> GetLevelTwo()
        {
            
                var item = _manager.ItemHierarchyStructure.GetByQuery(_ => _.StructureLevelNum == 2).ToList();

                if (item == null)
                {
                    return new wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>()
                    {
                        Succeeded = false,
                        Data = null,
                        Message = cns.ConstErrors.NotFound
                    };
                }
                return new wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>(item)
                };

            
           
        }

        public async Task<wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>> GetLevelThree(string parent)
        {
            
                var item = _manager.ItemHierarchyStructure.GetByQuery(_ => _.ParentStructure == parent).Where(_ => _.StructureLevelNum == 3).ToList();

                if (item == null)
                {
                    return new wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>()
                    {
                        Succeeded = false,
                        Data = null,
                        Message = cns.ConstErrors.NotFound
                    };
                }
                return new wap.Response<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>>(item)
                };

            
           
        }

        public async Task<wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>> GetByCode(string code)
        {
            
                var item = await _manager.ItemHierarchyStructure.GetByIdAsync(code);

                if (item == null)
                {
                    return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                    {
                        Succeeded = false,
                        Data = null,
                        Message = cns.ConstErrors.NotFound
                    };
                }
                return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>(item)
                };

            
           
        }

        public async Task<wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>> GetServiceItemByHierarchyCode(string code)
        {
           
                var item = await _manager.ItemHierarchyStructure.GetByIdAsync(code);

                if (item == null)
                {
                    return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                    {
                        Succeeded = false,
                        Data = null,
                        Message = cns.ConstErrors.NotFound
                    };
                }
                return new wap.Response<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<dto.ItemHierarchyStructure.ItemHierarchyStructureDto>(item)
                };

            
            
        }
    }

}
