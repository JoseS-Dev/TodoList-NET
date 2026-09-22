using TodoList_NET.Dtos;
using TodoList_NET.Models.Sessions;
using TodoList_NET.Services.SessionServices;

namespace TodoList_NET.Controllers.SessionsController;

[ApiController]
[Route("api/[controller]")]

public class SessionsController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<DtoSessionResponse?>> CreateSession(DtoSession session)
    {
        var createdSession = await _sessionService.CreateSessionAsync(session);
        if (createdSession == null)
        {
            return Unauthorized();
        }
        return Ok(createdSession);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult<string?>> LogoutSession()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var result = await _sessionService.LogoutSessionAsync(token);
        if (result == null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }
}