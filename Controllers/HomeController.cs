using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Dashboard()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }

            DashboardWrapper data = new DashboardWrapper();
            data.NewestProducts = _db.Products
                .OrderByDescending(p => p.CreateAt)
                .Take(5)
                .ToArray();

            data.RecentOrders = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderedProduct)
                .OrderByDescending(o => o.CreateAt)
                .Take(3)
                .ToArray();

            data.NewestUsers = _db.Users
                .OrderByDescending(u => u.CreatedAt)
                .Take(3)
                .ToArray();

            return View("Dashboard", data);
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
