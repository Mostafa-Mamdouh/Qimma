using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Cryptography.X509Certificates;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BillingServiceTrnHeader")]

    public class BillingServiceTrnHeaderController : Controller
    {
       
            private readonly bus.BillingServiceTrnHeader.BillingServiceTrnHeaderManager _manager;
            public BillingServiceTrnHeaderController(bus.BillingServiceTrnHeader.BillingServiceTrnHeaderManager manager)
            {
                _manager = manager;
            }
            [HttpPost("GetAll")]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _manager.GetAll());
            }
            [HttpPost("GetServiceItemByHierarchyCode")]
            public async Task<IActionResult> GetServiceItemByHierarchyCode(string Code)
            {
                return Ok(await _manager.GetServiceItemByHierarchyCode(Code));
            }
            [HttpPost("GetAllFuncational")]
            public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
            {
                return Ok(await _manager.GetAllFunctional(param));
            }
            [HttpPost("GetWithId")]
            public async Task<IActionResult> GetWithId(string id)
            {
                return Ok(await _manager.GetWithId(id));
            }
            [HttpPost("Add")]
            public async Task<IActionResult> Add(dto.BillingServiceTrn.DTOAddBillingServiceTrnHeader Body)
            {
                return Ok(await _manager.Add(Body));
            }
            [HttpPost("Update")]
            public async Task<IActionResult> Update(dto.BillingServiceTrn.DTOUpdateBillingServiceTrn Body)
            {
                return Ok(await _manager.Update(Body));
            }


    }


}
