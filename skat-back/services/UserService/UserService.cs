using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.DTO.UserDTO;
using skat_back.models;
using ILogger = Serilog.ILogger;

namespace skat_back.services.UserService;

public class UserService(IUnitOfWork uow, AppDbContext db, IMapper mapper, ILogger logger) : IUserService
{
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        logger.Information("Getting all users");
        return await db.Users.ProjectTo<UserResponseDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<UserResponseDto?> GetByIdAsync(Guid id)
    {
        logger.Information("Getting user by id: {Id}", id);

        try
        {
            var player = await db.Users.FindAsync(id);
            return player == null ? null : mapper.Map<UserResponseDto>(player);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error getting user by id: {Id}", id);
            throw;
        }
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        logger.Information("Creating user: {@User}", dto);

        try
        {
            var user = mapper.Map<User>(dto);

            db.Users.Add(user);
            await uow.CommitAsync();
            return mapper.Map<UserResponseDto>(user);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error creating user: {@User}", dto);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        logger.Information("Updating user: {@User}", dto);
        try
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
        catch (Exception ex)
        {
            logger.Error(ex, "Error updating user: {@User}", dto);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.Information("Deleting user with id: {Id}", id);
        try
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
                return false;
            db.Users.Remove(user);
            await uow.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error deleting user with id: {Id}", id);
            throw;
        }
    }
}