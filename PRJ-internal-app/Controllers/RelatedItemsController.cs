using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;
namespace PRJ_internal_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedItemsController : ControllerBase
    {
        private readonly bus.RelatedItems.RelatedItemsManager _manager;

        public RelatedItemsController(bus.RelatedItems.RelatedItemsManager manager)
        {
            _manager = manager;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetRelatedItems()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetRelatedItemsById(string relatedItemCode)
        {
            return Ok(await _manager.GetRelatedItemsById(relatedItemCode));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateRelatedItems(dto.DTORelatedItem body)
        {
            return Ok(await _manager.UpdateRelatedItems(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRelatedItems(dto.DTORelatedItem body)
        {
            return Ok(await _manager.AddRelatedItems(body));
        }

        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }

        [HttpPost("NextRelatedItemCode")]
        public async Task<IActionResult> GetMaxItemCode()
        {
            return Ok(await _manager.GetMaxItemCode());
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteRelatedItemsById(string relatedItemCode)
        {
            return Ok(await _manager.DeleteRelatedItems(relatedItemCode));
        }

        [HttpPost("GetFunctionalwithStructureCode")]
        public async Task<IActionResult> GetFunctionalwithStructureCode(wap.PagingRequest param)
        {
            var value = "10095005";
            return Ok(_manager.GetFunctionalwithStructureCode(param));
        }

    }
}
