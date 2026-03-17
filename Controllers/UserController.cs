using Microsoft.AspNetCore.Mvc;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{*id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();
        
        return Ok(user);
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> Create(CreateUserRequest user)
    {
        try
        {
            var created = await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
