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
            // Set primary key
            builder.HasKey(cv => cv.Id);

            // Explicitly configure foreign key to Comment
            builder.HasOne(cv => cv.Comment)
                   .WithMany(c => c.CommentVotes)  // Ensure Comment has a collection for CommentVotes
                   .HasForeignKey(cv => cv.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Explicitly configure foreign key to ApplicationUser
            builder.HasOne(cv => cv.User)
                   .WithMany()  // No navigation property needed on ApplicationUser
                   .HasForeignKey(cv => cv.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Ensure each user can vote only once per comment
            builder.HasIndex(cv => new { cv.CommentId, cv.UserId })
                   .IsUnique();

            // Optional: Define table name
            builder.ToTable("CommentVotes");
        }
    }
}
