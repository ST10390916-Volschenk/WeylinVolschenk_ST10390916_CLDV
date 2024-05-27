using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;
using System.IO.Compression;
using System.Web.Helpers;

namespace ST10390916_CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //------------------------------------------------Add Product-----------------------------

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var result = product.InsertProduct(product);

            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;
            return RedirectToAction("Shop", "Product");
        }
        
        public IActionResult AddProduct()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;
            return View();
        }    

        //----------------------------------------Shop---------------------------------------------

        [HttpGet]
        public ActionResult Shop()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;

            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;

            return View(products);
        }


        [HttpPost]
        public ActionResult Shop(Order order)
        {
            var result = order.InsertOrder(order);

            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;
            return RedirectToAction("Shop", "Product");
        }
    }
}
