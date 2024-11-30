using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class CommentVoteConfiguration : IEntityTypeConfiguration<CommentVote>
    {
        public void Configure(EntityTypeBuilder<CommentVote> builder)
        {
            builder.HasKey(cv => cv.Id);

            builder.HasOne(cv => cv.Comment)
                   .WithMany(c => c.CommentVotes)
                   .HasForeignKey(cv => cv.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.User)
                   .WithMany()
                   .HasForeignKey(cv => cv.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cv => new { cv.CommentId, cv.UserId })
                   .IsUnique();
        }
    }
}
