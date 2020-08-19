using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

using Session;
using Commerce.Models;

namespace Commerce.Controllers
{
    public class ProductController : Controller
    {
        private Context _db;

        public ProductController(Context context)
        {
            _db = context;
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("Products");
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
