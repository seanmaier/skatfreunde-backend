using skat_back.features.user.models;

namespace skat_back.features.user;

public interface IUsersService
{
    Task<ICollection<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(string userId);
    Task<UserResponseDto> CreateUserAsync(CreateUserDto username);
    Task<bool> UpdateUserAsync(string userId, UpdateUserDto dto);
    Task<bool> DeleteUserAsync(string userId);
}