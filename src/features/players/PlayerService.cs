using skat_back.features.players.models;
using skat_back.Lib;
using skat_back.utilities.mapping;

namespace skat_back.Features.Players;

/// <summary>
///     Represents the service implementation for managing players.
/// </summary>
public class PlayerService(IUnitOfWork unitOfWork) : IPlayerService
{
    public async Task<ICollection<ResponsePlayerDto>> GetAllAsync()
    {
        var players = await unitOfWork.Players.GetAllAsync();
        return players.Select(p => p.ToResponse()).ToList();
    }

    public async Task<ResponsePlayerDto?> GetByIdAsync(int id)
    {
        var user = await unitOfWork.Players.GetByIdAsync(id);
        return user?.ToResponse();
    }
    
    public async Task<ResponsePlayerDto?> GetByNameAsync(string name)
    {
        var player = await unitOfWork.Players.GetByNameAsync(name);
        return player?.ToResponse();
    }

    public async Task<ResponsePlayerDto> CreateAsync(CreatePlayerDto dto)
    {
        var newPlayer = dto.ToEntity();
        var user = await unitOfWork.Players.CreateAsync(newPlayer);
        await unitOfWork.SaveChangesAsync();
        return user.ToResponse();
    }

    public async Task<bool> UpdateAsync(int id, UpdatePlayerDto dto)
    {
        var existing = await unitOfWork.Players.GetByIdAsync(id);
        if (existing == null)
            return false;

        var updated = dto.ToEntity();
        existing.UpdateFrom(updated);

        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var player = await unitOfWork.Players.GetByIdAsync(id);

        if (player == null)
            return false;

        unitOfWork.Players.Delete(player);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}