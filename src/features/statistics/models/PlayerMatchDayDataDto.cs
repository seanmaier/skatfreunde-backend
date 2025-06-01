namespace skat_back.features.statistics.models;

public record PlayerMatchDayDataDto(
    string Name,
    float MatchShare,
    int TotalPoints,
    List<SeriesDto> Series
);