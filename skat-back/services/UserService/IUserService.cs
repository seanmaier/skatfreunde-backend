using skat_back.DTO.UserDTO;

namespace skat_back.services.UserService;

public interface IUserService : IService<ResponseUserDto, CreateUserDto, UpdateUserDto, Guid>
{
}