namespace skat_back.features.auth.models;

public record RefreshTokenResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);