using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { ID = 1, Name = "Laptop", Price = 1200 },
                new Product { ID = 2, Name = "Smartphone", Price = 800 },
                new Product { ID = 3, Name = "Headphones", Price = 150 }
            };

            return View(products);
        }
    }
}
