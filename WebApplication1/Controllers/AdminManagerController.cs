using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace UTC_Library.Controllers
{
    public class AdminManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminManagerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
            {
                context.Result = RedirectToAction("Login", "Main");
            }
            else
            {
                ViewBag.AdminName = _context.Admins.Find(adminId)?.AdminName ?? "Admin";
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            var user = _context.Users.ToList();
            return Json(new { data = user });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.ToList();
            return Json(new { data = books });
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Sử dụng phương thức BadRequest để trả về lỗi 400
            }

            var book = _context.Books.Find(id); // Thay đổi từ bookDb.AdminManager sang _context.Books
            if (book == null)
            {
                return NotFound(); // Sử dụng phương thức NotFound để trả về lỗi 404
            }

            return View(book);
        }

        // GET: AdminManager/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BookId,BookTitle,BookCategory,BookAuthor,BookCopies,BookPub,BookPubName,BookISBN,Copyright,DateAdded,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book); // Thêm sách vào DbContext
                _context.SaveChanges();
                TempData["OperationMsg"] = "Book added successfully"; // Sử dụng TempData thay vì Session
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Remove the TempData used for alerts
        public IActionResult OperationAlert()
        {
            TempData.Remove("OperationMsg");
            return RedirectToAction("Index");
        }

        // GET: AdminManager/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Sử dụng phương thức BadRequest để trả về lỗi 400
            }

            var book = _context.Books.Find(id); // Thay đổi từ bookDb.AdminManager sang _context.Books
            if (book == null)
            {
                return NotFound(); // Sử dụng phương thức NotFound để trả về lỗi 404
            }
            return View(book);
        }

        // POST: AdminManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("BookId,BookTitle,BookCategory,BookAuthor,BookCopies,BookPub,BookPubName,BookISBN,Copyright,DateAdded,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified; // Đặt trạng thái là Modified
                _context.SaveChanges();
                TempData["OperationMsg"] = "Book updated successfully"; // Sử dụng TempData thay vì Session
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: AdminManager/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Sử dụng phương thức BadRequest để trả về lỗi 400
            }

            var book = _context.Books.Find(id); // Thay đổi từ bookDb.AdminManager sang _context.Books
            if (book == null)
            {
                return NotFound(); // Sử dụng phương thức NotFound để trả về lỗi 404
            }
            return View(book);
        }

        // POST: AdminManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var book = _context.Books.Find(id); // Tìm sách theo ID
            //if (book == null)
            //{
            //    return NotFound(); // Nếu sách không tồn tại, trả về lỗi 404
            //}

            //// Kiểm tra xem sách có đang được mượn không
            //var isBookBorrowed = _context.Transactions
            //    .Any(t => t.BookId == id && t.TranStatus != "Returned"); // "Returned" là trạng thái khi sách đã được trả

            //if (isBookBorrowed)
            //{
            //    // Nếu sách đang được mượn, hiển thị thông báo lỗi
            //    TempData["ErrorMessage"] = "Không thể xóa sách vì hiện có người đang mượn.";
            //    return RedirectToAction("Index");
            //}

            //// Nếu không có ai mượn, tiến hành xóa sách
            //_context.Books.Remove(book);
            //_context.SaveChanges();

            //TempData["SuccessMessage"] = "Xóa sách thành công.";
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose(); // Giải phóng DbContext
            }
            base.Dispose(disposing);
        }
    }
}
