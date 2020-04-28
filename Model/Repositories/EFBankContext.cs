using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Model.Repositories
{
    public class EFBankContext : DbContext
    {
        public static IConfigurationRoot configuration;

        //Property per Entity
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Rekening> Rekeningen { get; set; }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(
                builder => builder.AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));

            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Zoek de naam in de connectionstrings - appsettings.json
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("Appsettings.json", false)
                    .Build();

                var connectionString = configuration.GetConnectionString("EFBank");

                if (connectionString != null)
                {
                    optionsBuilder.UseSqlServer(
                        connectionString,
                        options => options.MaxBatchSize(150)) //Max aantal sql commands
                        .UseLoggerFactory(GetLoggerFactory())
                        .EnableSensitiveDataLogging(true); //Toont de waarden van de parameters bij logging
                }
            }            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mappings Klant
            modelBuilder.Entity<Klant>()
                .ToTable("Klanten");
            modelBuilder.Entity<Klant>()
                .HasKey(k => k.KlantId);
            modelBuilder.Entity<Klant>()
                .Property(k => k.KlantId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Klant>()
                .Property(k => k.Naam)
                .IsRequired()
                .HasMaxLength(20);

            //Mappings Rekening
            modelBuilder.Entity<Rekening>()
                .ToTable("Rekeningen");
            modelBuilder.Entity<Rekening>()
                .HasKey(r => r.RekeningNr);
            modelBuilder.Entity<Rekening>()
                .Property(r => r.RekeningNr)
                .ValueGeneratedNever();
            modelBuilder.Entity<Rekening>()
                .HasOne(r => r.Klant)
                .WithMany(r => r.Rekeningen)
                .HasForeignKey(r => r.KlantId);
            modelBuilder.Entity<Rekening>()
                .Ignore(r => r.SoortRekening);

            //Seeding Klanten
            modelBuilder.Entity<Klant>().HasData
                (
                    new 
                    { 
                        KlantId = 1,
                        Naam = "Marge"
                    },
                    new
                    {
                        KlantId = 2,
                        Naam = "Homer"
                    },
                    new
                    {
                        KlantId = 3,
                        Naam = "Lisa"
                    },
                    new
                    {
                        KlantId = 4,
                        Naam = "Maggie"
                    },
                    new
                    {
                        KlantId = 5,
                        Naam = "Bart"
                    }
                );

            //Seeding Rekeningen
            modelBuilder.Entity<Rekening>().HasData
                (
                    new
                    {
                        RekeningNr = "BE68123456789012",
                        KlantId = 1,
                        Saldo = 1000m,
                        SoortRekening = SoortRekening.Z
                    },
                    new
                    {
                        RekeningNr = "BE68234567890169",
                        KlantId = 1,
                        Saldo = 2000m,
                        SoortRekening = SoortRekening.S
                    },
                    new
                    {
                        RekeningNr = "BE68345678901212",
                        KlantId = 2,
                        Saldo = 500m,
                        SoortRekening = SoortRekening.S
                    }
                );
        }
    }
}
