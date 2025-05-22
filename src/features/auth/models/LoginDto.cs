namespace skat_back.features.auth.models;

public record LoginDto(
    string LoginInput,
    string Password,
    bool RememberMe
);