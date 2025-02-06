using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> users = new List<User>
    {
        new User { Id = 1, UserName = "john_doe", Email = "john@example.com", FullName = "John Doe", Role = "Developer" },
        new User { Id = 2, UserName = "jane_smith", Email = "jane@example.com", FullName = "Jane Smith", Role = "Project Manager" }
    };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        user.Id = users.Max(u => u.Id) + 1;
        users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;
        user.FullName = updatedUser.FullName;
        user.Role = updatedUser.Role;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        users.Remove(user);
        return NoContent();
    }
}
