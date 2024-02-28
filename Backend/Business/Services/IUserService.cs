using Entities.DTOs;
using TS.Result;

namespace Business.Services;
public interface IUserService
{
    Task<Result<string>> CreateUserAsync(RegisterRequestDto request, CancellationToken cancellationToken);
}
