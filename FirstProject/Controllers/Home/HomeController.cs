using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
