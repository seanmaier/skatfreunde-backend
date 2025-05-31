namespace skat_back.features.statistics.models;

public record SeriesDto(
    int Points,
    int Won,
    int Lost
);