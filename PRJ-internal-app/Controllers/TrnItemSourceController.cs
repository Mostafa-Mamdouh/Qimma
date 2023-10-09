using Microsoft.AspNetCore.Mvc;
using PRJ.Business;

namespace PRJ_internal_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrnItemSourceController : ControllerBase
    {
        private readonly ItemSourceManager _manager;
        public TrnItemSourceController(ItemSourceManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetEditHistory")]
        public async Task<IActionResult> GetEditHistory(string id, string trnType)
        {
            return Ok(await _manager.GetEditHistory(id, trnType));
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _manager.GetById(id));
        }
    }
}
