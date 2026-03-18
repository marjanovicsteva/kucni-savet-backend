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

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var household = await _householdService.GetByIdAsync(id);

        if (household is null)
            return NotFound();

        return Ok(HouseholdMapper.ToResponse(household));
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateHouseholdRequest request)
    {
        var authResult = await _authorizationService.AuthorizeAsync(
            User,
            HouseholdRepository.Id(request.Id),
            "CanEditHousehold"
        );

        if (!authResult.Succeeded)
            return Forbid();

        var household = await _householdService.UpdateAsync(request.Id, request.Name);

        if (household is null)
            return NotFound();

        return Ok(HouseholdMapper.ToResponse(household));
    }

}
