namespace Entities.DTOs;
public sealed record LoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RehfreshTokenExpires
);
