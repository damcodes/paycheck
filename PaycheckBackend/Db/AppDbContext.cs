using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using PaycheckBackend.Models;

namespace PaycheckBackend.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Job>? Jobs { get; set; }
        public DbSet<Workday>? Workdays { get; set; }
        public DbSet<Paycheck>? Paychecks { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();
        }
    }
}