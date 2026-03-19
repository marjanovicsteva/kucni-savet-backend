using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    [Authorize]
    public async Task<ActionResult> GetById(string id)
    {
        var chore = await _choreService.GetByIdAsync(id);

        if (chore is null)
            return NotFound();

        return Ok(ChoreMapper.ToResponse(chore));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateChoreRequest request)
    {
        var householdId = User.FindFirst("householdId")?.Value;

        if (householdId is null)
            return BadRequest();

        try
        {
            var created = await _choreService.CreateAsync(request.Name, request.Frequency, householdId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPost("{choreId}/assign/{assigneeId}")]
    [Authorize]
    public async Task<ActionResult> Assign([FromRoute] string choreId, [FromRoute] string assigneeId)
    {
        var chore = await _choreService.AddAssigneeToChore(choreId, assigneeId);

        if (chore is null)
            return BadRequest();

        return Ok(ChoreMapper.ToResponse(chore));
    }
}