using AutoMapper;
using PRJ.Application.DTOs.RelatedItems.Hierarchy;
using PRJ.Application.DTOs.Tree;
using PRJ.Application.Enums;
using cns = PRJ.GlobalComponents.Const;
using Data = PRJ.Application.DTOs.RelatedItems.Hierarchy.Data;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.RelatedItems
{
    public class RelatedItemHierarchyManager
    {
        private readonly Business.TreeControl.TreeControlManager _tree;
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public RelatedItemHierarchyManager(rep.IUnitOfWork manager, IMapper mapper, TreeControl.TreeControlManager tree)
        {
            _manager = manager;
            _mapper = mapper;
            _tree = tree;
        }

        private RelatedItemsHierarchyTreeNode GetTreeNode(ent.RelatedItemsHierarchyStructure parent, List<ent.RelatedItemsHierarchyStructure> originalList)
        {
            var TreeNode = new RelatedItemsHierarchyTreeNode()
            {
                Data = new Data()
                {
                    ItemStructureCode = parent.ItemStructureCode.ToString(),
                    ItemStructureDesFrn = parent.ItemStructureDesFrn,
                    ItemStructureDesNtv = parent.ItemStructureDesNtv,
                    StructureLevelNum = parent.StructureLevelNum,
                    ParentStructure = parent.ParentStructure,
                    ItemStructureLongDes = parent.ItemStructureLongDes,
                    StructureGeneralDetailFlag = parent.StructureGeneralDetailFlag,
                    HSCode = parent.HSCode,
                    ItemNo = parent.ItemNo,
                },
                Children = originalList.Where(x => x.ParentStructure == parent.ItemStructureCode.ToString())
               .Select(x => GetTreeNode(x, originalList))
               .OrderBy(x => x.Data.ParentStructure)
               .ToList()
            };
            return TreeNode;
        }
        public List<RelatedItemsHierarchyTreeNode> GetRelatedItemsTreeNode(string itemStrDes)
        {
            var allItems = _manager.RelatedItemHierarchy.GetByQuery(_x => _x.ItemStructureCode != null).ToList();
            List<RelatedItemsHierarchyTreeNode> treeNode = new List<RelatedItemsHierarchyTreeNode>();
            if (!string.IsNullOrEmpty(itemStrDes))
            {
                treeNode = allItems.Where(x => string.IsNullOrWhiteSpace(x.ParentStructure))
                .Select(x => GetTreeNode(x, allItems))
                .OrderBy(x => x.Data.ItemStructureCode)
                .ToList();
            }
            else
            {
                treeNode = allItems.Where(x => string.IsNullOrWhiteSpace(x.ParentStructure))
               .Select(x => GetTreeNode(x, allItems))
               .OrderBy(x => x.Data.ItemStructureCode)
               .ToList();
            }

            return treeNode;
        }

        public async Task<wap.Response<string>> AddItem(dto.RelatedItems.Hierarchy.DTORelatedItemsHierarchy body)
        {

            GetNextLevelNumDto node = new GetNextLevelNumDto();
            node.TreeCode = TreeControlValues.RelatedItem.ToString();
            node.LevelNum = (int)body.StructureLevelNum;
            node.ParentCode = body.ParentStructure;
            /* get new node code */
            var res = await _tree.GetNextLevelNum(node);
            body.ItemStructureCode = res;
            var newItem = new ent.RelatedItemsHierarchyStructure();
            newItem = _mapper.Map<ent.RelatedItemsHierarchyStructure>(body);
            await _manager.RelatedItemHierarchy.AddAsync(newItem);
            var item = await _manager.RelatedItemHierarchy.GetByIdAsync(node.ParentCode);
            if (item != null)
            {
                item.StructureGeneralDetailFlag = 1;
                _manager.RelatedItemHierarchy.Update(item);
            }
            await _manager.CompleteAsync();
            return new wap.Response<string>()
            {
                Succeeded = true,
                Data = body.ItemStructureCode
            };


        }

        public async Task<wap.Response<bool>> Delete(string code)
        {

            var item = _manager.RelatedItemHierarchy.GetById(code);
            if (item == null)
            {
                return new wap.Response<bool>()
                {
                    Succeeded = false,
                    Data = false,
                    Message = cns.ConstErrors.NotFound
                };
            }
            var isParent = _manager.RelatedItemHierarchy.GetByQuery(i => i.ItemStructureCode == code && i.StructureGeneralDetailFlag == 1).Count();
            if (isParent > 0)
            {
                return new wap.Response<bool>()
                {
                    Succeeded = false,
                    Data = false,
                    Message = cns.ConstErrors.NodeHasChildren
                };
            }
            await _manager.RelatedItemHierarchy.DeleteAsync(item);
            await _manager.CompleteAsync();

            return new wap.Response<bool>()
            {
                Succeeded = true,
                Data = true,
            };

        }

        public async Task<wap.Response<dto.RelatedItems.DTOEditRelatedItems>> Update(dto.RelatedItems.DTOEditRelatedItems body)
        {

            var item = await _manager.RelatedItemHierarchy.GetByIdAsync(body.ItemStructureCode);
            if (item == null)
            {
                return new wap.Response<dto.RelatedItems.DTOEditRelatedItems>()
                {
                    Succeeded = false,
                    Data = null,
                    Message = cns.ConstErrors.NotFound
                };
            }
            var Data = _mapper.Map<ent.RelatedItemsHierarchyStructure>(body);
            _manager.RelatedItemHierarchy.Update(Data);
            _manager.Complete();

            return new wap.Response<dto.RelatedItems.DTOEditRelatedItems>()
            {
                Succeeded = true,
            };

        }

        public async Task<wap.Response<DTORelatedItemsHierarchy>> GetByCode(string code)
        {

            var item = await _manager.RelatedItemHierarchy.GetByIdAsync(code);

            if (item == null)
            {
                return new wap.Response<DTORelatedItemsHierarchy>()
                {
                    Succeeded = false,
                    Data = null,
                    Message = cns.ConstErrors.NotFound
                };
            }
            return new wap.Response<DTORelatedItemsHierarchy>()
            {
                Succeeded = true,
                Data = _mapper.Map<DTORelatedItemsHierarchy>(item)
            };


        }

        public async Task<wap.Response<List<DTORelatedItemsHierarchyAsLookup>>> GetAsLookup()
        {
            var items = await _manager.RelatedItemHierarchy.GetAllAsync();
            var resp = _mapper.Map<List<DTORelatedItemsHierarchyAsLookup>>(items);
            if (resp.Count > 0)
            {
                return new wap.Response<List<DTORelatedItemsHierarchyAsLookup>>()
                {
                    Succeeded = true,
                    Data = resp,
                };
            }
            return new wap.Response<List<DTORelatedItemsHierarchyAsLookup>>()
            {
                Succeeded = false,
                Data = null,
                Message = cns.ConstErrors.NotFound
            };
        }
    }
}
