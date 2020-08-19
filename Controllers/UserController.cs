using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Session;
using Commerce.Models;

namespace Commerce.Controllers
{
    public class UserController : Controller
    {
        private Context _db;

        public UserController(Context context)
        {
            _db = context;
        }

        [HttpGet("users/register")]
        public ViewResult RegisterForm()
        {
            return View("RegisterForm");
        }

        [HttpPost("users/register")]
        public IActionResult RegisterForm(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (!_db.Users.Any(u => u.Email == newUser.Email))
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    newUser.Password = hasher.HashPassword(newUser, newUser.Password);
                    _db.Users.Add(newUser);
                    _db.SaveChanges();
                    HttpContext.Session.Set<User>("user", newUser);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Email", "Email already in use!");
            }
            return RegisterForm();
        }

        [HttpGet("users/login")]
        public ViewResult LoginForm()
        {
            return View("LoginForm");
        }

        [HttpPost("users/login")]
        public IActionResult Login(LoginUser logUser)
        {
            if (ModelState.IsValid)
            {
                User user = _db.Users
                    .FirstOrDefault(u => u.Email == logUser.Email);
                if (user != null)
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    var isAuth = hasher.VerifyHashedPassword(
                        user,
                        user.Password,
                        logUser.Password
                    );
                    if (isAuth != 0)
                    {
                        HttpContext.Session.Set<User>("user", user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Email", "Invalid Email/Password");
            }
            return LoginForm();
        }

        [HttpGet("users/logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
