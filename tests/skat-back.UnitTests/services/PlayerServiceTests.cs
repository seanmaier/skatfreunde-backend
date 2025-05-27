using FakeItEasy;
using FluentAssertions;
using skat_back.Features.Players;
using skat_back.features.players.models;
using skat_back.Lib;
using skat_back.utilities.mapping;

namespace skat_back.Tests.services;

public class PlayerServiceTests
{
    [Fact]
    public async Task PlayerService_GetAllAsync_ShouldReturnAllPlayers()
    {
        // Arrange
        var fakePlayers = new List<Player>
        {
            new() { Name = "Player1", CreatedById = Guid.NewGuid() },
            new() { Name = "Player2", CreatedById = Guid.NewGuid() }
        };

        var playerRepo = A.Fake<IPlayerRepository>();
        A.CallTo(() => playerRepo.GetAllAsync()).Returns(Task.FromResult<ICollection<Player>>(fakePlayers));

        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Players).Returns(playerRepo);

        var service = new PlayerService(unitOfWork);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.First().Should().Be(new ResponsePlayerDto(
            fakePlayers[0].Id,
            fakePlayers[0].Name,
            fakePlayers[0].CreatedAt,
            fakePlayers[0].UpdatedAt
        ));

        A.CallTo(() => playerRepo.GetAllAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task PlayerService_GetById_ShouldReturnPlayer()
    {
        // Arrange
        var fakePlayer = new Player
        {
            Name = "Player1", CreatedById = Guid.NewGuid()
        };

        var playerRepo = A.Fake<IPlayerRepository>();
        A.CallTo(() => playerRepo.GetByIdAsync(1)).Returns(Task.FromResult<Player?>(fakePlayer));

        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Players).Returns(playerRepo);

        var service = new PlayerService(unitOfWork);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Player1");
    }

    [Fact]
    public async Task PlayerService_Create_ReturnsResponseDto()
    {
        // Arrange
        var createDto = new CreatePlayerDto(
            Name: "NewPlayer",
            CreatedByUserId: Guid.NewGuid().ToString());

        var expectedPlayer = new Player
        {
            Name = createDto.Name,
            CreatedById = Guid.Parse(createDto.CreatedByUserId),
        };
        
        var playerRepo = A.Fake<IPlayerRepository>();
        A.CallTo(() => playerRepo.CreateAsync(A<Player>._)).Returns(Task.FromResult(expectedPlayer));
        
        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Players).Returns(playerRepo);
        A.CallTo(() => unitOfWork.SaveChangesAsync()).Returns(Task.FromResult(1));
        
        var service = new PlayerService(unitOfWork);
        
        // Act
        var result = await service.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("NewPlayer");
        A.CallTo(() => playerRepo.CreateAsync(A<Player>.That.Matches(p => p.Name == createDto.Name)))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task? PlayerService_Update_ReturnsTrue()
    {
        // Arrange
        var updateDto = new UpdatePlayerDto(
            Name: "UpdatedPlayer",
            CreatedByUserId: Guid.NewGuid().ToString());

        var existingPlayer = new Player
        {
            Id = 1,
            Name = "OldPlayer",
            CreatedById = Guid.Parse(updateDto.CreatedByUserId),
            UpdatedAt = DateTime.UtcNow
        };

        var playerRepo = A.Fake<IPlayerRepository>();
        A.CallTo(() => playerRepo.GetByIdAsync(1)).Returns(Task.FromResult(existingPlayer));
        
        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Players).Returns(playerRepo);
        
        var service = new PlayerService(unitOfWork);
        
        // Act
        var result = await service.UpdateAsync(1, updateDto);

        // Assert
        result.Should().BeTrue();
        existingPlayer.Name.Should().Be("UpdatedPlayer");
        existingPlayer.UpdatedAt.Should().BeAfter(DateTime.UtcNow.AddSeconds(-1));
        
        A.CallTo(() => playerRepo.GetByIdAsync(1)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task PlayerService_Delete_ReturnsTrue()
    {
        // Arrange
        var existingPlayer = new Player
        {
            Id = 1,
            Name = "PlayerToDelete",
            CreatedById = Guid.NewGuid()
        };
        
        var playerRepo = A.Fake<IPlayerRepository>();
        A.CallTo(() => playerRepo.GetByIdAsync(1)).Returns(Task.FromResult(existingPlayer));
        
        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Players).Returns(playerRepo);
        
        var service = new PlayerService(unitOfWork);
        
        // Act
        
        var result = await service.DeleteAsync(1);
        
        // Assert
        
        result.Should().BeTrue();
        A.CallTo(() => playerRepo.GetByIdAsync(1)).MustHaveHappenedOnceExactly();
        A.CallTo(() => playerRepo.Delete(existingPlayer)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();
    }
}
