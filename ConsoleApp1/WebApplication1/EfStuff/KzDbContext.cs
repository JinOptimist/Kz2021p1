﻿using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<Citizen>()
                .HasOne(x => x.House)
                .WithMany(x => x.Citizens);

            modelBuilder.Entity<BronResto>()
                .HasOne(x => x.Restoranses)
                .WithMany(x => x.Brons);

            base.OnModelCreating(modelBuilder);
        }
    }
}
