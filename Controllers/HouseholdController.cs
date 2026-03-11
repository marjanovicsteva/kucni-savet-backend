using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace;

[Route("api/[controller]")]
[ApiController]
public class HouseholdController : ControllerBase
{
    public readonly IHouseholdService _householdService;

    public HouseholdController(IHouseholdService householdService)
    {
        _householdService = householdService;
    }

    [HttpGet("{*id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var household = await _householdService.GetByIdAsync(id);

        if (household is null)
            return NotFound();

        return Ok(household);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateHouseholdRequest request)
    {
        try
        {
            var created = await _householdService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
