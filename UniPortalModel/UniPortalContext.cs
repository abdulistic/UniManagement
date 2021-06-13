using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UniPortalModel
{
    public partial class UniPortalContext : DbContext
    {
        public UniPortalContext()
        {
        }

        public UniPortalContext(DbContextOptions<UniPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignClassStudent> AssignClassStudents { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<UniClass> UniClasses { get; set; }
        public virtual DbSet<UniTest> UniTests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = .; Initial Catalog = UniPortal; User ID = sa; Password = Password1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AssignClassStudent>(entity =>
            {
                entity.HasKey(e => e.AssignClassId);

                entity.ToTable("AssignClassStudent");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.ClassId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.HasKey(e => e.ResultId);

                entity.ToTable("TestResult");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StudentMarks)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UniClass>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("UniClass");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UniTest>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("UniTest");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
