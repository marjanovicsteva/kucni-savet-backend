using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Mappers;

public static class HouseholdMapper
{
    public static HouseholdResponse ToResponse(Household household) => new HouseholdResponse
    {
        Id = household.Id,
        Name = household.Name,
    };

    public static Household ToDomain(HouseholdResponse household) => new Household
    {
        Id = household.Id,
        Name = household.Name,
    };
}