using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

public class OrderController : Controller
{
    private static List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Маргарита", Price = 10.99m, Description = "Класична піца з томатним соусом і моцарелою" },
        new Product { Id = 2, Name = "Пепероні", Price = 12.99m, Description = "Піца з пепероні, моцарелою і томатним соусом" },
        new Product { Id = 3, Name = "Гавайська", Price = 14.99m, Description = "Піца з ананасами, шинкою і моцарелою" },
        new Product { Id = 4, Name = "Чотири сири", Price = 15.99m, Description = "Піца з різними видами сиру" }
    };

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (user.Age < 16)
        {
            ModelState.AddModelError("", "Вік повинен бути не менше 16.");
            return View(user);
        }

        return RedirectToAction("ChoosePizza", new { name = user.Name, age = user.Age });
    }

    [HttpGet]
    public IActionResult ChoosePizza(string name, int age)
    {
        var user = new User { Name = name, Age = age };

        ViewBag.User = user;
        return View(Products);
    }

    [HttpPost]
    public IActionResult Cart(List<int> quantities, string name, int age)
    {
        var user = new User { Name = name, Age = age };

        var selectedProducts = Products
            .Where((p, index) => quantities[index] > 0)
            .Select((p, index) => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = quantities[index]
            })
            .ToList();

        if (!selectedProducts.Any())
        {
            ModelState.AddModelError("", "Ваша корзина порожня. Виберіть хоча б одну піцу.");
            return RedirectToAction("ChoosePizza", new { name = user.Name, age = user.Age });
        }

        ViewBag.User = user;
        return View("ViewCart", selectedProducts);
    }

    [HttpPost]
    public IActionResult PlaceOrder(string name)
    {
        ViewBag.UserName = name;
        return View("OrderConfirmed");
    }
}
