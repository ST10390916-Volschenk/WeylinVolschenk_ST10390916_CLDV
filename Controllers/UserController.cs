using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace ST10390916_CLDV_POE.Controllers
{
    public class UserController : Controller
    {

        public User usertbl = new User();

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            usertbl.insert_User(user);
            int userID = usertbl.SelectUser(user.Email, user.Password);
            HttpContext.Session.SetInt32("UserID", userID);
            ViewData["UserID"] = userID;
            return RedirectToAction("MyWork", "User");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usertbl);
        }

        //----------------------------------------------Login----------------------------------------------------------

        private readonly User login;

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            login = new User();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = new User();
            int userID = user.SelectUser(email, password);
            if (userID != -1)
            {
                HttpContext.Session.SetInt32("UserID", userID);
                return RedirectToAction("MyWork", "User");
            }
            else
            {
                ViewData["LoginFailed"] = "Email or password is incorrect.";
                return View();
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        [HttpGet]
        public ActionResult MyWork()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            List<Product> products = Product.GetUserProducts(userID);
            ViewData["products"] = products;
            return View(products);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
