using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class ProjectFundingConfiguration : IEntityTypeConfiguration<ProjectFunding>
    {
        public void Configure(EntityTypeBuilder<ProjectFunding> builder)
        {
            builder.HasKey(pf => new { pf.ProjectId, pf.FundingId });

            builder
                .HasOne(pf => pf.Project)
                .WithMany(p => p.ProjectFundings)
                .HasForeignKey(pf => pf.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pf => pf.Funding)
                .WithMany(f => f.ProjectFundings)
                .HasForeignKey(pf => pf.FundingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
