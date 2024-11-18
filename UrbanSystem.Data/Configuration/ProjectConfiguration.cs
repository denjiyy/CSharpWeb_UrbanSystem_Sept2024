﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Location)
                .WithMany(l => l.Projects)
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(f => f.DesiredSum)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(2048);

            builder.Property(p => p.CreatedOn)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.IsCompleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.ToTable("Projects");
        }
    }
}