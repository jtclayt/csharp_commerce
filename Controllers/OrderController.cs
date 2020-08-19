using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
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

            User user = HttpContext.Session.Get<User>("user");

            Order[] allOrders = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderedProduct)
                .Where(o => o.Customer.UserId == user.UserId)
                .ToArray();
            return View("Orders", allOrders);
        }

        [HttpPost("orders")]
        public IActionResult CreateOrder(NewOrderWrapper postData)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                Product prod = _db.Products
                    .SingleOrDefault(p => p.ProductId == postData.Form.ProductId);

                if (postData.Form.Quantity <= prod.Quantity)
                {
                    postData.Form.UserId = HttpContext.Session.Get<User>("user").UserId;
                    _db.Add(postData.Form);
                    prod.Quantity -= postData.Form.Quantity;
                    prod.UpdatedAt = DateTime.Now;
                    _db.SaveChanges();
                    return RedirectToAction("Orders");
                }

                ModelState.AddModelError("Form.Quantity", "Not enough product in stock.");
            }
            return NewOrder(postData.Form.ProductId);
        }

        [HttpGet("orders/{productId}/new")]
        public IActionResult NewOrder(int productId)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Login", "User");
            }

            NewOrderWrapper data = new NewOrderWrapper();
            data.Form = new Order();
            data.Form.ProductId = productId;
            data.CurrentProduct = _db.Products
                .SingleOrDefault(p => p.ProductId == productId);
            return View("NewOrder", data);
        }

        private bool CheckUser()
        {
            ViewData["user"] = HttpContext.Session.Get<User>("user");
            return ViewData["user"] != null;
        }
    }
}
