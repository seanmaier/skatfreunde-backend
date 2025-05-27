namespace skat_back.features.statistics.models;

public record AnnualDataResponseDto(
    AnnualPlayerData[] PlayersData,
    GuestData[] GuestsData,
    string MatchDay,
    DateTime LastUpdated
);