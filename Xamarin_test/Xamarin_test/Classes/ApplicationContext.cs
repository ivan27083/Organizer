using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_test.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin_test;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Xamarin_test.Classes
{
    public class ApplicationContext : DbContext
    {
        private string dbPath;
        string DATABASE_NAME = "base.db";
        IPath IPath;
        public DbSet<Day> days { get; set; } = null!;
        public DbSet<Mission> missions { get; set; } = null!;
        public DbSet<Purpose> purposes { get; set; } = null!;
        public DbSet<Daily> dailies { get; set; } = null!;
        public ApplicationContext()
        {
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(DATABASE_NAME);
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Day>()
                .HasOne(u => u.daily_nav)
                .WithMany(c => c.days)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Purpose>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Purpose>()
                .HasOne(u => u.purp_nav)
                .WithMany(c => c.purposes)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Purpose>()
                .HasOne(u => u.mission_nav)
                .WithMany(c => c.purposes)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
