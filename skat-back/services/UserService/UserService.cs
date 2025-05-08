using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.DTO.UserDTO;
using skat_back.utilities.mapping;
using ILogger = Serilog.ILogger;

namespace skat_back.services.UserService;

public class UserService(IUnitOfWork uow, AppDbContext db, ILogger logger) : IUserService
{
    public async Task<ICollection<ResponseUserDto>> GetAllAsync()
    {
        logger.Information("Getting all users");
        return await db.Users.Select(r => r.ToResponse()).ToListAsync();
    }

    public async Task<ResponseUserDto?> GetByIdAsync(Guid id)
    {
        logger.Information("Getting user by id: {Id}", id);

        try
        {
            var player = await db.Users.FindAsync(id);
            return player?.ToResponse();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error getting user by id: {Id}", id);
            throw;
        }
    }

    public async Task<ResponseUserDto> CreateAsync(CreateUserDto dto)
    {
        logger.Information("Creating user: {@User}", dto);

        try
        {
            var user = dto.ToEntity();

            db.Users.Add(user);
            await uow.CommitAsync();
            return user.ToResponse();
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

            var user = dto.ToEntity();

            db.Users.Update(user);
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
    
    public async Task<bool> CheckEmailAsync(string email)
    {
        logger.Information("Checking email: {Email}", email);
        try
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error checking email: {Email}", email);
            throw;
        }
    }
}