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

            builder
                .HasOne(f => f.Project)
                .WithMany(p => p.Fundings)
                .HasForeignKey(f => f.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.Amount)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(f => f.FundedOn)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
