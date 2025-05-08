using skat_back.DTO.UserDTO;
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
    
    public static ResponseUserDto ToDto(this User userDto)
    {
        return new ResponseUserDto(
            Id: userDto.Id,
            FirstName: userDto.FirstName,
            LastName: userDto.LastName,
            Password: userDto.Password,
            Email: userDto.Email
        );
    }
}