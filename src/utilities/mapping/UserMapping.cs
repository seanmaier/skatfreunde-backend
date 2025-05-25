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

    public static ApplicationUser ToEntity(this CreateUserDto dto)
    {
        return new ApplicationUser
        {
            UserName = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password,
        };
    }

    public static ApplicationUser ToEntity(this UpdateUserDto dto)
    {
        return new ApplicationUser
        {
            Id = dto.Id,
            UserName = dto.Username,
            Email = dto.Email
        };
    }

    public static UserResponseDto ToResponse(this ApplicationUser user, List<string> roles)
    {
        return new UserResponseDto(
            user.Id,
            user.UserName!,
            user.Email!,
            roles.ToList()
        );
    }
}