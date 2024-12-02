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
                new Product { ID = 1, Name = "Laptop", Price = 1200, CreatedDate = DateTime.Now },
                new Product { ID = 2, Name = "Smartphone", Price = 800, CreatedDate = DateTime.Now.AddDays(-5) },
                new Product { ID = 3, Name = "Headphones", Price = 150, CreatedDate = DateTime.Now.AddDays(-10) }
            };

            return View(products);
        }
    }
}
