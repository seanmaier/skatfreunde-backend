namespace skat_back.features.statistics.models;

public record PlayerMatchDayDataDto(
    string Name,
    int MatchShare,
    int TotalPoints,
    List<SeriesDto> Series
);