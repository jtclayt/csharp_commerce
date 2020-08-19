using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

using Session;
using Commerce.Models;

namespace Commerce.Controllers
{
    public class HomeController : Controller
    {
        private Context _db;

        public HomeController(Context context)
        {
            _db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("Index");
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
