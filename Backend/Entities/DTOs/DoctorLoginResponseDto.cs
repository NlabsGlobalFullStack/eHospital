namespace Entities.DTOs;
public sealed record DoctorLoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RehfreshTokenExpires
);
