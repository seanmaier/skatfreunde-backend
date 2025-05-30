using FluentAssertions;
using skat_back.features.matches.matchRounds.models;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.features.matchRounds.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.validation.validators.matchRound;

namespace skat_back.Tests.validation;

public class MatchRoundValidationTest
{
    private readonly CreateMatchRoundValidator _createMatchRoundValidator = new();
    private readonly UpdateMatchRoundValidator _updateMatchRoundValidator = new();

    /*-------------------------Create MatchRound Validation---------------------------*/

    [Fact]
    public void Validate_CreateMatchRound_ValidMatchRound_ReturnsSuccess()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "1", "2", new List<CreatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_CreateMatchRound_InvalidMatchRound_ReturnsFailure()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "", "", new List<CreatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1),
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Table cannot be empty."))
            .And.Contain(e => e.ErrorMessage.Contains("Round number"))
            .And.Contain(e => e.ErrorMessage.Contains("unique player IDs"));
    }

    [Fact]
    public void Validate_CreateMatchRound_InvalidTable_ReturnsFailure()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "1", "122", new List<CreatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Table number must be between 1 and 2 characters long."));
    }

    [Fact]
    public void Validate_CreateMatchRound_InvalidRoundNumber_ReturnsFailure()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "112", "1", new List<CreatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Round number must be exactly 1 character long."));
    }

    [Fact]
    public void Validate_CreateMatchRound_EmptyPlayerRoundStats_ReturnsFailure()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "1", "2", new List<CreatePlayerRoundStatsDto>()
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Player round statistics cannot be empty."));
    }

    [Fact]
    public void Validate_CreateMatchRound_NotUniquePlayerId_ReturnsFailure()
    {
        // Arrange
        var matchRound = new CreateMatchRoundDto(
            "1", "2", new List<CreatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1),
                new(1, 200, 3, 1) // Invalid because playerId is not unique
            }
        );

        // Act
        var result = _createMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("unique player IDs"));
    }

    /*-------------------------Update MatchRound Validation---------------------------*/

    [Fact]
    public void Validate_UpdateMatchRound_ValidMatchRound_ReturnsSuccess()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "1", "2", new List<UpdatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_UpdateMatchRound_InvalidMatchRound_ReturnsFailure()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "", "", new List<UpdatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1),
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Table cannot be empty."))
            .And.Contain(e => e.ErrorMessage.Contains("Round number cannot be empty."))
            .And.Contain(e => e.ErrorMessage.Contains("Player round statistics must have unique player IDs."));
    }

    [Fact]
    public void Validate_UpdateMatchRound_InvalidTable_ReturnsFailure()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "1", "122", new List<UpdatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Table number must be between"));
    }

    [Fact]
    public void Validate_UpdateMatchRound_InvalidRoundNumber_ReturnsFailure()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "112", "1", new List<UpdatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1)
            }
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("Round number must be exactly 1 character long."));
    }

    [Fact]
    public void Validate_UpdateMatchRound_EmptyPlayerRoundStats_ReturnsFailure()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "1", "2", new List<UpdatePlayerRoundStatsDto>()
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Player round statistics cannot be empty."));
    }

    [Fact]
    public void Validate_UpdateMatchRound_NotUniquePlayerId_ReturnsFailure()
    {
        // Arrange
        var matchRound = new UpdateMatchRoundDto(
            "1", "2", new List<UpdatePlayerRoundStatsDto>
            {
                new(1, 100, 2, 1),
                new(1, 200, 3, 1) // Valid because playerId is unique
            }
        );

        // Act
        var result = _updateMatchRoundValidator.Validate(matchRound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage
                .Contains("unique player IDs"));
    }
}