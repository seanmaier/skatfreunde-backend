using FluentAssertions;
using skat_back.features.auth.models;
using skat_back.utilities.validation.validators.auth;

namespace skat_back.Tests.validation;

public class AuthValidationTest
{
    private readonly LoginValidator _loginValidator = new();
    private readonly RegisterValidator _registerValidator = new();

    /*-------------------------Login Validation---------------------------*/

    [Fact]
    public void Validate_Login_ValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var loginDto = new LoginDto("testuser", "TestPassword123!", false);

        // Act
        var result = _loginValidator.Validate(loginDto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Login_InvalidCredentials_ReturnsFailure()
    {
        // Arrange
        var loginDto = new LoginDto("", "short", false);

        // Act
        var result = _loginValidator.Validate(loginDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Login input is required."))
            .And.Contain(e => e.ErrorMessage.Contains("Password must be at least 8 characters long."));
    }

    [Fact]
    public void Validate_Login_EmptyUsername_ReturnsFailure()
    {
        // Arrange
        var loginDto = new LoginDto("", "TestPassword123!", false);

        // Act
        var result = _loginValidator.Validate(loginDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Login input is required."));
    }

    [Fact]
    public void Validate_Login_EmptyPassword_ReturnsFailure()
    {
        // Arrange
        var loginDto = new LoginDto("testuser", "", false);

        // Act
        var result = _loginValidator.Validate(loginDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password is required."));
    }

    [Fact]
    public void Validate_Login_PasswordTooShort_ReturnsFailure()
    {
        // Arrange
        var loginDto = new LoginDto("testuser", "short", false);

        // Act
        var result = _loginValidator.Validate(loginDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password must be at least 8 characters long."));
    }

    /*-------------------------Register Validation---------------------------*/

    [Fact]
    public void Validate_Register_ValidUsername_ReturnsSuccess()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "test@mail.de", "TestPassword123!");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Register_InvalidUsername_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("", "test@mail.de", "short");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Username is required"));
    }

    [Fact]
    public void Validate_Register_InvalidEmail_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "invalid-email", "TestPassword123!");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Invalid email format."));
    }

    [Fact]
    public void Validate_Register_InvalidPassword_MissingUpperCase_ReturnsFailed()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "test@mail.de", "testpassword123!");

        // Act        
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password must contain at least one uppercase letter."));
    }

    [Fact]
    public void Validate_Register_EmptyEmail_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", string.Empty, "TestPassword123!");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Email is required."));
    }

    [Fact]
    public void Validate_Register_EmptyPassword_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "test@mail.de", "");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password is required."));
    }

    [Fact]
    public void Validate_Register_PasswordMissingSpecialCharacter_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "test@mail.de", "TestPassword123");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password must contain at least one special character."));
    }

    [Fact]
    public void Validate_Register_PasswordMissingNumber_ReturnsFailure()
    {
        // Arrange
        var registerDto = new RegisterDto("testuser", "test@mail.de", "TestPassword!");

        // Act
        var result = _registerValidator.Validate(registerDto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .Contain(e => e.ErrorMessage.Contains("Password must contain at least one digit."));
    }
}