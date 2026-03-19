using Microsoft.AspNetCore.Mvc;
using KucniSavetBackend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using KucniSavetBackend.Domain;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult> GetById(string id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();
        
        return Ok(UserMapper.ToResponse(user));
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(User user)
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

    [HttpPost("add-to-household")]
    [Authorize]
    public async Task<IActionResult> AddUserToHousehold([FromQuery] string name)
    {
        var householdId = User.FindFirst("householdId")?.Value;

        if (householdId is null)
            return BadRequest();

        var user = await _userService.CreateWithInviteCodeAsync(name, householdId);

        return Ok(UserMapper.ToResponse(user));
    }
}
