using System.Data;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(AgentViewModel avm)
        {


            if(avm.NIK == null || avm.NIK.Length < 6)
                return View("Index");
                //return Json(new { Success = false });

            TempData["Agent"] = JsonConvert.SerializeObject(avm);

            return View("Index");
            //return Json (new { Success = true });
        }

        public ActionResult Login()
        {
                return View();
        }

        public ActionResult LoginAttempt(LoginViewModel lvm)
        {

            var avmJson = TempData.Peek("Agent");
            TempData.Keep("Agent");

            var avm = avmJson == null ? new AgentViewModel() : JsonConvert.DeserializeObject<AgentViewModel>(avmJson.ToString());

            if (avm.Email == lvm.Email && avm.Password == lvm.Password)
                return RedirectToAction("Welcome");
            else
                return View("Login");

        }

        public ActionResult Welcome()
        {
            var avmJson = TempData.Peek("Agent");
            TempData.Keep("Agent");

            var avm = avmJson == null ? new AgentViewModel() : JsonConvert.DeserializeObject<AgentViewModel>(avmJson.ToString());
            return View(avm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
