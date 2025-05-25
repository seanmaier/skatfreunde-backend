using skat_back.data;
using skat_back.features.user.models;
using skat_back.utilities.mapping;

namespace skat_back.features.user;

public class UserService(AppDbContext db) : IUserService
{
    public async Task<UserResponseDto?> GetUser(string userId)
    {
        var user = await db.Users.FindAsync(userId);
        return user?.ToResponse();
    }

    public async Task<bool> UpdateUser(string userId, UpdateUserDto updateUserDto)
    {
        var user = await db.Users.FindAsync(userId);
        if (user == null)
            return false;

        user.UserName = updateUserDto.Username;
        user.Email = updateUserDto.Email;

        var affected = await db.SaveChangesAsync();
        if (affected == 0)
            throw new InvalidOperationException(
                "No rows were updated. This might be due to concurrency issues or the user not existing.");

        return true;
    }

    public async Task<bool> DeleteUser(string userId)
    {
        var user = await db.Users.FindAsync(userId);
        if (user == null)
            return false;

        db.Users.Remove(user);
        var affected = await db.SaveChangesAsync();
        if (affected == 0)
            throw new InvalidOperationException(
                "No rows were deleted. This might be due to concurrency issues or the user not existing.");

        return true;
    }
}