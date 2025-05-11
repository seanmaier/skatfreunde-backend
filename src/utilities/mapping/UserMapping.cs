using skat_back.Features.Users;
using skat_back.models;

namespace skat_back.utilities.mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto userDto)
    {
        return new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Password = userDto.Password
        };
    }

    public static User ToEntity(this UpdateUserDto userDto)
    {
        return new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Password = userDto.Password
        };
    }

    public static ResponseUserDto ToResponse(this User userDto)
    {
        return new ResponseUserDto(
            userDto.Id,
            userDto.FirstName,
            userDto.LastName,
            userDto.Email,
            userDto.Password
        );
    }
}