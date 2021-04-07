using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff
{
    public class KzDbContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }

        public DbSet<Adress> Adress { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        public KzDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>()
                .HasOne(x => x.House)
                .WithMany(x => x.Citizens);

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
        }
    }
}
