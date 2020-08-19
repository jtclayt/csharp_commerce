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
            Product[] allProducts = _db.Products
                .OrderBy(p => p.Name)
                .ToArray();
            return View("Products", allProducts);
        }

        [HttpPost("products")]
        public IActionResult CreateProduct(Product newProduct)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                newProduct.UserId = HttpContext.Session.Get<User>("user").UserId;
                _db.Add(newProduct);
                _db.SaveChanges();
                return RedirectToAction("Products");
            }
            return NewProduct();
        }

        [HttpGet("products/new")]
        public IActionResult NewProduct()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            return View("NewProduct");
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
