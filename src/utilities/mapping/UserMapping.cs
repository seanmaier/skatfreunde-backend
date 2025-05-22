using skat_back.features.auth.models;

namespace skat_back.utilities.mapping;

public static class UserMapping
{
    public static ApplicationUser ToEntity(this RegisterDto userDto)
    {
        return new ApplicationUser
        {
            UserName = userDto.Username,
            Email = userDto.Email
        };
    }
}