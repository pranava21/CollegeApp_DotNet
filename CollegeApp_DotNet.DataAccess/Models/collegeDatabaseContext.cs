﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CollegeApp_DotNet.DataAccess.Models
{
    public partial class collegeDatabaseContext : DbContext
    {
        public collegeDatabaseContext()
        {
        }

        public collegeDatabaseContext(DbContextOptions<collegeDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=192.168.0.144;Database=collegeDatabase;Username=postgres;Password=pranava");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.AttendanceUid)
                    .HasName("Attendance_pkey");

                entity.ToTable("Attendance", "rsuniversity");

                entity.Property(e => e.AttendanceUid).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AttendedOn).HasColumnType("timestamp without time zone");

                entity.Property(e => e.IsPresent).HasColumnName("isPresent");

                entity.HasOne(d => d.DepartmentU)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.DepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_attendance_fkey");

                entity.HasOne(d => d.FacultyU)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.FacultyUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("faculty_attendance_fkey");

                entity.HasOne(d => d.StudentU)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_attendance_fkey");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentUid)
                    .HasName("department_pkey");

                entity.ToTable("Department", "rsuniversity");

                entity.Property(e => e.DepartmentUid).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.DepartmentCode).HasMaxLength(10);

                entity.Property(e => e.DepartmentImageUrl).HasMaxLength(1000);

                entity.Property(e => e.DepartmentName).HasMaxLength(200);
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.FacultyUid)
                    .HasName("faculty_pkey");

                entity.ToTable("Faculty", "rsuniversity");

                entity.Property(e => e.FacultyUid).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FacultyId).HasMaxLength(10);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.HasOne(d => d.DepartmentU)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.DepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("faculty_department_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentUid)
                    .HasName("student_pkey");

                entity.ToTable("Student", "rsuniversity");

                entity.Property(e => e.StudentUid).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.StudentId).HasMaxLength(10);

                entity.HasOne(d => d.DepartmentU)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_department_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserUid)
                    .HasName("User_pkey");

                entity.ToTable("Users", "rsuniversity");

                entity.Property(e => e.UserUid).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.EmailId).HasMaxLength(200);

                entity.Property(e => e.FirstName).HasMaxLength(200);

                entity.Property(e => e.IsFaculty).HasColumnName("isFaculty");

                entity.Property(e => e.IsStudent).HasColumnName("isStudent");

                entity.Property(e => e.LastName).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.HasOne(d => d.DepartmentU)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DepartmentUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_user_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
