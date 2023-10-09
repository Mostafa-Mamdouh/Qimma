using Microsoft.AspNetCore.Identity;
using PRJ.Business.InternalUserMaster;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Coomon
{
	public class Helper
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;
		private readonly InternalUserMasterManager _userManager;
		public Helper(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, InternalUserMasterManager userManager)
		{
			this._httpContextAccessor = httpContextAccessor;
			this._configuration = configuration;
			this._userManager = userManager;
		}

		public wap.Response<dto.InternalMasterUser.DTOInternalMasterUserBasic> GetCurrentUser()
		{
			if(_configuration["Environment"].ToString() == "Devlopment")
			{
				return _userManager.GetUserInfo("NRRC\\RaedShabatat");
			}

			return _userManager.GetUserInfo(_httpContextAccessor.HttpContext.User.Identity.Name);
		}
		public string GetSiteURL()
		{
			return _configuration["SiteURL"].ToString();
		}
	}
}