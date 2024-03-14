using Entities.DTOs;
using Entities.Models;
using TS.Result;

namespace Business.Services;
public interface IAppointmentService
{
    Task<Result<string>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
    Task<Result<string>> CompleteAsync(CompleteAppointmentDto request, CancellationToken cancellationToken);
    Task<Result<List<Appointment?>>> GetAllByDoctorIdAsync(Guid DoctorId, CancellationToken cancellationToken);
    Task<Result<AppUser?>> FindPatientbyIdentityNumberAsync(FindPatientDto request, CancellationToken cancellationToken);
    Task<Result<List<AppUser>>> GetAllDoctorsAsync(CancellationToken cancellationToken);
    Task<Result<string>> DeleteByAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken);
}
