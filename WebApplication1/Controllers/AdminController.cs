using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Validate(Admin model)
        {
            var admin = _context.Admins
                .FirstOrDefault(a => a.AdminEmail == model.AdminEmail && a.AdminPassword == model.AdminPassword);

            if (admin != null)
            {
                // Lưu ID admin vào session
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);

                // Đăng nhập thành công, chuyển hướng đến trang quản lý
                return RedirectToAction("Index", "AdminManager");
            }
            else
            {
                ViewBag.Message = "Email hoặc mật khẩu không chính xác.";
                return View("Login");
            }
        }
    }
}
