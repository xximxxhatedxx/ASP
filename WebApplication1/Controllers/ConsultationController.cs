using Microsoft.AspNetCore.Mvc;

public class ConsultationController : Controller
{
    public IActionResult Register()
    {
        ViewBag.Products = new[] { "JavaScript", "C#", "Java", "Python", "Основи" };
        return View();
    }

    [HttpPost]
    public IActionResult Register(ConsultationFormModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Product == "Основи" && model.ConsultationDate.DayOfWeek == DayOfWeek.Monday)
            {
                ModelState.AddModelError("ConsultationDate", "Консультація щодо 'Основи' не може проходити по понеділках.");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
        }

        ViewBag.Products = new[] { "JavaScript", "C#", "Java", "Python", "Основи" };
        return View(model);
    }

    public IActionResult Success()
    {
        return View();
    }
}
