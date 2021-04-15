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

        public DbSet<Restorans> Restorans { get; set; }

        public DbSet<BronResto> BronResto { get; set; }

        public KzDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            
            RolesResto adminRole = new RolesResto { Id = 1, Name = adminRoleName };
            RolesResto userRole = new RolesResto { Id = 2, Name = userRoleName };
            UsersResto adminUser = new UsersResto { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<RolesResto>().HasData(new RolesResto[] { adminRole, userRole });
            modelBuilder.Entity<UsersResto>().HasData(new UsersResto[] { adminUser }); 
            
            modelBuilder.Entity<UsersResto>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users);

            modelBuilder.Entity<RolesResto>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Role);*/

            modelBuilder.Entity<Citizen>()
                .HasOne(x => x.House)
                .WithMany(x => x.Citizens);

            modelBuilder.Entity<BronResto>()
                .HasOne(x => x.ObjectResto)
                .WithMany(x => x.Brons);

            base.OnModelCreating(modelBuilder);
        }
    }
}
