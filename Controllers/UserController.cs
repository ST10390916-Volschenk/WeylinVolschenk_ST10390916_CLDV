using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;

namespace ST10390916_CLDV_POE.Controllers
{
    public class UserController : Controller
    {
        private readonly User login;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            login = new User();
            _httpContextAccessor = httpContextAccessor;
        }

        //-------------------------------------Sign Up--------------------------------------------------------------

        //public User usertbl = new User();

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            user.insert_User(user);
            int userID = user.SelectUser(user.Email, user.Password);

            HttpContext.Session.SetInt32("UserID", userID);
            return RedirectToAction("MyWork", "User");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        //----------------------------------------------Login----------------------------------------------------------

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

        public IActionResult Login()
        {
            return View();
        }

        //----------------------------------------------My Work-------------------------------------------------------------

        [HttpGet]
        public ActionResult MyWork()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;

            List<Product> products = Product.GetUserProducts(userID);
            ViewData["products"] = products;
            return View();
        }


        //-----------------------------------------My order history--------------------------------------------------------------

        
        //-----------------------------------------------Sales--------------------------------------------------------------------

    }
}
