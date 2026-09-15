using TodoList_NET.Services.UserServices;
using TodoList_NET.Models.Users;
using TodoList_NET.Dtos;

namespace TodoList_NET.Controllers.UsersController;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers(
        [FromQuery] DtoParam param
    )
    {
        var users = await _userService.GetAllUsersAsync(param);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<DtoUserResponse>> CreateUser(DtoUserCreate user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), createdUser);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DtoUserResponse?>> UpdateUser(int id, DtoUserUpdate user)
    {
        var updatedUser = await _userService.UpdateUserAsync(id, user);
        if (updatedUser == null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
