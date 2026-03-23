using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChoreController(IChoreService choreService) : ControllerBase
{
    public readonly IChoreService _choreService = choreService;

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        var chore = await _choreService.GetByIdAsync(id);

        if (chore is null)
            return NotFound();

        return Ok(ChoreMapper.ToResponse(chore));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateChoreRequest request)
    {
        var householdId = User.FindFirst("householdId")?.Value;

        if (householdId is null)
            return BadRequest();

        try
        {
            var created = await _choreService.CreateAsync(request.Name, request.Frequency, householdId);

            if (created is null)
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPut("{choreId}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] string choreId, [FromBody] UpdateChoreRequest request)
    {
        var chore = await _choreService.UpdateAsync(choreId, request.Name, request.Frequency);

        if (chore is null)
            return BadRequest();

        return Ok(ChoreMapper.ToResponse(chore));
    }

    [HttpPatch("{choreId}/assign/{assigneeId}")]
    [Authorize]
    public async Task<IActionResult> Assign([FromRoute] string choreId, [FromRoute] string assigneeId)
    {
        var chore = await _choreService.AddAssignee(choreId, assigneeId);

        if (chore is null)
            return BadRequest();

        return Ok(ChoreMapper.ToResponse(chore));
    }

    [HttpPatch("{choreId}/unassign/{assigneeId}")]
    [Authorize]
    public async Task<IActionResult> Unassign([FromRoute] string choreId, [FromRoute] string assigneeId)
    {
        var chore = await _choreService.RemoveAssigne(choreId, assigneeId);

        if (chore is null)
            return BadRequest();

        return Ok(ChoreMapper.ToResponse(chore));
    }

    [HttpPatch("{choreId}/mark-done")]
    [Authorize]
    public async Task<IActionResult> MarkDone([FromRoute] string choreId)
    {
        var chore = await _choreService.MarkAsDone(choreId);

        if (chore is null)
            return BadRequest();

        return Ok(ChoreMapper.ToResponse(chore));
    }
}