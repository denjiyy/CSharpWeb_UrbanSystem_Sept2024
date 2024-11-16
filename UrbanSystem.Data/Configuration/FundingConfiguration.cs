using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class FundingConfiguration : IEntityTypeConfiguration<Funding>
    {
        public void Configure(EntityTypeBuilder<Funding> builder)
        {
            builder.HasKey(f => f.Id);


            builder.Property(f => f.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(f => f.CreatedOn)
                   .IsRequired();

            builder
                .HasMany(f => f.ProjectFundings)
                .WithOne(pf => pf.Funding)
                .HasForeignKey(pf => pf.FundingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
