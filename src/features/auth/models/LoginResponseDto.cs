namespace skat_back.features.auth.models;

public record LoginResponseDto(
    string AccessToken,
    RefreshToken RefreshToken
);