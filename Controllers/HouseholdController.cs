using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using KucniSavetBackend.Repositories.RavenDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HouseholdController(IHouseholdService householdService, IAuthorizationService authorizationService) : ControllerBase
{
    private readonly IHouseholdService _householdService = householdService;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        var household = await _householdService.GetByIdAsync(id);

        if (household is null)
            return NotFound();

        return Ok(HouseholdMapper.ToResponse(household));
    }

    [HttpPut("{householdId}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] string householdId, [FromBody] UpdateHouseholdRequest request)
    {
        var authResult = await _authorizationService.AuthorizeAsync(
            User,
            HouseholdRepository.Id(householdId),
            "CanEditHousehold"
        );

        if (!authResult.Succeeded)
            return Forbid();

        var household = await _householdService.UpdateAsync(householdId, request.Name);

        if (household is null)
            return NotFound();

        return Ok(HouseholdMapper.ToResponse(household));
    }

}
