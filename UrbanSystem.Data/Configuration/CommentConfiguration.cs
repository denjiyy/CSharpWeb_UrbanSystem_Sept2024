using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.AddedOn)
                .IsRequired();

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Suggestion)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.SuggestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Upvotes)
                .HasDefaultValue(0);

            builder.Property(c => c.Downvotes)
                .HasDefaultValue(0);
        }
    }
}