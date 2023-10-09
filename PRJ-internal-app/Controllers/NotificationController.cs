using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRJ.Application.DTOs.Common;
using PRJ.Business;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]

    public class NotificationController  : ControllerBase
    {
        private readonly NotificationManager _manager;
        public NotificationController(NotificationManager manager)
        {
            _manager = manager;
        }
        #region Notification
       
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailRequest dto)
        {
            await _manager.SendEmailAsync(dto);
            return Ok();
        }
      
        #endregion
    }
}
