using Entities.Enums;

namespace Entities.DTOs;
public sealed record RegisterRequestDto(
    string FirstName,
    string LastName,
    string IdentityNumber = "11111111111",
    string FullAddress = "",
    string? Email = null,
    string? UserName = null,
    string? Password = null,
    DateOnly? DateOfBirth = null,
    string? BloodType = null,
    UserType UserType = UserType.User,
    Specialty? Specialty = null,
    List<string>? WorkingDays = null,
    string? PhoneNumber = null
);
