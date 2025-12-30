using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;
internal class NotificationDbContext : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
                      .ToTable("Notifications");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Type)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(p => p.Title)
            .IsRequired();

        builder
           .Property(p => p.Status)
            .HasConversion<int>()
           .IsRequired();
    }
}
