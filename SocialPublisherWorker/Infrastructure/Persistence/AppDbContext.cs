using Microsoft.EntityFrameworkCore;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PostPublication> PostPublications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PostPublication>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ExternalPostId)
                .HasMaxLength(255);

            entity.HasIndex(x => new
                {
                    x.Platform,
                    x.WeekStart
                })
                .IsUnique();
        });
    }
}