namespace skat_back.features.statistics.models;

public record AnnualPlayerData(
    string PlayerName,
    int Games,
    int TotalPoints,
    int AveragePoints,
    int Difference);