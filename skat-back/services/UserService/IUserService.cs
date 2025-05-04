using skat_back.models;

namespace skat_back.services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(string id);
    Task<User> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(string id, User user);
    Task<bool> DeleteUserAsync(string id);
}