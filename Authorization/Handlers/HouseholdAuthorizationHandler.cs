using KucniSavetBackend.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace KucniSavetBackend.Authorization.Handlers;

public class HouseholdAuthorizationHandler : AuthorizationHandler<HouseholdRequirement, string>
{

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HouseholdRequirement requirement, string householdId)
    {
        var userHouseholdId = context.User.FindFirst("householdId")?.Value;

        if (userHouseholdId is null) return;

        if (householdId == userHouseholdId)
            context.Succeed(requirement);
    }
}