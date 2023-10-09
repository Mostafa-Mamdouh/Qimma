using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ItemHierarchyStructure")]
   
    public class ItemHierarchyStructureController : ControllerBase
    {
        private readonly bus.ItemHierarchyStructure.ItemHierarchyStructureManager _manager;

        public ItemHierarchyStructureController(bus.ItemHierarchyStructure.ItemHierarchyStructureManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dto.ItemHierarchyStructure.AddItemHierarchyStructureDto body)
        {
            return Ok(await _manager.AddItem(body));
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var res = _manager.GetItemHierarchyStructureTreeNode();
            return Ok(res);
        }
        [HttpPost]
        [Route("GetLevelTwo")]
        public IActionResult GetLevelTwo()
        {
            var res = _manager.GetLevelTwo();
            return Ok(res);
        }
        [HttpPost]
        [Route("GetLevelThreeByParent")]
        public IActionResult GetLevelThreeByParent(string parent)
        {
            var res = _manager.GetLevelThree(parent);
            return Ok(res);
        }
        
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _manager.Delete(code));
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.ItemHierarchyStructure.ItemHierarchyStructureDto body)
        {
            return Ok(await _manager.Update(body));
        }

        [HttpPost]
        [Route("GetByCode")]
        public async Task<IActionResult> GetByCode(string code)
        {
            return Ok(await _manager.GetByCode(code));
        }
    }
}
