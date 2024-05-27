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
            int userID = -1;
            HttpContext.Session.SetInt32("UserID", userID);
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

        [HttpGet]
        public ActionResult Orders()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            List<Order> orders = Order.GetUserOrders((int) userID);
            List<Product> products = new List<Product>();
            List<User> users = new List<User>();

            foreach (var order in orders)
            {
                products.Add(Order.GetProduct(order.ProductID));
            }

            foreach (var product in products)
            {
                users.Add(Order.GetUserInfo(product.OwnerID));
            }

            ViewData["sellers"] = users;
            ViewData["orders"] = orders;
            ViewData["products"] = products;

            return View();
        }

        //-----------------------------------------------Sales--------------------------------------------------------------------

        [HttpGet]
        public ActionResult Sales()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            List<Order> orders = Order.GetUserSales((int)userID);
            List<Product> products = new List<Product>();
            List<User> users = new List<User>();

            foreach (var order in orders)
            {
                products.Add(Order.GetProduct(order.ProductID));
                users.Add(Order.GetUserInfo(order.ClientID));
            }

            ViewData["buyers"] = users;
            ViewData["orders"] = orders;
            ViewData["products"] = products;

            return View();
        }

        [HttpPost]
        public ActionResult Sales(int OrderID)
        {
            Order order = new Order();
            var result = order.ProcessOrder(OrderID);
            return RedirectToAction("Sales", "User");
        }

    }
}
