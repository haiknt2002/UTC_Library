using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace UTC_Library.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IActionResult Login()
        {
            return View();
        }
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Validate(User model)
        {
            Console.WriteLine("-------------------Validate--------------------------");

            var user = _context.Users
                .FirstOrDefault(a => a.UserEmail == model.UserEmail && a.UserPass == model.UserPass);

            if (user != null)
            {
                Console.WriteLine("-------------------Not Null--------------------------");

                // Lưu ID admin vào session
                HttpContext.Session.SetInt32("UserId", user.UserId);

                // Đăng nhập thành công, chuyển hướng đến trang quản lý
                return RedirectToAction("Index", "UserManager", new { userId = user.UserId, userName = user.UserName });
            }
            else
            {
                ViewBag.Message = "Email hoặc mật khẩu không chính xác.";
                return View("Login");
            }
        }
        // User logout, redirect to main. 
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Home", "Main");
        }
    }

}
