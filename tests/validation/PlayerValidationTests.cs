using FluentAssertions;
using skat_back.Features.Players;
using skat_back.utilities.validation.validators.players;
using static skat_back.utilities.constants.TestingConstants;


namespace skat_back.Tests.validation;

public class PlayerValidationTests
{
    private readonly CreatePlayerValidator _createValidator = new();
    private readonly UpdatePlayerValidator _updateValidator = new();

    /*-------------------------Create Player Validation---------------------------*/
    [Fact]
    public void Validate_CreatePlayer_ValidPlayer_ReturnsSuccess()
    {
        // Arrange
        var player = new CreatePlayerDto(Guid.NewGuid().ToString(), "Test Player");

        // Act
        var result = _createValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_CreatePlayer_InvalidPlayer_ReturnsFailure()
    {
        // Arrange
        var player = new CreatePlayerDto(Guid.Empty.ToString(), string.Empty);

        // Act
        var result = _createValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidTestData))]
    public void Validate_CreatePlayer_InvalidUserId_ReturnsFailure(string invalidGuid)
    {
        // Arrange
        var player = new CreatePlayerDto(invalidGuid, "Test Player");

        // Act
        var result = _createValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("GUID"));
    }

    [Theory]
    [InlineData("a")] // Too short 
    [InlineData(
        "This name is way too long for a player name and should not be valid because it exceeds the maximum length allowed")] // Too long
    [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890")] // Invalid chars
    [InlineData("!@#$%^&*()")] // Invalid chars
    [InlineData("")] // Empty string
    public void Validate_CreatePlayer_InvalidPlayerName_ReturnsFailure(string name)
    {
        // Arrange
        var player = new CreatePlayerDto(Guid.NewGuid().ToString(), name);

        // Act
        var result = _createValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    /*-------------------------Update Player Validation---------------------------*/

    [Fact]
    public void Validate_UpdatePlayer_ValidPlayer_ReturnsSuccess()
    {
        // Arrange
        var player = new UpdatePlayerDto(Guid.NewGuid().ToString(), "Updated Player");

        // Act
        var result = _updateValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_UpdatePlayer_InvalidPlayer_ReturnsFailure()
    {
        // Arrange
        var player = new UpdatePlayerDto(Guid.Empty.ToString(), string.Empty);

        // Act
        var result = _updateValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidTestData))]
    public void Validate_UpdatePlayer_InvalidUserId_ReturnsFailure(string invalidGuid)
    {
        // Arrange
        var player = new UpdatePlayerDto(invalidGuid, "Test Player");

        // Act
        var result = _updateValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("GUID"));
    }

    [Theory]
    [InlineData("a")] // Too short
    [InlineData(
        "This name is way too long for a player name and should not be valid because it exceeds the maximum length allowed")] // Too long
    [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890")] // Invalid chars
    [InlineData("!@#$%^&*()")] // Invalid chars
    [InlineData("")] // Empty string
    public void Validate_UpdatePlayer_InvalidPlayerName_ReturnsFailure(string name)
    {
        // Arrange
        var player = new UpdatePlayerDto(Guid.NewGuid().ToString(), name);

        // Act
        var result = _updateValidator.Validate(player);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    /*-------------------------Setup for Validation---------------------------*/

    private class InvalidGuidTestData : TheoryData<string>
    {
        public InvalidGuidTestData()
        {
            Add(null!); // Null
            Add(""); // Empty
            Add("not-a-guid");
            Add("123e4567-e89b-12d3");
            Add("123e4567-e89b-12d3-a456-xyzxyzxyzx");
            Add(TestUserId);
        }
    }
}