using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Add-Migration cli use context class and comprehended it for 
/// create Migration Class
/// </summary>
namespace SamuraiApp.Data
{
    
    public class SamuraiContextNoTracking:DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public SamuraiContextNoTracking()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //Use Dbset.AsTracking() for special query to be tracked
        }
        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => 
            {
                builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
            });


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData";
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(connectionString);
              //.UseSqlServer(connectionString,option => option.MaxBatchSize(150));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                        .HasKey(s => new { s.SamuraiId, s.BattleId });
            modelBuilder.Entity<Horse>()
                        .ToTable("Horses");

        }
    }
}
