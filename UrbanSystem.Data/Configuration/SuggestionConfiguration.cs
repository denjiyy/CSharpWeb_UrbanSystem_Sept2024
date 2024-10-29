using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UrbanSystem.Data.Models;
using static UrbanSystem.Common.EntityValidationConstants.Suggestion;

namespace UrbanSystem.Data.Configuration
{
    public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder.Property(s => s.Category)
                .IsRequired()
                .HasMaxLength(CategoryMaxLength);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(s => s.Status)
                .IsRequired();

            builder.Property(s => s.Priority)
                .IsRequired();

            builder.HasOne(s => s.Province)
                .WithMany(m => m.Suggestions)
                .HasForeignKey(s => s.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedSuggestions());
        }

        private List<Suggestion> SeedSuggestions()
        {
            return new List<Suggestion>
            {
                new Suggestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Improve Public Transport",
                    Category = "Transport",
                    AttachmentUrl = null,
                    Description = "Implement more frequent bus routes during peak hours to reduce congestion.",
                    UploadedOn = DateTime.UtcNow,
                    Status = "Pending",
                    Upvotes = 10,
                    Downvotes = 2,
                    Priority = "High",
                    ProvinceId = Guid.Parse("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97") // Sofia
                },
                new Suggestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Park Renovation",
                    Category = "Environment",
                    AttachmentUrl = null,
                    Description = "Renovate the central park by adding new benches, lighting, and a playground area.",
                    UploadedOn = DateTime.UtcNow,
                    Status = "Approved",
                    Upvotes = 25,
                    Downvotes = 1,
                    Priority = "Medium",
                    ProvinceId = Guid.Parse("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97") // Sofia
                },
                new Suggestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Waste Management System",
                    Category = "Waste Management",
                    AttachmentUrl = null,
                    Description = "Introduce a recycling program and increase the frequency of waste collection.",
                    UploadedOn = DateTime.UtcNow,
                    Status = "In Review",
                    Upvotes = 15,
                    Downvotes = 0,
                    Priority = "High",
                    ProvinceId = Guid.Parse("c86d7f51-e3c4-4f62-b557-b7bffdecbfe9") // Plovdiv
                }
            };
        }
    }
}
