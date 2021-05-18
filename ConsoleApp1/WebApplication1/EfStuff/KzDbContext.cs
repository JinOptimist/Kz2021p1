using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff
{
    public class KzDbContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<SportComplex> SportComplex { get; set; }
        public DbSet<SportEvent> SportEvent { get; set; }
        public DbSet<Fireman> Firemen { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<TripRoute> TripRoute { get; set; }
		    public DbSet<Policeman> Policemen { get; set; }
		    public DbSet<PoliceCallHistory> PoliceCallHistory { get; set; }
		    public DbSet<Violations> Violations { get; set; }
		    public DbSet<PoliceAcademy> PoliceAcademy { get; set; }
		    public DbSet<Answer> Answers { get; set; }
		    public DbSet<Question> Qestions { get; set; }
		    public DbSet<Shift> Shifts { get; set; }
        public DbSet<Flight> Flights { get; set; }


		public KzDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>()
                .HasOne(x => x.House)
                .WithMany(x => x.Citizens);
            modelBuilder.Entity<Citizen>()
               .HasOne(x => x.Fireman)
               .WithOne(x => x.Citizen)
               .HasForeignKey<Fireman>(x => x.CitizenId);


            modelBuilder.Entity<Policeman>()
                .HasOne(c => c.Citizen)
                .WithOne(p => p.Policeman);

            modelBuilder.Entity<Student>()
                       .HasIndex(u => u.Email)
                       .IsUnique();

           /* modelBuilder.Entity<Student>()
                      .HasIndex(u => u.IIN)
                      .IsUnique();

            modelBuilder.Entity<Pupil>()
                      .HasIndex(u => u.IIN)
                      .IsUnique();*/

            modelBuilder.Entity<University>()
                      .HasIndex(u => u.Name)
                      .IsUnique();

            modelBuilder.Entity<School>()
                     .HasIndex(u => u.Name)
                     .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bus>()
                .HasOne(x => x.RoutePlan)
                .WithMany(x => x.Buses);

            modelBuilder.Entity<Policeman>()
                .HasMany(v => v.Violations)
                .WithOne(v => v.Policeman)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Citizen>()
                .HasMany(v => v.Violations)
                .WithOne(c => c.Citizen);

            modelBuilder.Entity<PoliceAcademy>()
                .HasOne(c => c.Citizen)
                .WithOne(p => p.PoliceAcademy);

            modelBuilder.Entity<Policeman>()
                .HasMany(pc => pc.PoliceCallHistories)
                .WithOne(p => p.Policeman)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Citizen>()
                .HasMany(pc => pc.PoliceCallHistories)
                .WithOne(c => c.Citizen);

            modelBuilder.Entity<Shift>()
                .HasOne(p => p.Policeman)
                .WithMany(s => s.Shifts);

            modelBuilder.Entity<Passenger>()
                .HasMany(p => p.Flights)
                .WithMany(f => f.Passengers);
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Passengers)
                .WithMany(p => p.Flights);
            modelBuilder.Entity<Citizen>()
                .HasOne(c => c.PlanePassenger)
                .WithOne(p => p.Citizen)
                .HasForeignKey<Passenger>(p => p.CitizenId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
