using JobTrackPro.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobTrackPro.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<JobTask> JobTasks => Set<JobTask>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<JobApplication>(entity =>
        {
            entity.Property(x => x.CompanyName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.JobTitle)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Location)
                .HasMaxLength(150);

            entity.Property(x => x.Source)
                .HasMaxLength(100);

            entity.Property(x => x.ApplicationUrl)
                .HasMaxLength(500);

            entity.Property(x => x.SalaryMin)
                .HasColumnType("decimal(18,2)");

            entity.Property(x => x.SalaryMax)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Interview>(entity =>
        {
            entity.Property(x => x.RoundName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.InterviewType)
                .HasMaxLength(100);

            entity.Property(x => x.InterviewerName)
                .HasMaxLength(150);

            entity.Property(x => x.InterviewerEmail)
                .HasMaxLength(150);

            entity.Property(x => x.Outcome)
                .HasMaxLength(100);

            entity.HasOne(x => x.JobApplication)
                .WithMany(x => x.Interviews)
                .HasForeignKey(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<JobTask>(entity =>
        {
            entity.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasOne(x => x.JobApplication)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FullName)
                .HasMaxLength(150)
                .IsRequired();
        });
    }
}