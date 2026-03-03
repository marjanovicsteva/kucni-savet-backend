using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using KucniSavetBackend;
using KucniSavetBackend.DTO;
using KucniSavetBackend.Services;
using KucniSavetBackend.Mappers;
using KucniSavetBackend.Responses;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetUsers([FromQuery] Pagination pagination)
    {
        var response = await _userService.GetAllAsync(pagination);
        return Ok(response);
    }

    // GET: api/User/5
    [HttpGet("{*id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _userService.Get(id);

        if (user is null)
            return NotFound();
        
        return Ok(user);
    }

    // PUT: api/User/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, UserDto user)
    {
        // if (id != user.Id)
        // {
        //     return BadRequest();
        // }

        // _context.Entry(user).State = EntityState.Modified;

        // try
        // {
        //     await _context.SaveChangesAsync();
        // }
        // catch (DbUpdateConcurrencyException)
        // {
        //     if (!UserExists(id))
        //     {
        //         return NotFound();
        //     }
        //     else
        //     {
        //         throw;
        //     }
        // }

        // return NoContent();
        throw new NotImplementedException();
    }

    // POST: api/User
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UserDto>> PostUser(UserDto user)
    {
        await _userService.Create(UserMapper.ToDomain(user));

        return Ok(user);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        // var user = await _context.Users.FindAsync(id);
        // if (user == null)
        // {
        //     return NotFound();
        // }

        // _context.Users.Remove(user);
        // await _context.SaveChangesAsync();

        // return NoContent();
        throw new NotImplementedException();
    }

    private bool UserExists(string id)
    {
        throw new NotImplementedException();
    }
}
