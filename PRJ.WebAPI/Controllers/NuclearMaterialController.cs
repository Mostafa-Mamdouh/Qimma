using Microsoft.AspNetCore.Mvc;
using PRJ.Application.DTOs.NuclearMaterial.Shield;
using PRJ.Business.NuclearMaterial;

namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NuclearMaterialController : ControllerBase
    {
        private readonly NuclearMaterialManager _manager;

        public NuclearMaterialController(NuclearMaterialManager manager)
        {
            _manager = manager;
        }

        [HttpPost("AddNuclearShield")]
        public async Task<IActionResult> AddNuclearShield(DTOShield param)
        {
            return Ok(await _manager.AddNuclearShield(param));
        }

        [HttpPost("GetNuclearMaterialsAsLookUp")]
        public async Task<IActionResult> GetNuclearMaterialsAsLookUp(string id)
        {
            return Ok(await _manager.GetNuclearMaterialsAsLookUp(id));
        }

        [HttpPost("GetNuclearMaterialsForTransaction")]
        public async Task<IActionResult> GetNuclearMaterialsForTransaction(string id, string sourceId)
        {
            return Ok(await _manager.GetNuclearMaterialsForTransaction(id, sourceId));
        }
    }
}
