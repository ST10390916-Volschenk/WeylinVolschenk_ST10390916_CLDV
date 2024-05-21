using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;
using System.IO.Compression;

namespace ST10390916_CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        public Product productTbl = new Product();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //------------------------------------------------Add Product-----------------------------

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var result = productTbl.InsertProduct(product);
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");

            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;
            return RedirectToAction("Shop", "Product");
        }
        
        public IActionResult AddProduct()
        {
            return View();
        }    

        //----------------------------------------Shop---------------------------------------------

        [HttpGet]
        public IActionResult Shop()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;

            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;

            return View(products);
        }

    }
}
