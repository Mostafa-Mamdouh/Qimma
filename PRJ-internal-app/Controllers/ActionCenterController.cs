using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRJ.Application.DTOs;
using PRJ.Application.DTOs.Common;
using PRJ.Application.Warppers;
using PRJ.Business;

namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionCenterController : ControllerBase
    {
        private readonly ActionCenterManager _manager;
        private readonly EnquiryCenterManager _enquiryManager;

        private readonly NotificationManager _notificationManager;
        public ActionCenterController(ActionCenterManager manager, NotificationManager notificationManager, EnquiryCenterManager enquiryManager)
        {
            _manager = manager;
            _notificationManager = notificationManager;
            _enquiryManager = enquiryManager;
        }

        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
        [HttpPost("GetAllEnquiryFuncational")]
        public async Task<IActionResult> GetAllEnquiryFunctional([FromBody] EnquiryPagingRequest param)
        {
            return Ok(await _enquiryManager.GetAllFunctional(param));
        }

        [HttpPost("AddNewStatusHistory")]
        public async Task<IActionResult> AddNewStatusHistory(DTOItemSourceStatusHistoryEditor param)
        {
            return Ok(await _manager.AddNewStatusHistory(param));
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailRequest dto)
        {
            await _notificationManager.SendEmailAsync(dto);
            var msg = new DTOItemSourceMsgHistoryEditor();
            msg.MsgText = dto.Body;
            msg.CreatedBy = msg.CreatedBy;
            msg.SourceId = dto.Id;
            return Ok(await _manager.AddNewMsgHistory(msg));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _manager.GetById(id));
        }


    }
}
