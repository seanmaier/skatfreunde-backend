using skat_back.features.user.models;

namespace skat_back.features.user;

public interface IUserService
{
    Task<UserResponseDto?> GetUser(string userId);
    Task<bool> UpdateUser(string userId, UpdateUserDto updateUserDto);
    Task<bool> DeleteUser(string userId);
}