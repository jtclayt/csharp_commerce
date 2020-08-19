using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

using Session;
using Commerce.Models;

namespace Commerce.Controllers
{
    public class OrderController : Controller
    {
        private Context _db;

        public OrderController(Context context)
        {
            _db = context;
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("Orders");
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
