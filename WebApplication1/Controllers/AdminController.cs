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
            // Kiểm tra xem người dùng có tồn tại không
            var admin = _context.Admins
                .FirstOrDefault(a => a.AdminEmail == model.AdminEmail && a.AdminPassword == model.AdminPassword);

            if (admin != null)
            {
                // Đăng nhập thành công
                return RedirectToAction("Index", "AdminManager");
            }
            else
            {
                // Đăng nhập thất bại, thông báo lỗi
                ViewBag.Message = "Email hoặc mật khẩu không chính xác.";
                return View("Login");
            }
        }
    }
}
