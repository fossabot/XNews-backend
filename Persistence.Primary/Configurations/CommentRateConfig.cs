﻿using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class CommentRateConfig : IEntityTypeConfiguration<CommentRate>
    {
        public void Configure(EntityTypeBuilder<CommentRate> builder)
        {
            builder
                .ToTable("CommentRate");
                
            builder
                .Property(e => e.CommentRateId)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(e => e.CommentId, "IX_CommentRate_CommentId");
                
            builder
                .HasOne(d => d.Comment)
                .WithMany(p => p.CommentRates)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_CommentRate_CommentId");
        }
    }
}