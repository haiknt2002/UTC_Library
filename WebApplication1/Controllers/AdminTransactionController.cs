using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace UTC_Library.Controllers
{
    public class AdminTransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTransactionController(ApplicationDbContext context)
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
        // Returns admin request view, here admin can accept and reject the book requests
        public IActionResult Requests()
        {
            var transactions = _context.Transactions.ToList();
            return View(transactions);
        }

        // Returns all book requests in JSON format
        public IActionResult GetAllRequests()
        {
            var transactionList = _context.Transactions
                .Where(r => r.TranStatus == "Requested")
                .ToList();
            return Json(new { data = transactionList });
        }

        // Accepts the book request
        public IActionResult AcceptRequest(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest(); // Trả về mã 400 nếu tranId là null
            }

            var transaction = _context.Transactions
                .FirstOrDefault(t => t.TranId == tranId);

            if (transaction == null)
            {
                return NotFound(); // Trả về mã 404 nếu không tìm thấy giao dịch
            }

            transaction.TranStatus = "Accepted";
            transaction.TranDate = DateTime.Now.ToString();

            _context.SaveChanges();
            return RedirectToAction("Requests"); // Redirect đến trang Requests
        }

        // Reject the book request
        public IActionResult RejectRequest(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest(); // Trả về mã 400 nếu tranId là null
            }

            var transaction = _context.Transactions
                .FirstOrDefault(t => t.TranId == tranId);

            if (transaction == null)
            {
                return NotFound(); // Trả về mã 404 nếu không tìm thấy giao dịch
            }

            transaction.TranStatus = "Rejected";
            transaction.TranDate = DateTime.Now.ToString();

            var book = _context.Books
                .FirstOrDefault(b => b.BookId == transaction.BookId);

            if (book != null)
            {
                book.BookCopies += 1; // Tăng số lượng sách
            }

            _context.SaveChanges();
            return RedirectToAction("Requests"); // Redirect đến trang Requests
        }

        // Returns admin accepted view, here admin can view the accepted books
        public IActionResult Accepted()
        {
            var transactions = _context.Transactions.ToList();
            return View(transactions);
        }

        // Returns all accepted books in JSON format
        public IActionResult GetAllAccepted()
        {
            var transactionList = _context.Transactions
                .Where(r => r.TranStatus == "Accepted")
                .ToList();
            return Json(new { data = transactionList });
        }

        // Returns admin return view, here admin can accept book return requests
        public IActionResult Return()
        {
            var transactions = _context.Transactions.ToList();
            return View(transactions);
        }

        // Returns all return books in JSON format
        public IActionResult GetAllReturn()
        {
            var transactionList = _context.Transactions
                .Where(r => r.TranStatus == "Returned")
                .ToList();
            return Json(new { data = transactionList });
        }

        // Accepts the book return request
        public IActionResult AcceptReturn(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest(); // Trả về mã 400 nếu tranId là null
            }

            var transaction = _context.Transactions
                .FirstOrDefault(t => t.TranId == tranId);

            if (transaction == null)
            {
                return NotFound(); // Trả về mã 404 nếu không tìm thấy giao dịch
            }

            var book = _context.Books
                .FirstOrDefault(b => b.BookId == transaction.BookId);

            if (book != null)
            {
                book.BookCopies += 1; // Tăng số lượng sách
            }

            _context.SaveChanges();
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction("Return"); // Redirect đến trang Return
        }

        // Returns admin home view
        public IActionResult AdminHome()
        {
            return View();
        }

        // Returns admin about view
        public IActionResult AdminAbout()
        {
            return View();
        }

        // Returns admin contact view
        public IActionResult AdminContact()
        {
            return View();
        }
    }
}
