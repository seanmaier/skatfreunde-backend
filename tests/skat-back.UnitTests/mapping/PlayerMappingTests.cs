using FluentAssertions;
using skat_back.features.players.models;
using skat_back.utilities.mapping;

namespace skat_back.Tests.mapping;

public class PlayerMappingTests
{
    [Fact]
    public void PlayerMapping_CreatePlayerDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createPlayerDto = new CreatePlayerDto
        (
            Guid.NewGuid().ToString(),
            "Test Player"
        );

        // Act
        var player = createPlayerDto.ToEntity();

        // Assert
        player.Name.Should().Be(createPlayerDto.Name);
        player.CreatedById.Should().Be(Guid.Parse(createPlayerDto.CreatedById));
    }

    [Fact]
    public void PlayerMapping_UpdatePlayerDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var updatePlayerDto = new UpdatePlayerDto
        (
            Guid.NewGuid().ToString(),
            "Updated Player"
        );

        // Act
        var player = updatePlayerDto.ToEntity();

        // Assert
        player.Name.Should().Be(updatePlayerDto.Name);
        player.UpdatedById.Should().Be(Guid.Parse(updatePlayerDto.UpdatedById));
    }

    [Fact]
    public void PlayerMapping_Player_ToResponse_ShouldMapCorrectly()
    {
        // Arrange
        var player = new Player
        {
            Id = 1,
            Name = "Test Player",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = Guid.NewGuid(),
            UpdatedById = Guid.NewGuid()
        };

        // Act
        var responsePlayerDto = player.ToResponse();

        // Assert
        responsePlayerDto.Id.Should().Be(player.Id);
        responsePlayerDto.Name.Should().Be(player.Name);
        responsePlayerDto.CreatedAt.Should().BeCloseTo(player.CreatedAt, TimeSpan.FromSeconds(1));
        responsePlayerDto.UpdatedAt.Should().BeCloseTo(player.UpdatedAt.Value, TimeSpan.FromSeconds(1));
        responsePlayerDto.CreatedById.Should().Be(player.CreatedById.ToString());
        responsePlayerDto.UpdatedById.Should().Be(player.UpdatedById.ToString());
    }

    [Fact]
    public void PlayerMapping_NullCreatePlayerDto_ToEntity_ShouldThrowArgumentNullException()
    {
        // Arrange
        CreatePlayerDto? createPlayerDto = null;

        // Act
        Action act = () => createPlayerDto.ToEntity();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void PlayerMapping_NullUpdatePlayerDto_ToEntity_ShouldThrowArgumentNullException()
    {
        // Arrange
        UpdatePlayerDto? updatePlayerDto = null;

        // Act
        Action act = () => updatePlayerDto.ToEntity();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void PlayerMapping_NullPlayer_ToResponse_ShouldThrowArgumentNullException()
    {
        // Arrange
        Player? player = null;

        // Act
        Action act = () => player.ToResponse();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void PlayerMapping_EmptyPlayer_ToResponse_ShouldReturnDefaultResponsePlayerDto()
    {
        // Arrange
        var player = new Player();

        // Act
        var responsePlayerDto = player.ToResponse();

        // Assert
        responsePlayerDto.Id.Should().Be(0);
        responsePlayerDto.Name.Should().BeNullOrEmpty();
        responsePlayerDto.CreatedAt.Should().BeCloseTo(DateTime.MinValue, TimeSpan.FromSeconds(1));
        responsePlayerDto.UpdatedAt.Should().BeNull();
        responsePlayerDto.CreatedById.Should().BeEquivalentTo("00000000-0000-0000-0000-000000000000");
        responsePlayerDto.UpdatedById.Should().BeNullOrEmpty();
    }
}