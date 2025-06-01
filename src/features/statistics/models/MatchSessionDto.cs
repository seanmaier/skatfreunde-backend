namespace skat_back.features.statistics.models;

public record MatchSessionDto(
    string MatchDay,
    DateTime LastUpdated,
    IEnumerable<PlayerMatchDayDataDto>? PlayerData
);