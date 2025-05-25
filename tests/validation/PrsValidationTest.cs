using FluentAssertions;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.validation.validators.playerRoundStats;

namespace skat_back.Tests.validation;

public class PrsValidationTest
{
    private readonly CreatePrsValidator _createPrsValidator = new();
    private readonly UpdatePrsValidator _updatePrsValidator = new();
    
    /*-------------------------Create PlayerRoundStats Validation---------------------------*/
    
    [Fact]
    public void Validate_CreatePrs_ValidPrs_ReturnsSuccess()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(1, 20, 10, 6);
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Validate_CreatePrs_InvalidPrs_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(-1, -1, -1, -1);
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Validate_CreatePrs_InvalidPoints_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(1, 20000, 10, 6); // Points exceed limit
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Points must be less than 10.000"));
    }
    
    [Fact]
    public void Validate_CreatePrs_InvalidGameSum_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(1, 20, 35, 20); // Lost + Won = 55
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("The sum of won and lost games must be greater than 0 and less than 50."));
    }
    
    [Fact]
    public void Validate_CreatePrs_EmptyPlayerId_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(0, 20, 10, 6); // PlayerId is empty
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("PlayerId is required."));
    }
    
    [Fact]
    public void Validate_CreatePrs_ZeroPoints_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(1, 0, 10, 6); // Points are zero
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Points are required."));
    }
    
    [Fact]
    public void Validate_CreatePrs_ZeroGames_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(1, 20, 0, 0); // Lost + Won = 0
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("The sum of won and lost games must be greater than 0 and less than 50."));
    }
    
    [Theory]
    [InlineData(1, 20, 10, 6)] // Valid case
    [InlineData(1, 0, 10, 6)] // Zero points
    [InlineData(1, 20, 30, 0)] // Lost + Won = 50
    [InlineData(0, 20, 10, 6)] // Empty PlayerId
    public void Validate_CreatePrs_VariousCases_ReturnsExpectedResults(int playerId, int points, int won, int lost)
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(playerId, points, won, lost);
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        if (playerId <= 0 || points < 1 || points >= 10000 || won + lost <= 0 || won + lost >= 50)
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }
        else
        {
            result.IsValid.Should().BeTrue();
        }
    }
    
    [Fact]
    public void Validate_CreatePrs_EmptyDto_ReturnsFailure()
    {
        // Arrange
        var prs = new CreatePlayerRoundStatsDto(0, 0, 0, 0); // All fields are empty
        
        // Act
        var result = _createPrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
    
    /*-------------------------Update PlayerRoundStats Validation---------------------------*/

    [Fact]
    public void Validate_UpdatePrs_ValidPrs_ReturnsSuccess()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 1, 20, 10, 6);
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void Validate_UpdatePrs_InvalidPrs_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(-1, -1, -1, -1, -1);
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Validate_UpdatePrs_InvalidPoints_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 1, 20000, 10, 6); // Points exceed limit
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Points must be less than 10.000"));
    }
    
    [Fact]
    public void Validate_UpdatePrs_InvalidGameSum_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 1, 20, 40, 20); // Lost + Won = 50
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("The sum of won and lost games must be greater than 0 and less than 50."));
    }
    
    [Fact]
    public void Validate_UpdatePrs_EmptyPlayerId_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 0, 20, 10, 6); // PlayerId is empty
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("PlayerId is required."));
    }
    
    [Fact]
    public void Validate_UpdatePrs_ZeroPoints_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 1, 0, 10, 6); // Points are zero
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Points are required."));
    }
    
    [Fact]
    public void Validate_UpdatePrs_ZeroGames_ReturnsFailure()
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(10, 1, 20, 0, 0); // Lost + Won = 0
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("The sum of won and lost games must be greater than 0 and less than 50."));
    }
    
    [Theory]
    [InlineData(10, 1, 20, 10, 6)] // Valid case
    [InlineData(10, 1, 0, 10, 6)] // Zero points
    [InlineData(10, 1, 20, 30, 0)] // Lost + Won = 50
    [InlineData(10, 0, 20, 10, 6)] // Empty PlayerId
    public void Validate_UpdatePrs_VariousCases_ReturnsExpectedResults(int matchRoundId, int playerId, int points, int won, int lost)
    {
        // Arrange
        var prs = new UpdatePlayerRoundStatsDto(matchRoundId, playerId, points, won, lost);
        
        // Act
        var result = _updatePrsValidator.Validate(prs);
        
        // Assert
        if (playerId <= 0 || points < 1 || points >= 10000 || won + lost <= 0 || won + lost >= 50)
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }
        else
        {
            result.IsValid.Should().BeTrue();
        }
    }
}