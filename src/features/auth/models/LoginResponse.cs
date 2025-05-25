namespace skat_back.features.auth.models;

public record LoginResponse(
    string AccessToken,
    RefreshToken RefreshToken
);