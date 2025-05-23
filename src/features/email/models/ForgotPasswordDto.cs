namespace skat_back.features.email.models;

public record ForgotPasswordDto(
    string Email,
    string FrontendUrl
);