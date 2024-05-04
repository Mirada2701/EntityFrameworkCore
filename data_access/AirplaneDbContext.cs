using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore
{
    public class AirplaneDbContext : DbContext
    {
        public AirplaneDbContext()
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }
        //Colections
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=LEGION5\SQLEXPRESS;
                                        Initial Catalog = Airplane;
                                        Integrated Security=True;
                                        Connect Timeout=2;Encrypt=False;
                                        Trust Server Certificate=False;
                                        Application Intent=ReadWrite;
                                        Multi Subnet Failover=False"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API configuration
            modelBuilder.Entity<Airplane>()
                .Property(a => a.Model)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Client>().ToTable("Passangers");
            modelBuilder.Entity<Client>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("FirstName");
            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Flight>().HasKey(f => f.Number);
            modelBuilder.Entity<Flight>()
                .Property(f => f.DepartureCity)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Flight>()
               .Property(f => f.ArrivalCity)
               .IsRequired()
               .HasMaxLength(100);

            //One to many (1.....*)
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Airplane)
                .WithMany(a => a.Flights)
                .HasForeignKey(f => f.AirplaneId);

            //Many to many (*.....*)
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Clients)
                .WithMany(c => c.Flights);

            /*one to one*/
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Credentials)
                .WithOne(c => c.Client)
                .HasForeignKey<Client>(c => c.Id);

            modelBuilder.SeedAirplanes();
            modelBuilder.SeedFlights();
            modelBuilder.SeedCredentials();
            modelBuilder.SeedClients();
        }
    }
}
