using Microsoft.AspNetCore.Mvc;
using PRJ.Application.DTOs.NuclearMaterial;
using PRJ.Business.NuclearMaterial;
using wap = PRJ.Application.Warppers;


namespace PRJ_internal_app.Controllers.NuclearMaterial
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

        [HttpPost("AddNuclearMaterial")]
        public async Task<IActionResult> AddNuclearMaterial(DTONuclearMaterial param)
        {
            return Ok(await _manager.AddNuclearMaterial(param));
        }

        [HttpPost("UpdateNuclearMaterial")]
        public async Task<IActionResult> UpdateNuclearMaterial(DTONuclearMaterial param)
        {
            return Ok(await _manager.UpdateNuclearMaterial(param));
        }

        [HttpPost("GetNuclearMaterials")]
        public async Task<IActionResult> GetNuclearMaterials()
        {
            return Ok(await _manager.GetNuclearMaterials());
        }

        [HttpPost("GetNuclearMaterialById")]
        public async Task<IActionResult> GetNuclearMaterialById(string id)
        {
            return Ok(await _manager.GetNuclearMaterialById(id));
        }

        [HttpPost("DeleteNuclearMaterial")]
        public async Task<IActionResult> DeleteNuclearMaterial(string id)
        {
            return Ok(await _manager.DeleteNuclearMaterial(id));
        }
        [HttpPost("GetNuclearMaterialsFunctional")]
        public async Task<IActionResult> GetNuclearMaterialsFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllNuclearMaterialFunctional(param));
        }
    }
}
