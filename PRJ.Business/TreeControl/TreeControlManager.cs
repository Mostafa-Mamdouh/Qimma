using AutoMapper;
using PRJ.Application.DTOs.Tree;
using PRJ.Application.Enums;
using rep = PRJ.Repository;


namespace PRJ.Business.TreeControl
{
    public class TreeControlManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public TreeControlManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<string> GetNextLevelNum(GetNextLevelNumDto getNextLevelNumDto)
        {
            
                string nextLevelNum = null;
                //Get Level Settings.
                var levelData = _manager.TreeControl.GetByQuery(t => t.TreeCode == getNextLevelNumDto.TreeCode && t.LevelNum == getNextLevelNumDto.LevelNum).FirstOrDefault();

                if (getNextLevelNumDto.TreeCode == TreeControlValues.Billing.ToString())
                {
                    if (getNextLevelNumDto.LevelNum == 1)
                    {
                        var lastItem = _manager.ItemHierarchyStructure.GetByQuery(i => i.StructureLevelNum == 1).OrderByDescending(i => i.ItemStructureCode).FirstOrDefault();
                        if (lastItem != null)
                        {
                            nextLevelNum = (Convert.ToInt32(lastItem.ItemStructureCode) + 1).ToString();
                        }
                        else
                        {
                            nextLevelNum = levelData.StartingNum.ToString();
                        }
                    }
                    else
                    {
                        var lastItem = _manager.ItemHierarchyStructure.GetByQuery(i => i.StructureLevelNum == getNextLevelNumDto.LevelNum && i.ParentStructure == getNextLevelNumDto.ParentCode).ToList();
                        var padding = levelData.LevelPadding;
                        var length = levelData.LevelLength;
                        string newPadding = null;

                        if (lastItem.Count == 0)
                        {
                            var startNum = levelData.StartingNum;
                            var startNumCount = startNum;
                            var numberOfDigitsInStartNum = 0;
                            while (startNumCount > 0)
                            {
                                startNumCount = startNumCount / 10;
                                numberOfDigitsInStartNum++;
                            }
                            if (numberOfDigitsInStartNum == length)
                            {
                                nextLevelNum = getNextLevelNumDto.ParentCode + startNum.ToString();
                            }
                            else
                            {
                                var numOfPadding = length - numberOfDigitsInStartNum;
                                for (int i = 0; i < Convert.ToInt32(numOfPadding); i++)
                                {
                                    newPadding = newPadding + padding;
                                }
                                newPadding = newPadding + startNum.ToString();
                                nextLevelNum = getNextLevelNumDto.ParentCode + newPadding;
                            }
                        }
                        else
                        {
                            var interval = levelData.LevelInterval;
                            var newCode = ((lastItem.Count + 1) * interval) - interval;
                            var newCodeCount = newCode;
                            var numberOfDigitsInNewCode = 0;
                            while (newCodeCount > 0)
                            {
                                newCodeCount = newCodeCount / 10;
                                numberOfDigitsInNewCode++;
                            }
                            if (numberOfDigitsInNewCode == length)
                            {
                                nextLevelNum = getNextLevelNumDto.ParentCode + newCode.ToString();
                            }
                            else
                            {
                                var numOfPadding = length - numberOfDigitsInNewCode;
                                for (int i = 0; i < Convert.ToInt32(numOfPadding); i++)
                                {
                                    newPadding = newPadding + padding;
                                }
                                newPadding = newPadding + newCode.ToString();
                                nextLevelNum = getNextLevelNumDto.ParentCode + newPadding;
                            }
                        }
                    }
                }else if(getNextLevelNumDto.TreeCode == TreeControlValues.RelatedItem.ToString())
                {
                    if (getNextLevelNumDto.LevelNum == 1)
                    {
                        var lastItem = _manager.RelatedItemHierarchy.GetByQuery(i => i.StructureLevelNum == 1).OrderByDescending(i => i.ItemStructureCode).FirstOrDefault();
                        if (lastItem != null)
                        {
                            nextLevelNum = (Convert.ToInt32(lastItem.ItemStructureCode) + 1).ToString();
                        }
                        else
                        {
                            nextLevelNum = levelData.StartingNum.ToString();
                        }
                    }
                    else
                    {
                        var lastItem = _manager.RelatedItemHierarchy.GetByQuery(i => i.StructureLevelNum == getNextLevelNumDto.LevelNum && i.ParentStructure == getNextLevelNumDto.ParentCode).ToList();
                        var padding = levelData.LevelPadding;
                        var length = levelData.LevelLength;
                        string newPadding = null;

                        if (lastItem.Count == 0)
                        {
                            var startNum = levelData.StartingNum;
                            var startNumCount = startNum;
                            var numberOfDigitsInStartNum = 0;
                            while (startNumCount > 0)
                            {
                                startNumCount = startNumCount / 10;
                                numberOfDigitsInStartNum++;
                            }
                            if (numberOfDigitsInStartNum == length)
                            {
                                nextLevelNum = getNextLevelNumDto.ParentCode + startNum.ToString();
                            }
                            else
                            {
                                var numOfPadding = length - numberOfDigitsInStartNum;
                                for (int i = 0; i < Convert.ToInt32(numOfPadding); i++)
                                {
                                    newPadding = newPadding + padding;
                                }
                                newPadding = newPadding + startNum.ToString();
                                nextLevelNum = getNextLevelNumDto.ParentCode + newPadding;
                            }
                        }
                        else
                        {
                            var interval = levelData.LevelInterval;
                            var newCode = ((lastItem.Count + 1) * interval) - interval;
                            var newCodeCount = newCode;
                            var numberOfDigitsInNewCode = 0;
                            while (newCodeCount > 0)
                            {
                                newCodeCount = newCodeCount / 10;
                                numberOfDigitsInNewCode++;
                            }
                            if (numberOfDigitsInNewCode == length)
                            {
                                nextLevelNum = getNextLevelNumDto.ParentCode + newCode.ToString();
                            }
                            else
                            {
                                var numOfPadding = length - numberOfDigitsInNewCode;
                                for (int i = 0; i < Convert.ToInt32(numOfPadding); i++)
                                {
                                    newPadding = newPadding + padding;
                                }
                                newPadding = newPadding + newCode.ToString();
                                nextLevelNum = getNextLevelNumDto.ParentCode + newPadding;
                            }
                        }
                    }
                }
                return nextLevelNum;
            
        }
    }
}
