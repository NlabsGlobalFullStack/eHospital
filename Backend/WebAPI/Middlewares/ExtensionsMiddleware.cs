using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUserAsync(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any(u => u.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Cuma",
                    LastName = "KÖSE",
                    IdentityNumber = "11111111111",
                    FullAddress = "Elazığ",
                    DateOfBirth = DateOnly.Parse("29.08.1987"),
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    BloodType = "0 rh-",
                    UserType = Entities.Enums.UserType.Admin
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
