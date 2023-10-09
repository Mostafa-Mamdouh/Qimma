using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using PRJ.Application.Warppers;
using PRJ_ID4Server.Models;
using System.Diagnostics;


namespace PRJ_ID4Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            //var test = _localizer["lang"];
            //ViewData["lang"] = test;
            return View();
        }

        [HttpPost]
        public ActionResult<Response<string>> Login(LoginModel data)
        {
            Response<string> res = new Response<string>();

            if (data.Email.Equals("admin@g.com"))
            {
                res.Message = "Working well!!!";
                res.Succeeded = true;
                return res;
            }

            res.Message = "Not Working!!!";
            res.Succeeded = false;

            return res;
        }

        public IActionResult RestorePassword()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}