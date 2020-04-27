using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories
{
    public class EFBankContext : DbContext
    {
        public static IConfigurationRoot configuration;

        //Property per Entity
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Rekening> Rekeningen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
                    options => options.MaxBatchSize(150)); //Max aantal sql commands
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
        }
    }
}
