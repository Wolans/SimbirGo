using Microsoft.AspNetCore.Mvc;

using SimbirGo.Contracts.Commands.SimbirGoCommands;
using SimbirGo.Contracts.Interfaces;
using SimbirGo.Contracts.DTO;

namespace SimbirGo.App.Controllers;

[ApiController]
[Route("api/users")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand user)
    {
        string userID = Ulid.NewUlid().ToString();

        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _userService.CreateUser(userID, user, cancellationToken);

            return CreatedAtAction(nameof(GetUserById), new { userID }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userID}")]
    public async Task<ActionResult<UserDTO>> GetUserById(string userID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            var user = await _userService.GetUserById(userID, cancellationToken);

            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserDTO>>> GetAllUsers()
    {
        var cancellationToken = HttpContext?.RequestAborted ?? default;

        var users = await _userService.GetAllUsers(cancellationToken);

        return Ok(users);
    }

    [HttpPut("{userID}")]
    public async Task<IActionResult> UpdateUser(string userID, [FromBody] UserUpdateCommand user)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _userService.UpdateUser(userID, user, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{userID}")]
    public async Task<IActionResult> DeleteUser(string userID)
    {
        try
        {
            var cancellationToken = HttpContext?.RequestAborted ?? default;

            await _userService.DeleteUser(userID, cancellationToken);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}