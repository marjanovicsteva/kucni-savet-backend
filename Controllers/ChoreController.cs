using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChoreController : ControllerBase
{
    public readonly IChoreService _choreService;

    public ChoreController(IChoreService choreService)
    {
        _choreService = choreService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var chore = await _choreService.GetByIdAsync(id);

        if (chore is null)
            return NotFound();

        return Ok(chore);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateChoreRequest request)
    {
        try
        {
            var created = await _choreService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPost("{choreId}/assign/{assigneeId}")]
    public async Task<ActionResult> Assign([FromRoute] string choreId, [FromRoute] string assigneeId)
    {
        var chore = await _choreService.AddAssigneeToChore(choreId, assigneeId);
        return Ok(chore);
    }
}