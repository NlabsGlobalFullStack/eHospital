using AutoMapper;
using Business.Services;
using Entities.DTOs;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace DataAccess.Services;
internal sealed class UserService(UserManager<AppUser> userManager, IMapper mapper) : IUserService
{
    public async Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            var isEmailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email);
            if (isEmailExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Email already has taken");
            }
        }

        if (request.UserName is not null)
        {
            var isUserNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName);
            if (isUserNameExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "UserName already has taken");
            }
        }

        if (request.IdentityNumber != "11111111111")
        {
            bool identityNumberExists = await userManager.Users.AnyAsync(u => u.IdentityNumber == request.IdentityNumber);
            if (identityNumberExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "IdentityNumber number already exists");
            }
        }

        var user = mapper.Map<AppUser>(request);
        user.UserType = UserType.Patient;
        int number = 1;
        while(await userManager.Users.AnyAsync(u => u.UserName == user.UserName))
        {
            number++;
            user.UserName += number;
        }

        Random random = new();

        var isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);

            if (!userManager.Users.Any(u => u.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }            
        }

        if (request.Specialty is not null)
        {
            if (request.UserType == UserType.Doctor)
            {
                user.DoctorDetail = new DoctorDetail()
                {
                    Specialty = (Specialty)request.Specialty,
                    WorkingDays = request.WorkingDays ?? new()
                };
            }

            if (user.UserType == UserType.Nurse)
            {
                //
            }
        }

        IdentityResult result;
        if (request.Password is not null)
        {
            result = await userManager.CreateAsync(user, request.Password);
        }
        else
        {
            result = await userManager.CreateAsync(user); 
        }

        if (result.Succeeded)
        {
            return Result<string>.Succeed("User create is successfull");
        }

        return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
    }
    public async Task<Result<Guid>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            var ismailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email);
            if (ismailExists)
            {
                return Result<Guid>.Failure(StatusCodes.Status409Conflict, "Email already has taken");
            }
        }

        if (request.IdentityNumber != "11111111111")
        {
            var identityNumberExists = await userManager.Users.AnyAsync(u => u.IdentityNumber == request.IdentityNumber);
            if (identityNumberExists)
            {
                return Result<Guid>.Failure(StatusCodes.Status409Conflict, "IdentityNumber number already exists");
            }
        }
        AppUser user = mapper.Map<AppUser>(request);
        user.UserType = UserType.Patient;

        int number = 0;
        while (await userManager.Users.AnyAsync(u => u.UserName == user.UserName))
        {
            number++;
            user.UserName += number;
        }

        Random random = new();

        var isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);

            if (!userManager.Users.Any(u => u.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        user.EmailConfirmCodeSendDate = DateTime.UtcNow;

        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return Result<Guid>.Failure(500, result.Errors.Select(e => e.Description).ToList());
        }

        return Result<Guid>.Succeed(user.Id);
    }
}


