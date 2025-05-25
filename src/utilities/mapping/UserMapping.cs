using skat_back.features.auth.models;
using skat_back.features.user.models;

namespace skat_back.utilities.mapping;

public static class UserMapping
{
    public static ApplicationUser ToEntity(this RegisterDto dto)
    {
        return new ApplicationUser
        {
            UserName = dto.Username,
            Email = dto.Email
        };
    }

    public static ApplicationUser ToEntity(this UpdateUserDto dto)
    {
        return new ApplicationUser
        {
            Id = dto.UserId,
            UserName = dto.Username,
            Email = dto.Email
        };
    }

    public static UserResponseDto ToResponse(this ApplicationUser user)
    {
        if (user is { UserName: not null, Email: not null })
            return new UserResponseDto(
                user.UserName,
                user.Email
            );

        throw new ArgumentException("User must have a username and email to be mapped to a response DTO.");
    }
}