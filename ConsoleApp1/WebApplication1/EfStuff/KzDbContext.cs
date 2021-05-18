using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.EfStuff
{
    public class KzDbContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }

        public DbSet<Adress> Adress { get; set; }
        public DbSet<IncomingFlightInfo> IncomingFlightsInfo { get; set; }
        public DbSet<DepartingFlightInfo> DepartingFlightsInfo { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
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

        public DbSet<HCEstablishments> HCEstablishments { get; set; }
        public DbSet<HCWorker> HCWorker { get; set; }

        public DbSet<TvProgramme> TvProgrammes { get; set; }
        public DbSet<TvSchedule> TvSchedules { get; set; }
        public DbSet<TvStaff> TvStaff { get; set; }
        public DbSet<TvProgrammeStaff> TvProgrammeStaff { get; set; }
        public DbSet<TvChannel> TvChannels { get; set; }
        public DbSet<TvCelebrity> TvCelebrities { get; set; }
        public DbSet<TvProgrammeCelebrity> TvProgrammeCelebrities { get; set; }

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
                .WithOne(v => v.Policeman);

            modelBuilder.Entity<Citizen>()
                .HasMany(v => v.Violations)
                .WithOne(c => c.Citizen);

            modelBuilder.Entity<PoliceAcademy>()
                .HasOne(c => c.Citizen)
                .WithOne(p => p.PoliceAcademy);

            modelBuilder.Entity<Policeman>()
                .HasMany(pc => pc.PoliceCallHistories)
                .WithOne(p => p.Policeman);

            modelBuilder.Entity<Citizen>()
                .HasMany(pc => pc.PoliceCallHistories)
                .WithOne(c => c.Citizen);

            modelBuilder.Entity<HCWorker>()
                .HasOne(x => x.Facility)
                .WithMany(x => x.Workers)
                .HasForeignKey(x => x.FacilityId);

            modelBuilder.Entity<Citizen>()
                .HasOne(x => x.HCWorker)
                .WithOne(x => x.Citizen)
                .HasForeignKey<HCWorker>(x => x.CitizenId);

            modelBuilder.Entity<TvProgramme>()
               .HasMany(x => x.Schedules)
               .WithOne(x => x.Programme);

            modelBuilder.Entity<TvStaff>()
               .HasOne(x => x.Citizen)
               .WithOne(x => x.TvStaff);

            modelBuilder.Entity<TvProgrammeStaff>()
               .HasOne(x => x.Staff)
               .WithMany(x => x.Programme);

            modelBuilder.Entity<TvProgrammeStaff>()
              .HasOne(x => x.Programme)
              .WithMany(x => x.Staff);

            modelBuilder.Entity<TvProgramme>()
                .HasOne(x => x.Channel)
                .WithMany(x => x.Programmes);

            modelBuilder.Entity<TvStaff>()
                .HasOne(x => x.Channel)
                .WithMany(x => x.Staff);
            modelBuilder.Entity<TvCelebrity>()
               .HasOne(x => x.Citizen)
               .WithOne(x => x.TvCelebrity);

            modelBuilder.Entity<TvProgrammeCelebrity>()
               .HasOne(x => x.Celebrity)
               .WithMany(x => x.Programme);

            modelBuilder.Entity<TvProgrammeCelebrity>()
              .HasOne(x => x.Programme)
              .WithMany(x => x.Celebrities);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
