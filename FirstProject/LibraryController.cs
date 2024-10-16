using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class LibraryController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LibraryController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Home()
    {
        return Ok("Hello");
    }

    [HttpGet("Books")]
    public IActionResult Books(int a, int b)
    {
        var books = _configuration.GetSection("Books").Get<List<Book>>();

        if (books.Count == 0)
        {
            return BadRequest("No books found");
        }

        return Ok(String.Join('\n', books));
    }


    [HttpGet("Profile/{id:int?}")]
    public IActionResult Profile(int? id)
    {
        User user;
        if (id == null)
        {
            user = _configuration.GetSection("CurrentUser").Get<User>();
            if (user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user.ToString());
        }

        if (id < 0 || id > 5) return BadRequest("Out of range");

        user = _configuration
            .GetSection("Users")
            .Get<List<User>>()
            .FirstOrDefault(x => x.Id == id);

        if (user == null) return BadRequest("User not found");

        return Ok(user.ToString());
    }
}
