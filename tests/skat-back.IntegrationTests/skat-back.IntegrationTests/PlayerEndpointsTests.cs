using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Xunit.Abstractions;
using static skat_back.IntegrationTests.TestConstants;

namespace skat_back.IntegrationTests;

public class PlayerEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly ITestOutputHelper _helper;

    public PlayerEndpointsTests(CustomWebApplicationFactory factory, ITestOutputHelper helper)
    {
        _factory = factory;
        _helper = helper;
    }

    [Fact]
    public async Task GetAllPlayers_ShouldReturn200()
    {
        var handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            AllowAutoRedirect = false
        };

        var client = _factory.CreateDefaultClient(handler);
        
        _helper.WriteLine($"BaseAddress: {client.BaseAddress}");

        var loginPayload = new
        {
            email = TestUserMail,
            password = TestUserPassword
        };

        var loginContent = new StringContent(
            JsonSerializer.Serialize(loginPayload),
            Encoding.UTF8,
            "application/json");

        var loginResponse = await client.PostAsync("api/auth/login", loginContent);
        loginResponse.EnsureSuccessStatusCode();

        var response = await client.GetAsync("api/players");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}