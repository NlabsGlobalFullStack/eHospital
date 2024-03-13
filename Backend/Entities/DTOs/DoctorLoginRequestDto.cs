namespace Entities.DTOs;
public sealed record DoctorLoginRequestDto(
    string IdentityNumberOrUserNameOrEmail,
    string Password,
    bool rememberMe = false
);
