using skat_back.services;

namespace skat_back.Features.Users;

/// <summary>
///     Represents the service interface for managing users.
///     Specific implementations for the User Service should be provided here.
/// </summary>
public interface IUserService : IService<ResponseUserDto, CreateUserDto, UpdateUserDto, Guid>
{
    Task<bool> CheckEmailAsync(string email);
}