﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;
public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(p => p.Price).HasColumnType("money");
        builder.HasQueryFilter(f => (!f.Doctor!.IsDeleted || !f.Patient!.IsDeleted) && !f.IsItFinished);
    }
}
