using Entities.DTOs;
using Entities.Models;
using TS.Result;

namespace Business.Services;
public interface IUserService
{
    Task<Result<string>> CreateUserAsync(RegisterRequestDto request, CancellationToken cancellationToken);
    Task<Result<AppUser>> FindPatientWithIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken);
    Task<Result<string>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken);
    Task<Result<List<AppUser>>> GetAllDoctorsAsync(CancellationToken cancellationToken);
}
