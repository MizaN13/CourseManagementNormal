using CourseManagementNormal.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseManagementNormal.Web.Data
{
    public class SchoolContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public SchoolContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    if (!optionBuilder.IsConfigured)
        //        optionBuilder.UseSqlServer(
        //            _connectionString,
        //            b => b.MigrationsAssembly(_migrationAssemblyName)
        //        );
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                        .HasIndex(s => s.Name)
                        .IsUnique();
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Student>()
                         .Property(s => s.Id)
                         .HasDefaultValueSql("newid()");
            modelBuilder.Entity<Course>()
                        .Property(c => c.Id)
                        .HasDefaultValueSql("newid()");
            modelBuilder.Entity<Instructor>()
                        .Property(i => i.Id)
                        .HasDefaultValueSql("newid()");
            modelBuilder.Entity<Course>()
                        .HasIndex(c => c.Name)
                        .IsUnique();
            modelBuilder.Entity<StudentCourse>()
                        .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(d => d.Students)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //One to One relationship between Course and Istructure
            modelBuilder.Entity<Course>()
               .HasOne(i => i.Instructor)
               .WithOne(c => c.Course)
               .HasForeignKey<Course>(i => i.InstrutorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
