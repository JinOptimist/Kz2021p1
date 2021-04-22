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

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<ElectionBallot> ElectionBallots { get; set; }
        public DbSet<CandidateElection> CandidateElections { get; set; }
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
            
            modelBuilder.Entity<Citizen>()
                .HasMany(pc => pc.Candidates)
                .WithOne(c => c.Citizen);
            
            modelBuilder.Entity<Citizen>()
                .HasMany(pc => pc.Ballots)
                .WithOne(c => c.Citizen);


            modelBuilder.Entity<CandidateElection>()
                .HasKey(ce => new {ce.CandidateId, ce.ElectionId});
            modelBuilder.Entity<CandidateElection>()
                .HasOne(ce => ce.Candidate)
                .WithMany(c => c.CandidateElections)
                .HasForeignKey(ce => ce.CandidateId);
            modelBuilder.Entity<CandidateElection>()
                .HasOne(ce => ce.Election)
                .WithMany(e => e.CandidateElections)
                .HasForeignKey(ce => ce.ElectionId);

          
            modelBuilder.Entity<ElectionBallot>()
                .HasKey(eb => new {eb.ElectionId, eb.BallotId});
            modelBuilder.Entity<ElectionBallot>()
                .HasOne(eb => eb.Election)
                .WithMany(e => e.ElectionBallots)
                .HasForeignKey(eb => eb.ElectionId);
            modelBuilder.Entity<ElectionBallot>()
                .HasOne(eb => eb.Ballot)
                .WithMany(b => b.ElectionBallots)
                .HasForeignKey(eb => eb.BallotId);

            modelBuilder.Entity<Ballot>()
                .HasOne(c => c.Candidate)
                .WithMany(c => c.Ballots);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

    }
}
