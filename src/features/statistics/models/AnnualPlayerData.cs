namespace skat_back.features.statistics.models;

public record AnnualPlayerData(
    string PlayerName,
    int PlayerId,
    int GamesCount,
    int TotalPoints,
    int AveragePoints,
    int Difference,
    int WonTotal,
    int LostTotal);