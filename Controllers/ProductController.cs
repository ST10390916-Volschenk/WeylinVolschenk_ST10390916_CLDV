using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;
using System.IO.Compression;

namespace ST10390916_CLDV_POE.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult AddProduct()
        {
            return View();
        }

        public Product productTbl = new Product();

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var result = productTbl.InsertProduct(product);
            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;
            return RedirectToAction("Shop", "Product");
        }

        [HttpGet]
        public ActionResult Shop()
        {
            List<Product> products = Product.GetAllProducts();
            ViewData["products"] = products;
            return View(products);
        }

    }
}
