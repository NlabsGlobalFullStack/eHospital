using Entities.DTOs;
using TS.Result;

namespace Business.Services;
public interface IAppointmentService
{
    Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
}
