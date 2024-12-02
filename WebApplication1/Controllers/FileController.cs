using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApplication1.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        [Route("File/DownloadFile")]
        public IActionResult DownloadFile()
        {
            return View();
        }

        [HttpPost]
        [Route("File/DownloadFile")]
        public IActionResult DownloadFile(string firstName, string lastName, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "default.txt";
            }
            else if (!fileName.EndsWith(".txt"))
            {
                fileName += ".txt";
            }

            string fileContent = $"Ім'я: {firstName}\nПрізвище: {lastName}";

            var fileBytes = Encoding.UTF8.GetBytes(fileContent);

            return File(fileBytes, "text/plain", fileName);
        }
    }
}
