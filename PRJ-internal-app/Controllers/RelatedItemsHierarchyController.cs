using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ_internal_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedItemsHierarchyController : ControllerBase
    {
        private readonly bus.RelatedItems.RelatedItemHierarchyManager _manager;

        public RelatedItemsHierarchyController(bus.RelatedItems.RelatedItemHierarchyManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dto.RelatedItems.Hierarchy.DTORelatedItemsHierarchy body)
        {
            return Ok(await _manager.AddItem(body));
        }

        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll(string itemStr)
        {
            var res = _manager.GetRelatedItemsTreeNode(itemStr);
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
        public async Task<IActionResult> Update(dto.RelatedItems.DTOEditRelatedItems body)
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
        [Route("GetAsLookup")]
        public async Task<IActionResult> GetAsLookup()
        {
            return Ok(await _manager.GetAsLookup());
        }
    }
}
