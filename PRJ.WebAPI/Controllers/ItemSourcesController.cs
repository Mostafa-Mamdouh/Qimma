using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRJ.Application.DTOs;
using PRJ.Application.Warppers;
using PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ItemSourcesApi")]

    public class ItemSourcesController : ControllerBase
    {
        private readonly ItemSourceManager _manager;

        public ItemSourcesController(ItemSourceManager manager)
        {
            _manager = manager;
        }

        [HttpPost("SaveTransactionItemSource")]
        public async Task<IActionResult> AddItemSourcesProfile(dto.DTOItemSourceEditor body)
        {
            return Ok(await _manager.SaveTransactionItemSource(body));
        }
        [HttpPost("GetLandPaginData")]
        public async Task<IActionResult> GetLanPagData(PagingRequest body)
        {
            return Ok(await _manager.GetAllFunctional(body));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _manager.GetByIdForLandingPage(id));
        }

        [HttpPost("DeleteDraft")]
        public async Task<IActionResult> DeleteDraft(string id)
        {
            return Ok(await _manager.DeleteDraft(id));
        }

        [HttpPost("CreateSimiler")]
        public async Task<IActionResult> CreateSimiler(string id)
        {
            return Ok(await _manager.CreateSimiler(id));
        }

        [HttpPost("EditItemSource")]
        public async Task<IActionResult> EditItemSource(DTOAddTrnItemSourceHistory body)
        {
            return Ok(await _manager.EditItemSource(body));
        }

        [HttpPost("GetSourcesByPermit")]
        public async Task<IActionResult> GetSourcesByPermit(string permitId, int sourceType)
        {
            return Ok(await _manager.GetSourcesByPermit(permitId, sourceType));
        }
    }
}
