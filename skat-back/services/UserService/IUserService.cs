using skat_back.DTO.UserDTO;

namespace skat_back.services.UserService;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(string id);
    Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
    Task<bool> UpdateUserAsync(string id, UpdateUserDto dto);
    Task<bool> DeleteUserAsync(string id);
}