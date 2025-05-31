using FluentAssertions;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.mapping;

namespace skat_back.Tests.mapping;

public class PrsMappingTests
{
    [Fact]
    public void PrsMapping_CreatePlayerDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createPrs = new CreatePlayerRoundStatsDto(
            1,
            123,
            32,
            12
        );

        // Act
        var player = createPrs.ToEntity();

        // Assert
        player.PlayerId.Should().Be(createPrs.PlayerId);
        player.Points.Should().Be(createPrs.Points);
        player.Won.Should().Be(createPrs.Won);
        player.Lost.Should().Be(createPrs.Lost);
    }

    [Fact]
    public void PrsMapping_UpdatePlayerDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var updatePrs = new UpdatePlayerRoundStatsDto(
            1,
            456,
            24,
            8
        );

        // Act
        var player = updatePrs.ToEntity();

        // Assert
        player.PlayerId.Should().Be(updatePrs.PlayerId);
        player.Points.Should().Be(updatePrs.Points);
        player.Won.Should().Be(updatePrs.Won);
        player.Lost.Should().Be(updatePrs.Lost);
    }
    
    [Fact]
    public void PrsMapping_ToDto_ShouldMapCorrectly()
    {
        // Arrange
        var playerRoundStats = new PlayerRoundStats
        {
            PlayerId = 1,
            Points = 123,
            Won = 32,
            Lost = 12
        };

        // Act
        var responsePlayerRoundStatsDto = playerRoundStats.ToDto();

        // Assert
        responsePlayerRoundStatsDto.PlayerId.Should().Be(playerRoundStats.PlayerId);
        responsePlayerRoundStatsDto.Points.Should().Be(playerRoundStats.Points);
        responsePlayerRoundStatsDto.Won.Should().Be(playerRoundStats.Won);
        responsePlayerRoundStatsDto.Lost.Should().Be(playerRoundStats.Lost);
    }
    
    [Fact]
    public void PrsMapping_ToDto_WithEdgeCaseValues_ShouldMapCorrectly()
    {
        // Arrange
        var playerRoundStats = new PlayerRoundStats
        {
            PlayerId = 0,
            Points = int.MaxValue,
            Won = int.MinValue,
            Lost = 0
        };
    
        // Act
        var responsePlayerRoundStatsDto = playerRoundStats.ToDto();
    
        // Assert
        responsePlayerRoundStatsDto.PlayerId.Should().Be(playerRoundStats.PlayerId);
        responsePlayerRoundStatsDto.Points.Should().Be(playerRoundStats.Points);
        responsePlayerRoundStatsDto.Won.Should().Be(playerRoundStats.Won);
        responsePlayerRoundStatsDto.Lost.Should().Be(playerRoundStats.Lost);
    }
}