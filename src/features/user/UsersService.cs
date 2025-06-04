using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using skat_back.features.auth.models;
using skat_back.features.user.models;
using skat_back.utilities.mapping;

namespace skat_back.features.user;

public class UsersService(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    ILogger<UsersService> logger)
    : IUsersService
{
    public async Task<ICollection<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await userManager.Users.ToListAsync();
        if (users.Count != 0)
            return users.Select(u =>
            {
                var roles = userManager.GetRolesAsync(u).Result;
                return u.ToResponse(roles.ToList());
            }).ToList();
        logger.LogInformation("No users found in the database.");
        return new List<UserResponseDto>();
    }


    public async Task<UserResponseDto?> GetUserByIdAsync(string userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == Guid.Parse(userId));
        if (user == null)
        {
            logger.LogWarning("User with ID {UserId} not found", userId);
            return null;
        }

        var roles = userManager.GetRolesAsync(user).Result;
        return user.ToResponse(roles.ToList());
    }


    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
    {
        var user = dto.ToEntity();


        var result = userManager.CreateAsync(user, dto.Password).Result;
        if (!result.Succeeded)
            throw new ApplicationException(
                $"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        if (!await roleManager.RoleExistsAsync(dto.Role))
            throw new ArgumentException("Role does not exist");

        await userManager.AddToRoleAsync(user, dto.Role);

        logger.LogInformation("User {Username} created with role {Role}", user.UserName, dto.Role);
        var roles = await userManager.GetRolesAsync(user);
        return user.ToResponse(roles.ToList());
    }


    public async Task<bool> UpdateUserAsync(string userId, UpdateUserDto dto)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return false;

        user.UserName = dto.Username;
        user.Email = dto.Email;

        var currentRoles = await userManager.GetRolesAsync(user);
        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            logger.LogError("Failed to remove roles for user {UserId}: {Errors}", userId,
                string.Join(", ", removeResult.Errors.Select(e => e.Description)));
            throw new ApplicationException("Failed to remove roles");
        }

        var addResult = await userManager.AddToRoleAsync(user, dto.Role);
        if (!addResult.Succeeded)
        {
            logger.LogError("Failed to assign role {Role} to user {UserId}: {Errors}", dto.Role, userId,
                string.Join(", ", addResult.Errors.Select(e => e.Description)));
            throw new ApplicationException("Failed to assign role");
        }

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new ApplicationException(
                $"Failed to update user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");

        return true;
    }


    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            logger.LogWarning("User with ID {UserId} not found for deletion", userId);
            return false;
        }

        await userManager.DeleteAsync(user);
        return true;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        var user = await userManager.FindByEmailAsync(email);

        return user != null;
    }
}