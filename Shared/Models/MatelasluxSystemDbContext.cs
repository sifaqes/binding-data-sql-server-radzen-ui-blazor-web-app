﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BindingData_SQL_EF.Shared.Models;

public partial class MatelasluxSystemDbContext : DbContext
{
    public MatelasluxSystemDbContext()
    {
    }

    public MatelasluxSystemDbContext(DbContextOptions<MatelasluxSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=matelaslux_system_db;User ID=admin;Password=admin;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC072C6AB968");

            entity.ToTable("Book");

            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
