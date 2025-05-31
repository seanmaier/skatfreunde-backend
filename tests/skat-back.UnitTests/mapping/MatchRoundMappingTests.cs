using FluentAssertions;
using skat_back.features.matches.matchRounds.models;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.features.matchRounds.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.mapping;

namespace skat_back.Tests.mapping;

public class MatchRoundMappingTests
{
    [Fact]
    public void MatchRoundMapping_CreateMatchRoundDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createMatchRoundDto = new CreateMatchRoundDto
        (
            "1",
            "Test Round",
            new List<CreatePlayerRoundStatsDto>
            {
                new (1, 100, 50, 50),
                new (2, 200, 100, 100)
            }
        );

        // Act
        var matchRound = createMatchRoundDto.ToEntity();

        // Assert
        matchRound.RoundNumber.Should().Be(createMatchRoundDto.RoundNumber);
        matchRound.Table.Should().Be(createMatchRoundDto.Table);
        matchRound.PlayerRoundStats.Should().HaveCount(createMatchRoundDto.PlayerRoundStats.Count);
        matchRound.PlayerRoundStats.ElementAt(0).PlayerId.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(0).PlayerId);
        matchRound.PlayerRoundStats.ElementAt(0).Points.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(0).Points);
        matchRound.PlayerRoundStats.ElementAt(0).Won.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(0).Won);
        matchRound.PlayerRoundStats.ElementAt(0).Lost.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(0).Lost);
        matchRound.PlayerRoundStats.ElementAt(1).PlayerId.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(1).PlayerId);
        matchRound.PlayerRoundStats.ElementAt(1).Points.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(1).Points);
        matchRound.PlayerRoundStats.ElementAt(1).Won.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(1).Won);
        matchRound.PlayerRoundStats.ElementAt(1).Lost.Should().Be(createMatchRoundDto.PlayerRoundStats.ElementAt(1).Lost);
    }
    
    [Fact]
    public void MatchRoundMapping_UpdateMatchRoundDto_ToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var updateMatchRoundDto = new UpdateMatchRoundDto
        (
            "1",
            "Updated Round",
            new List<UpdatePlayerRoundStatsDto>
            {
                new (1, 150, 75, 75),
                new (2, 250, 125, 125)
            }
        );

        // Act
        var matchRound = updateMatchRoundDto.ToEntity();

        // Assert
        matchRound.RoundNumber.Should().Be(updateMatchRoundDto.RoundNumber);
        matchRound.Table.Should().Be(updateMatchRoundDto.Table);
        matchRound.PlayerRoundStats.Should().HaveCount(updateMatchRoundDto.PlayerRoundStats.Count);
        matchRound.PlayerRoundStats.ElementAt(0).PlayerId.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(0).PlayerId);
        matchRound.PlayerRoundStats.ElementAt(0).Points.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(0).Points);
        matchRound.PlayerRoundStats.ElementAt(0).Won.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(0).Won);
        matchRound.PlayerRoundStats.ElementAt(0).Lost.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(0).Lost);
        matchRound.PlayerRoundStats.ElementAt(1).PlayerId.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(1).PlayerId);
        matchRound.PlayerRoundStats.ElementAt(1).Points.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(1).Points);
        matchRound.PlayerRoundStats.ElementAt(1).Won.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(1).Won);
        matchRound.PlayerRoundStats.ElementAt(1).Lost.Should().Be(updateMatchRoundDto.PlayerRoundStats.ElementAt(1).Lost);
    }
    
    [Fact]
    public void MatchRoundMapping_MatchRound_ToResponse_ShouldMapCorrectly()
    {
        // Arrange
        var matchRound = new MatchRound
        {
            Id = 1,
            RoundNumber = "1",
            Table = "Table 1",
            PlayerRoundStats = new List<PlayerRoundStats>
            {
                new PlayerRoundStats { PlayerId = 1, Points = 100, Won = 50, Lost = 50 },
                new PlayerRoundStats { PlayerId = 2, Points = 200, Won = 100, Lost = 100 }
            }
        };

        // Act
        var responseMatchRoundDto = matchRound.ToResponse();

        // Assert
        responseMatchRoundDto.Id.Should().Be(matchRound.Id);
        responseMatchRoundDto.RoundNumber.Should().Be(matchRound.RoundNumber);
        responseMatchRoundDto.Table.Should().Be(matchRound.Table);
        responseMatchRoundDto.PlayerRoundStats.Should().HaveCount(matchRound.PlayerRoundStats.Count);
        responseMatchRoundDto.PlayerRoundStats.ElementAt(0).PlayerId.Should().Be(matchRound.PlayerRoundStats.ElementAt(0).PlayerId);
        responseMatchRoundDto.PlayerRoundStats.ElementAt(0).Points.Should().Be(matchRound.PlayerRoundStats.ElementAt(0).Points);
        responseMatchRoundDto.PlayerRoundStats.ElementAt(0).Won.Should().Be(matchRound.PlayerRoundStats.ElementAt(0).Won);
        responseMatchRoundDto.PlayerRoundStats.ElementAt(0).Lost.Should().Be(matchRound.PlayerRoundStats.ElementAt(0).Lost);
    }
    
    [Fact]
    public void MatchRoundMapping_MatchRound_ToResponse_WithEmptyStats_ShouldMapCorrectly()
    {
        // Arrange
        var matchRound = new MatchRound
        {
            Id = 1,
            RoundNumber = "1",
            Table = "Table 1",
            PlayerRoundStats = new List<PlayerRoundStats>()
        };

        // Act
        var responseMatchRoundDto = matchRound.ToResponse();

        // Assert
        responseMatchRoundDto.Id.Should().Be(matchRound.Id);
        responseMatchRoundDto.RoundNumber.Should().Be(matchRound.RoundNumber);
        responseMatchRoundDto.Table.Should().Be(matchRound.Table);
        responseMatchRoundDto.PlayerRoundStats.Should().BeEmpty();
    }
    
    [Fact]
    public void MatchRoundMapping_NullCreateMatchRoundDto_ToEntity_ShouldThrowArgumentNullException()
    {
        // Arrange
        CreateMatchRoundDto? createMatchRoundDto = null;

        // Act
        Action act = () => createMatchRoundDto.ToEntity();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }
    
    [Fact]
    public void MatchRoundMapping_NullUpdateMatchRoundDto_ToEntity_ShouldThrowArgumentNullException()
    {
        // Arrange
        UpdateMatchRoundDto? updateMatchRoundDto = null;

        // Act
        Action act = () => updateMatchRoundDto.ToEntity();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }
    
    [Fact]
    public void MatchRoundMapping_NullMatchRound_ToResponse_ShouldThrowArgumentNullException()
    {
        // Arrange
        MatchRound? matchRound = null;

        // Act
        Action act = () => matchRound.ToResponse();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }
    
    [Fact]
    public void MatchRoundMapping_EmptyMatchRound_ToResponse_ShouldReturnDefaultResponseMatchRoundDto()
    {
        // Arrange
        var matchRound = new MatchRound
        {
            RoundNumber = null,
            Table = null
        };

        // Act
        var responseMatchRoundDto = matchRound.ToResponse();

        // Assert
        responseMatchRoundDto.Id.Should().Be(0);
        responseMatchRoundDto.RoundNumber.Should().BeNull();
        responseMatchRoundDto.Table.Should().BeNull();
        responseMatchRoundDto.PlayerRoundStats.Should().BeEmpty();
    }
    
    [Fact]
    public void MatchRoundMapping_EmptyPlayerRoundStats_ToResponse_ShouldReturnEmptyList()
    {
        // Arrange
        var matchRound = new MatchRound
        {
            Id = 1,
            RoundNumber = "1",
            Table = "Table 1",
            PlayerRoundStats = new List<PlayerRoundStats>()
        };

        // Act
        var responseMatchRoundDto = matchRound.ToResponse();

        // Assert
        responseMatchRoundDto.PlayerRoundStats.Should().BeEmpty();
    }
    
    [Fact]
    public void MatchRoundMapping_EmptyPlayerRoundStats_ToEntity_ShouldReturnEmptyList()
    {
        // Arrange
        var createMatchRoundDto = new CreateMatchRoundDto
        (
            "1",
            "Test Round",
            new List<CreatePlayerRoundStatsDto>()
        );

        // Act
        var matchRound = createMatchRoundDto.ToEntity();

        // Assert
        matchRound.PlayerRoundStats.Should().BeEmpty();
    }
    
    [Fact]
    public void MatchRoundMapping_EmptyPlayerRoundStats_InUpdateDto_ToEntity_ShouldReturnEmptyList()
    {
        // Arrange
        var updateMatchRoundDto = new UpdateMatchRoundDto
        (
            "1",
            "Updated Round",
            new List<UpdatePlayerRoundStatsDto>()
        );

        // Act
        var matchRound = updateMatchRoundDto.ToEntity();

        // Assert
        matchRound.PlayerRoundStats.Should().BeEmpty();
    }
    
    [Fact]
    public void MatchRoundMapping_NullMatchRound_ToResponse_ShouldReturnDefaultResponseMatchRoundDto()
    {
        // Arrange
        MatchRound? matchRound = null;

        // Act
        Action act = () => matchRound.ToResponse();

        // Assert
        act.Should().Throw<NullReferenceException>();
    }
}