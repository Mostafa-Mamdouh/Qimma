using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRJ_internal_app.Coomon;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ_internal_app.Controllers.Billing
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        [Route("GetLevelTwo")]
        public async Task<IActionResult> GetLevelTwo()
        {
            return Ok(await _manager.GetLevelTwo());
        }

        [HttpPost]
        [Route("GetLevelThreeByParent")]
        public async Task<IActionResult> GetLevelThreeByParent(string parent)
        {
            return Ok(await _manager.GetLevelThree(parent));
        }

        
        /*[HttpPost]
        [Route("GetAllFuncational")]
        public async Task<IActionResult> GetAllFuncational(string body)
        {
            return Ok(await _manager.GetAllFuncational(body));
        }*/





    }
}
