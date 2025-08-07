using System;
using System.Collections.Generic;
using JopApplication.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JopApplication.Infrastructure.DBContext;

public partial class JobAppDbContext : DbContext
{
    public JobAppDbContext()
    {
    }

    public JobAppDbContext(DbContextOptions<JobAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Data Source=MOSTAFA;Initial Catalog=JopApplication;User ID=sa;Password=27122001;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>()
       .ToTable("AspNetUsers");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Job");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
