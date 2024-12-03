using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace UTC_Library.Controllers
{
    public class UserTransactionController : Controller
    {
        static int userId; // Used to store user id
        private readonly ApplicationDbContext _context;

        public UserTransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Returns user requested view, here user can cancel request.
        public IActionResult Requested(int? userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            UserTransactionController.userId = userId.Value;
            var requestList = _context.Transactions.Where(s => s.TranStatus == "Requested" && s.UserId == userId).ToList();

            if (!requestList.Any())
            {
                HttpContext.Session.SetString("requestMessage", "Your Requested list is empty, Go to Borrow section for request a book.");
            }
            else
            {
                HttpContext.Session.Remove("requestMessage");
            }
            // Truyền giá trị Session qua ViewBag
            ViewBag.ReceiveMessage = HttpContext.Session.GetString("requestMessage");
            return View(requestList);
        }

        // Cancel book request, redirected to requested
        public IActionResult DeleteRequest(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest();
            }

            var transaction = _context.Transactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return NotFound();
            }

            var book = _context.Books.FirstOrDefault(b => b.BookId == transaction.BookId);
            book.BookCopies += 1;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return RedirectToAction("Requested", new { userId = userId });
        }

        // Returns user rejected view, here user can rerequest and cancel book request.
        public IActionResult Rejected(int? userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            UserTransactionController.userId = userId.Value;
            var rejectedList = _context.Transactions.Where(s => s.TranStatus == "Rejected" && s.UserId == userId).ToList();

            if (!rejectedList.Any())
            {
                HttpContext.Session.SetString("rejectMessage", "Your Rejected list is empty, Wait for the admin to take action.");
            }
            else
            {
                HttpContext.Session.Remove("rejectMessage");
            }
            // Truyền giá trị Session qua ViewBag
            ViewBag.ReceiveMessage = HttpContext.Session.GetString("rejectMessage");
            return View(rejectedList);
        }

        // Rerequest book request, redirected to rejected
        public IActionResult RerequestRejected(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest();
            }

            var transaction = _context.Transactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return NotFound();
            }

            transaction.TranStatus = "Requested";
            transaction.TranDate = DateTime.Now.ToShortDateString();

            var book = _context.Books.FirstOrDefault(b => b.BookId == transaction.BookId);
            book.BookCopies -= 1;

            _context.SaveChanges();

            return RedirectToAction("Rejected", new { userId = userId });
        }

        // Cancel book request, redirected to rejected
        public IActionResult CancelRejected(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest();
            }

            var transaction = _context.Transactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return NotFound();
            }

            var book = _context.Books.FirstOrDefault(b => b.BookId == transaction.BookId);
            book.BookCopies += 1;

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return RedirectToAction("Rejected", new { userId = userId });
        }

        // Returns user received view, here user can read and return the book, redirected to received
        public IActionResult Received(int? userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            UserTransactionController.userId = userId.Value;
            var receivedList = _context.Transactions.Where(s => s.TranStatus == "Accepted" && s.UserId == userId).ToList();

            if (!receivedList.Any())
            {
                HttpContext.Session.SetString("receiveMessage", "Your Received list is empty, Wait for the admin to take action.");
            }
            else
            {
                HttpContext.Session.Remove("receiveMessage");
            }
            // Truyền giá trị Session qua ViewBag
            ViewBag.ReceiveMessage = HttpContext.Session.GetString("receiveMessage");
            return View(receivedList);
        }

        // Return book
        public IActionResult ReturnReceived(int? tranId)
        {
            if (tranId == null)
            {
                return BadRequest();
            }

            var transaction = _context.Transactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return NotFound();
            }

            transaction.TranDate = DateTime.Now.ToShortDateString();
            transaction.TranStatus = "Returned";

            _context.SaveChanges();

            return RedirectToAction("Received", new { userId = userId });
        }
    }
}
