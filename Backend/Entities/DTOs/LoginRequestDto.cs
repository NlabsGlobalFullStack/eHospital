namespace Entities.DTOs;
public sealed record LoginRequestDto(
    string UserNameOrEmail,
    string Password,
    bool rememberMe = false
);
