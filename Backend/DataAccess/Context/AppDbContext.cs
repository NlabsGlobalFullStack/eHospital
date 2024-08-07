﻿using Entities.Models;
using Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;
internal sealed class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DoctorDetail>? DoctorDetails { get; set; }
    public DbSet<Appointment>? Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUser).Assembly);

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
    }
}
