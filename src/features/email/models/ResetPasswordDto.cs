namespace skat_back.features.email.models;

public record ResetPasswordDto(
    string Token,
    string Email,
    string NewPassword
);