using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;
public static class UserManagerExtensions
{
    public static async Task<AppUser?> FindByIdentityNumber(this UserManager<AppUser> userManager, string identityNumber, CancellationToken cancellationToken)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.IdentityNumber == identityNumber, cancellationToken);
    }
}
