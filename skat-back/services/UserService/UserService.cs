using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.DTO.UserDTO;
using skat_back.models;

namespace skat_back.services.UserService;

public class UserService(IUnitOfWork uow, AppDbContext db, IMapper mapper) : IUserService
{
    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        return await db.Users.ProjectTo<UserResponseDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(string id)
    {
        User? player = await db.Users.FindAsync(id);
        return player == null ? null : mapper.Map<UserResponseDto>(player);
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
    {
        User user = mapper.Map<User>(dto);
        
        db.Users.Add(user);
        await uow.CommitAsync();
        return mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var existingUser = await db.Users.FindAsync(id);
        if (existingUser == null)
            return false;

        existingUser.FirstName = dto.FirstName;
        existingUser.LastName = dto.LastName;
        existingUser.Email = dto.Email;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        User? user = await db.Users.FindAsync(id);
        if (user == null)
            return false;
        db.Users.Remove(user);
        await uow.CommitAsync();
        return true;
    }
}