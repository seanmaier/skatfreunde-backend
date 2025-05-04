using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.models;

namespace skat_back.services.UserService;

public class UserService(IUnitOfWork uow, AppDbContext db) : IUserService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await db.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await db.Users.FindAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        db.Add(user);
        await uow.CommitAsync();
        return user;
    }

    public async Task<bool> UpdateUserAsync(string id, User updatedUser)
    {
        var existingUser = await db.Users.FindAsync(id);
        if (existingUser == null)
            return false;

        existingUser.FirstName = updatedUser.FirstName;
        existingUser.LastName = updatedUser.LastName;
        existingUser.Email = updatedUser.Email;
        existingUser.Password = updatedUser.Password;
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