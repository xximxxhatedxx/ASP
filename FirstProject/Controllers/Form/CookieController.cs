using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Controllers
{
    public class CookieController : Controller
    {
        [HttpGet]
        public IActionResult SetCookie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value, DateTime expiryDate)
        {
            if (expiryDate <= DateTime.Now)
            {
                throw new Exception("GenerateError");
            }

            var cookieOptions = new CookieOptions
            {
                Expires = expiryDate
            };

            Response.Cookies.Append("UserValue", value, cookieOptions);
            return RedirectToAction("CheckCookie");
        }

        [HttpGet("generateError")]
        public IActionResult GenerateError()
        {
            throw new Exception("GenerateError");
        }

        public IActionResult CheckCookie()
        {
            var hasCookie = Request.Cookies.TryGetValue("UserValue", out var cookieValue);

            ViewData["HasCookie"] = hasCookie;
            ViewData["CookieValue"] = cookieValue;

            return View();
        }
    }
}