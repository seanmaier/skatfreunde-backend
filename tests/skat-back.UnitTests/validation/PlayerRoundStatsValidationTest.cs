using FluentAssertions;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.validation.validators.playerRoundStats;

namespace skat_back.Tests.validation;

public class PlayerRoundStatsValidationTest
{
    private readonly CreatePrsValidator _createPrsValidator = new();
    private readonly UpdatePrsValidator _updatePrsValidator = new();

    /*-------------------------Create PlayerRoundStatistics Validation---------------------------*/

    [Fact]
    public void Validate_CreatePlayerRoundStats_ValidPlayerRoundStats_ReturnsSuccess()
    {
        // Arrange
        var playerRoundStats = new CreatePlayerRoundStatsDto(1, 100, 2, 1);

        // Act
        var result = _createPrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_CreatePlayerRoundStats_InvalidPlayerId_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new CreatePlayerRoundStatsDto(0, 100, 51, 1);

        // Act
        var result = _createPrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
        /*result.IsValid.*/ // TODO 
    }

    [Fact]
    public void Validate_CreatePlayerRoundStats_InvalidPoints_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new CreatePlayerRoundStatsDto(1, -100, 2, 1);

        // Act
        var result = _createPrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_CreatePlayerRoundStats_InvalidLostAndWon_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new CreatePlayerRoundStatsDto(1, 100, 51, 1);

        // Act
        var result = _createPrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    /*-------------------------Update PlayerRoundStatistics Validation---------------------------*/

    [Fact]
    public void Validate_UpdatePlayerRoundStats_ValidPlayerRoundStats_ReturnsSuccess()
    {
        // Arrange
        var playerRoundStats = new UpdatePlayerRoundStatsDto(1, 100, 2, 1);

        // Act
        var result = _updatePrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_UpdatePlayerRoundStats_ValidPlayerRoundStats_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new UpdatePlayerRoundStatsDto(-1, 20_000, 50, 1);

        // Act
        var result = _updatePrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UpdatePlayerRoundStats_InvalidPlayerId_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new UpdatePlayerRoundStatsDto(0, 100, 2, 1);

        // Act
        var result = _updatePrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UpdatePlayerRoundStats_InvalidPoints_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new UpdatePlayerRoundStatsDto(1, -100, 2, 1);

        // Act
        var result = _updatePrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UpdatePlayerRoundStats_InvalidLostAndWon_ReturnsFailure()
    {
        // Arrange
        var playerRoundStats = new UpdatePlayerRoundStatsDto(1, 100, 51, 1);

        // Act
        var result = _updatePrsValidator.Validate(playerRoundStats);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}