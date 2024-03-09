using AutoMapper;
using Business.Services;
using Entities.DTOs;
using Entities.Enums;
using Entities.Models;
using Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace DataAccess.Services;
internal sealed class AppointmentService(
    UserManager<AppUser> userManager,
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IAppointmentService
{
    public async Task<Result<string>> CompleteAsync(CompleteAppointmentDto request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(a => a.Id == request.AppointmentId, cancellationToken);
        if (appointment is null)
        {
            return Result<string>.Failure("Appointment not found");
        }

        if (appointment.IsItFinished)
        {
            return Result<string>.Failure("Appointment already finsh. you cannot close again");
        }

        appointment.EpicrisisReport = request.EpicrisisReport;
        appointment.IsItFinished = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Appointment is completed");
    }

    public async Task<Result<string>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        var doctor = await userManager.Users.Include(u => u.DoctorDetail).FirstOrDefaultAsync(u => u.Id == request.DoctorId);
        if (doctor is null || doctor.UserType is not UserType.Doctor)
        {
            return Result<string>.Failure("Doctor not found");
        }

        string day = request.StartDate.ToString("dddd");

        if (!doctor.DoctorDetail!.WorkingDays.Contains(day))
        {
            return Result<string>.Failure("Doctor is not working that day");
        }

        var appointments =
                 appointmentRepository
                .GetWhere(p => p.DoctorId == request.DoctorId);

        var startDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        var isDoctorHaveAppointment = true;

        isDoctorHaveAppointment = await appointments
            .AnyAsync(a => 
                (
                    (a.StartDate < endDate && a.StartDate > startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                    (a.EndDate > startDate && a.EndDate == endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                    (a.StartDate >= startDate && a.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                    (a.StartDate <= startDate && a.EndDate >= endDate) // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                )
                ,cancellationToken
            );

        if (isDoctorHaveAppointment)
        {
            return Result<string>.Failure("Doctor is not available in that time");
        }
        
        var appointment = mapper.Map<Appointment>(request);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Create appointment is succedded");
    }

    public async Task<Result<List<Appointment>>> GetAllByDoctorIdAsync(Guid DoctorId, CancellationToken cancellationToken)
    {
        var appointments = await appointmentRepository
            .GetWhere(a => a.DoctorId == DoctorId)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .OrderBy(a => a.StartDate).ToListAsync();

        return Result<List<Appointment>>.Succeed(appointments);
    }
}
