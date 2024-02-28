using AutoMapper;
using Business.Services;
using Entities.DTOs;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace DataAccess.Services;
internal sealed class UserService(UserManager<AppUser> userManager, IMapper mapper) : IUserService
{
    public async Task<Result<string>> CreateUserAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            var isEmailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email);
            if (isEmailExists)
            {
                return Result<string>.Failure(500, "Email already has taken");
            }
        }

        if (request.UserName is not null)
        {
            var isUserNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName);
            if (isUserNameExists)
            {
                return Result<string>.Failure(500, "UserName already has taken");
            }
        }

        if (request.IdentityNumber != "11111111111")
        {
            bool identityNumberExists = await userManager.Users.AnyAsync(u => u.IdentityNumber == request.IdentityNumber);
            if (identityNumberExists)
            {
                return Result<string>.Failure(500, "IdentityNumber number already exists");
            }
        }

        var user = mapper.Map<AppUser>(request);

        Random random = new();

        user.EmailConfirmCode = random.Next(100000, 999999);

        if (request.UserType == UserType.Doctor)
        {
            if (request.Specialty is not null)
            {
                user.DoctorDetail = new DoctorDetail()
                {
                    UserId = user.Id,
                    Specialty = (Specialty)request.Specialty,
                    WorkingDays = request.WorkingDays ?? new()
                };
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

        return (200, "User created action successfully");
    }
}
