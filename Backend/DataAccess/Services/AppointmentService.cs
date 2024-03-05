﻿using AutoMapper;
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
    public async Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        AppUser? doctor = await userManager.Users.Include(p => p.DoctorDetail).FirstOrDefaultAsync(p => p.Id == request.DoctorId);
        if (doctor is null || doctor.UserType is not UserType.Doctor)
        {
            return Result<string>.Failure("Doctor not found");
        }

        string day = request.StartDate.ToString("dddd");

        if (!doctor.DoctorDetail!.WorkingDays.Contains(day))
        {
            return Result<string>.Failure("Doctor is not working that day");
        }

        IQueryable<Appointment> appointments =
                 appointmentRepository
                .GetWhere(p => p.DoctorId == request.DoctorId);

        DateTime startDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        DateTime endDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        bool isDoctorHaveAppointment = true;

        isDoctorHaveAppointment = appointments.Any(p => p.StartDate <= startDate && p.EndDate > startDate);

        if (isDoctorHaveAppointment)
        {
            return Result<string>.Failure("Doctor is not available in that time");
        }

        isDoctorHaveAppointment = appointments.Any(p => p.StartDate < endDate && p.EndDate >= endDate);

        if (isDoctorHaveAppointment)
        {
            return Result<string>.Failure("Doctor is not available in that time");
        }

        Appointment appointment = mapper.Map<Appointment>(request);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Create appointment is succedded");
    }
}
