using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using skat_back.Features.Players;
using skat_back.features.players.models;

namespace skat_back.tests.controllers;

public class PlayerControllerTest
{
    private readonly IPlayerService _service = A.Fake<IPlayerService>();

    [Fact]
    public async Task PlayerController_GetAll_ReturnsOk()
    {
        // Arrange
        var controller = new PlayersController(_service);

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PlayerController_GetById_ReturnsOk()
    {
        // Arrange
        var controller = new PlayersController(_service);

        // Act
        var result = await controller.GetById(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PlayerController_Create_ReturnsOk()
    {
        // Arrange
        var controller = new PlayersController(_service);
        var createPlayerDto = new CreatePlayerDto(Guid.NewGuid().ToString(), "Test Player");
        var createdPlayer = new ResponsePlayerDto(1, "Test Player", DateTime.Now, DateTime.Now);

        A.CallTo(() => _service.CreateAsync(createPlayerDto)).Returns(Task.FromResult(createdPlayer));

        // Act
        var result = await controller.Create(createPlayerDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtActionResult>();
    }
}