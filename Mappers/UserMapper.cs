using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Mappers;

public static class UserMapper
{
    public static UserResponse ToResponse(User user) => new UserResponse
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image,
        Household = user.Household is not null ? HouseholdMapper.ToResponse(user.Household) : null,
    };

    public static User ToDomain(UserResponse user) => new User
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image,
        Household = user.Household is not null ? HouseholdMapper.ToDomain(user.Household) : null,
    };
}