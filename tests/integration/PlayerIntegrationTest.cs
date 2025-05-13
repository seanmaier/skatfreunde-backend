using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using skat_back.Features.Players;
using skat_back.Features.Users;

namespace skat_back.Tests.integration;

public class PlayerIntegrationTest(WebApplicationFactory<CreatePlayerDto> factory)
    : IClassFixture<WebApplicationFactory<CreatePlayerDto>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private static readonly User User = new User
    {
        Id = Guid.NewGuid(),
        FirstName = "Test",
        LastName = "User",
        Password = "TestPassword",
        Email = "test-user@gmail.de"
    };

    private readonly string _userId = User.Id.ToString();
    
    /*--------------------CREATE--------------------*/
    
    [Fact]
    public async Task PlayerController_Create_ReturnsCreated()
    {
        // Arrange
        var newPlayer = new CreatePlayerDto("71A23ED9-766C-4DF6-8145-08BE2888836C", "Test Player");
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/Players", newPlayer);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task PlayerController_Create_ReturnsBadRequest()
    {
        // Arrange
        var createPlayerDto = new CreatePlayerDto(Guid.Empty.ToString(), string.Empty);
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Players")
        {
            Content = new StringContent(JsonSerializer.Serialize(createPlayerDto), Encoding.UTF8, "application/json")
        };
        
        // Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Should().Contain("Name is required");
        content.Should().Contain("UserId must not be an empty GUID");
    }
    
    /*--------------------UPDATE--------------------*/
    
    [Fact]
    public async Task PlayerController_Update_ReturnsOk()
    {
        // Arrange
        var updatePlayerDto = new UpdatePlayerDto(_userId.ToString(), "Updated Player");
        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/players/")
        {
            Content = new StringContent(JsonSerializer.Serialize(updatePlayerDto), Encoding.UTF8, "application/json")
        };
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task PlayerController_Update_ReturnsBadRequest()
    {
        // Arrange
        var updatePlayerDto = new UpdatePlayerDto(Guid.Empty.ToString(), string.Empty);
        var request = new HttpRequestMessage(HttpMethod.Put, "/api/players/1")
        {
            Content = new StringContent(JsonSerializer.Serialize(updatePlayerDto), Encoding.UTF8, "application/json")
        };
        
        // Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Should().Contain("Name is required");
        content.Should().Contain("UserId must not be an empty GUID");
    }
    
    /*--------------------GET ALL--------------------*/
    
    [Fact]
    public async Task PlayerController_GetAll_ReturnsOk()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/players");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    
    [Fact]
    public async Task PlayerController_GetAll_ReturnsNotFound()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/players/999");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    /*--------------------GET BY ID--------------------*/
    
    [Fact]
    public async Task PlayerController_GetById_ReturnsOk()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/players/1");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task PlayerController_GetById_ReturnsNotFound()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/players/999");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task PlayerController_GetById_ReturnsBadRequest()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/players/invalid-id");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    /*--------------------DELETE--------------------*/
    
    [Fact]
    public async Task PlayerController_Delete_ReturnsNoContent()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/players/1");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task PlayerController_Delete_ReturnsNotFound()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/players/999");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task PlayerController_Delete_ReturnsBadRequest()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/players/invalid-id");
        
        // Act
        var response = await _client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}