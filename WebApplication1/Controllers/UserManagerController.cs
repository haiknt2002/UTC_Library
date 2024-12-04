using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace UTC_Library.Controllers
{
    public class UserManagerController : Controller
    {
        static int _userId;          // Used to store user id.
        static string? _userName;     // Used to store user name.

        private readonly ApplicationDbContext _context;
        public UserManagerController(ApplicationDbContext context)
        {
            _context = context;
        }



        // Returns user books borrow view, here user can request for a book.
        public ActionResult Index(int? userId,string username)
        {
            Console.WriteLine("-------------------Da dang nhap vao--------------------------");
            if (userId == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            var user = _context.Users
               .FirstOrDefault(a => a.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            _userId = (int)userId;
            _userName = username;
            return View();
        }

        // Returns user home view.
        public ActionResult UserHome()
        {
            return View();
        }

        // Returns user about view.
        public ActionResult UserAbout()
        {
            return View();
        }

        // Returns user contact view.
        public ActionResult UserContact()
        {
            return View();
        }

        // Navbar menus.
        // Redirected to index view of borrow controller with user id and username.
        public ActionResult MenuBorrow()
        {
            return RedirectToAction("Index", "UserManager", new { userId = _userId, userName = _userName });
        }

        // Redirected to Requested view of user transaction controller with user id.
        public ActionResult MenuRequested()
        {
            return RedirectToAction("Requested", "UserTransaction", new { userId = _userId });
        }

        // Redirected to Received view of user transaction controller with user id.
        public ActionResult MenuReceived()
        {
            HttpContext.Session.Remove("receivedBadge");
            return RedirectToAction("Received", "UserTransaction", new { userId = _userId });
        }

        // Redirected to Rejected view of user transaction controller with user id.
        public ActionResult MenuRejected()
        {
            HttpContext.Session.Remove("rejectedBadge");
            return RedirectToAction("Rejected", "UserTransaction", new { userId = _userId });
        }

        // Borrow the book, redirect to index view.
        public ActionResult Borrow(int? bookId)
        {
            Console.WriteLine("---------------Muon-----------------");
            if (_context.Transactions.Where(t => t.UserId == _userId).Count() < 6)
            {
                if (bookId != null)
                {
                    Console.WriteLine("---------------co Sach-----------------");
                    var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
                    if (book == null)
                    {
                        return NotFound();
                    }
                    if (book.BookCopies > 0)
                    {
                        book.BookCopies = book.BookCopies - 1;
                        var trans = new WebApplication1.Models.Transaction
                        {
                            BookId = book.BookId,
                            BookTitle = book.BookTitle,
                            BookISBN = book.BookISBN,
                            TranDate = DateTime.Now.ToShortDateString(),
                            TranStatus = "Requested",
                            UserId = _userId,
                            UserName = _userName,
                        };
                        _context.SaveChanges();
                        _context.Transactions.Add(trans);
                        _context.SaveChanges();
                        HttpContext.Session.SetString("requestMsg", "Requested successfully");
                    }
                    else
                    {
                        HttpContext.Session.SetString("requestMsg", "Không thể mượn, số lượng sách bằng 0.");
                    }
                    ViewBag.requestMsg = HttpContext.Session.GetString("requestMsg");
                }
                else
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }
            }
            else
            {
                HttpContext.Session.SetString("requestMsg", "Sorry you cant take more than six books");
            }
            return RedirectToAction("Index", "UserManager", new {   userId = _userId, userName = _userName });
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }

        // Remove the session datas which are used for alerts
        // ReqAlert
        public ActionResult RequestAlert()
        {
            HttpContext.Session.Remove("requestMsg");
            return RedirectToAction("Index", "Borrow", new { userId = _userId, userName = _userName });
        }
        public ActionResult GetAll()
        {
            var books = _context.Books.ToList();
            return Json(new { data = books });
        }
    }
}
