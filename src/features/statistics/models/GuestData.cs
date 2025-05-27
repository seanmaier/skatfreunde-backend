namespace skat_back.features.statistics.models;

public record GuestData(
    string GuestName,
    int Games,
    int TotalPoints,
    int AveragePoints,
    int Difference);