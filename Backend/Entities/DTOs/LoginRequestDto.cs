namespace Entities.DTOs;
public sealed record LoginRequestDto(
    string IdentityNumberOrUserNameOrEmail,
    string Password,
    bool rememberMe = false
);
