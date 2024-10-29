using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UrbanSystem.Data.Models;
using static UrbanSystem.Common.EntityValidationConstants.Comment;

namespace UrbanSystem.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(ContentMaxLength);

            builder.Property(c => c.PostedOn)
                .IsRequired();

            builder.HasOne(c => c.Suggestion)
                   .WithMany(s => s.Comments)
                   .HasForeignKey(c => c.SuggestionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
