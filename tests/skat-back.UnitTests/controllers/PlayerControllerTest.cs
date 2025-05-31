using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using skat_back.Features.Players;
using skat_back.features.players.models;

namespace skat_back.tests.controllers;

public class PlayerControllerTest
{
    private readonly PlayersController _controller;
    private readonly IPlayerService _service = A.Fake<IPlayerService>();

    public PlayerControllerTest()
    {
        _controller = new PlayersController(_service);
    }

    [Fact]
    public async Task PlayerController_GetAll_ReturnsOk()
    {
        // Act
        var result = await _controller.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PlayerController_GetById_ReturnsOk()
    {
        // Act
        var result = await _controller.GetById(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PlayerController_GetById_ReturnsNotFound()
    {
        // Arrange
        A.CallTo(() => _service.GetByIdAsync(1)).Returns(Task.FromResult<ResponsePlayerDto?>(null));

        // Act
        var result = await _controller.GetById(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task PlayerController_Create_ReturnsOk()
    {
        // Arrange
        var createPlayerDto = new CreatePlayerDto(Guid.NewGuid().ToString(), "Test Player");
        var createdPlayer = new ResponsePlayerDto(1, "Test Player", DateTime.Now, DateTime.Now,
            Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        A.CallTo(() => _service.CreateAsync(createPlayerDto)).Returns(Task.FromResult(createdPlayer));

        // Act
        var result = await _controller.Create(createPlayerDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task PlayerController_Update_ReturnsOk()
    {
        // Arrange
        var updatePlayerDto = new UpdatePlayerDto(Guid.NewGuid().ToString(), "Updated Player");

        A.CallTo(() => _service.UpdateAsync(1, updatePlayerDto)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.Update(1, updatePlayerDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task PlayerController_Update_ReturnsNotFound()
    {
        // Arrange
        var updatePlayerDto = new UpdatePlayerDto(Guid.NewGuid().ToString(), "Updated Player");

        A.CallTo(() => _service.UpdateAsync(1, updatePlayerDto)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.Update(1, updatePlayerDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task PlayerController_Delete_ReturnsNoContent()
    {
        // Arrange
        A.CallTo(() => _service.DeleteAsync(1)).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task PlayerController_Delete_ReturnsNotFound()
    {
        // Arrange
        A.CallTo(() => _service.DeleteAsync(1)).Returns(Task.FromResult(false));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }
}