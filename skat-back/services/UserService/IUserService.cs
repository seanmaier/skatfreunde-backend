using skat_back.DTO.UserDTO;

namespace skat_back.services.UserService;

public interface IUserService : IBaseService<UserResponseDto, CreateUserDto, UpdateUserDto, Guid>
{
}