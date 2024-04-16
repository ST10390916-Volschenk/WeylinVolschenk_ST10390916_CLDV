using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;

namespace ST10390916_CLDV_POE.Controllers
{
    public class SignUpController : Controller
    {
        public SignUpModel usertbl = new SignUpModel();

        [HttpPost]
        public ActionResult SignUp(SignUpModel user)
        {
            var result = usertbl.insert_User(user);
            return RedirectToAction("MyWork", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usertbl);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
