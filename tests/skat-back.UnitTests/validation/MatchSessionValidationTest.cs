using FluentAssertions;
using skat_back.features.matches.matchRounds.models;
using skat_back.features.matches.matchSessions.models;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.features.matchRounds.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.validation.validators.matchSession;

namespace skat_back.Tests.validation;

public class MatchSessionValidationTest
{
    private readonly CreateMatchSessionValidator _createMatchSessionValidator = new();
    private readonly UpdateMatchSessionValidator _updateMatchSessionValidator = new();

    /*-------------------------Create MatchSession Validation---------------------------*/

    [Fact]
    public void Validate_CreateMatchSession_ValidMatchSession_ReturnsSuccess()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(SharedTestData.InvalidGuidTestData))]
    public void Validate_CreateMatchSession_InvalidUserId_ReturnsFailure(string invalidGuid)
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            invalidGuid, "KW23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("GUID"));
    }

    [Fact]
    public void Validate_CreateMatchSession_InvalidCalendarWeek_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "InvalidKW", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("CalendarWeek"));
    }

    [Fact]
    public void Validate_CreateMatchSession_EmptyMatchRounds_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<CreateMatchRoundDto>());

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("MatchRounds cannot be empty."));
    }

    [Fact]
    public void Validate_CreateMatchSession_EmptyPrs_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>())
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Player round statistics cannot be empty."));
    }

    [Fact]
    public void Validate_CreateMatchSession_NotUniqueRoundNumber_ReturnsSuccess()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                ),
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                {
                    new(1, 100, 2, 1)
                })
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_CreateMatchSession_NotUniquePlayerId_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1),
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("unique player IDs"));
    }

    /*-------------------------Update MatchSession Validation---------------------------*/

    [Fact]
    public void Validate_UpdateMatchSession_ValidMatchSession_ReturnsSuccess()
    {
        // Arrange
        var matchSession = new UpdateMatchSessionDto(
            Guid.NewGuid().ToString(), "23", new List<UpdateMatchRoundDto>
            {
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _updateMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(SharedTestData.InvalidGuidTestData))]
    public void Validate_UpdateMatchSession_InvalidUserId_ReturnsFailure(string invalidGuid)
    {
        // Arrange
        var matchSession = new UpdateMatchSessionDto(
            invalidGuid, "KW23", new List<UpdateMatchRoundDto>
            {
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _updateMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("GUID"));
    }

    [Fact]
    public void Validate_UpdateMatchSession_InvalidCalendarWeek_ReturnsFailure()
    {
        // Arrange
        var matchSession = new UpdateMatchSessionDto(
            Guid.NewGuid().ToString(), "InvalidKW", new List<UpdateMatchRoundDto>
            {
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _updateMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("CalendarWeek"));
    }

    [Fact]
    public void Validate_UpdateMatchSession_EmptyMatchRounds_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<CreateMatchRoundDto>());

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("MatchRounds cannot be empty."));
    }

    [Fact]
    public void Validate_UpdateMatchSession_EmptyPrs_ReturnsFailure()
    {
        // Arrange
        var matchSession = new CreateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<CreateMatchRoundDto>
            {
                new("1", "2", new List<CreatePlayerRoundStatsDto>())
            }
        );

        // Act
        var result = _createMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Player round statistics cannot be empty."));
    }

    [Fact]
    public void Validate_UpdateMatchSession_NotUniqueRoundNumber_ReturnsSuccess()
    {
        // Arrange
        var matchSession = new UpdateMatchSessionDto(
            Guid.NewGuid().ToString(), "23", new List<UpdateMatchRoundDto>
            {
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1)
                    }
                ),
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                {
                    new(1, 100, 2, 1)
                })
            }
        );

        // Act
        var result = _updateMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_UpdateMatchSession_NotUniquePlayerId_ReturnsFailure()
    {
        // Arrange
        var matchSession = new UpdateMatchSessionDto(
            Guid.NewGuid().ToString(), "KW23", new List<UpdateMatchRoundDto>
            {
                new("1", "2", new List<UpdatePlayerRoundStatsDto>
                    {
                        new(1, 100, 2, 1),
                        new(1, 100, 2, 1)
                    }
                )
            }
        );

        // Act
        var result = _updateMatchSessionValidator.Validate(matchSession);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("unique player IDs"));
    }
}