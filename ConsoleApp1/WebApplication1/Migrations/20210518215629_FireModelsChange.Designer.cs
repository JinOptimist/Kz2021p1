﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.EfStuff;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(KzDbContext))]
    [Migration("20210518215629_FireModelsChange")]
    partial class FireModelsChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CertificatePupil", b =>
                {
                    b.Property<long>("CertificatesId")
                        .HasColumnType("bigint");

                    b.Property<long>("PupilsId")
                        .HasColumnType("bigint");

                    b.HasKey("CertificatesId", "PupilsId");

                    b.HasIndex("PupilsId");

                    b.ToTable("CertificatePupil");
                });

            modelBuilder.Entity("CertificateStudent", b =>
                {
                    b.Property<long>("CertificatesId")
                        .HasColumnType("bigint");

                    b.Property<long>("StudentsId")
                        .HasColumnType("bigint");

                    b.HasKey("CertificatesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CertificateStudent");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Adress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FloorCount")
                        .HasColumnType("int");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Adress");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Airport.DepartingFlightInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Airline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DepartingFlightsInfo");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Airport.IncomingFlightInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Airline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ETA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IncomingFlightsInfo");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Airport.Passenger", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Bus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("RoutePlanId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoutePlanId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Certificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Citizen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatingDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("HouseId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsOutOfCity")
                        .HasColumnType("bit");

                    b.Property<int>("Local")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("Citizens");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FireIncident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Dead")
                        .HasColumnType("int");

                    b.Property<long?>("FiremanId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Injured")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FiremanId");

                    b.HasIndex("TeamId");

                    b.ToTable("FireIncidents");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FireTruck", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TruckNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TruckState")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FireTrucks");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.Fireman", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FiremanTeamId")
                        .HasColumnType("bigint");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("WorkExperYears")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId")
                        .IsUnique();

                    b.HasIndex("FiremanTeamId");

                    b.ToTable("Firemen");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Shift")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamState")
                        .HasColumnType("int");

                    b.Property<long?>("TruckId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TruckId")
                        .IsUnique()
                        .HasFilter("[TruckId] IS NOT NULL");

                    b.ToTable("FiremanTeams");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.PoliceAcademy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<string>("EMail")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId")
                        .IsUnique();

                    b.ToTable("PoliceAcademy", "Police");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.PoliceCallHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CallPriority")
                        .HasColumnType("int");

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCall")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PolicemanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("PolicemanId");

                    b.ToTable("PoliceCallHistory", "Police");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Policeman", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("StartWork")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId")
                        .IsUnique();

                    b.ToTable("Policemen", "Police");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Pupil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageMark")
                        .HasColumnType("float");

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClassYear")
                        .HasColumnType("int");

                    b.Property<int?>("ENT")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("GraduatedYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("IIN")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("SchoolId")
                        .HasColumnType("bigint");

                    b.Property<string>("Subject")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Pupils");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.School", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.SportComplex", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountOfEmployees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SportComplex");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.SportEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SportEvent");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.SportSection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("SportComplexId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SportComplexId");

                    b.ToTable("SportSection");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseYear")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("EnteredYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Faculty")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Gpa")
                        .HasColumnType("float");

                    b.Property<DateTime?>("GraduatedYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("IIN")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("OnGrant")
                        .HasColumnType("bit");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("UniversityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UniversityId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.TripRoute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TripTime")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TripRoute");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.University", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Violations", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CitizenId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<long>("PolicemanId")
                        .HasColumnType("bigint");

                    b.Property<int>("SeverityViolation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CitizenId");

                    b.HasIndex("PolicemanId");

                    b.ToTable("Violations", "Police");
                });

            modelBuilder.Entity("CertificatePupil", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Certificate", null)
                        .WithMany()
                        .HasForeignKey("CertificatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfStuff.Model.Pupil", null)
                        .WithMany()
                        .HasForeignKey("PupilsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CertificateStudent", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Certificate", null)
                        .WithMany()
                        .HasForeignKey("CertificatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfStuff.Model.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Bus", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.TripRoute", "RoutePlan")
                        .WithMany("Buses")
                        .HasForeignKey("RoutePlanId");

                    b.Navigation("RoutePlan");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Citizen", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Adress", "House")
                        .WithMany("Citizens")
                        .HasForeignKey("HouseId");

                    b.Navigation("House");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FireIncident", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Firemen.Fireman", null)
                        .WithMany("FireIncidents")
                        .HasForeignKey("FiremanId");

                    b.HasOne("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", "FiremanTeam")
                        .WithMany("FireIncidents")
                        .HasForeignKey("TeamId");

                    b.Navigation("FiremanTeam");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.Fireman", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Citizen", "Citizen")
                        .WithOne("Fireman")
                        .HasForeignKey("WebApplication1.EfStuff.Model.Firemen.Fireman", "CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", "FiremanTeam")
                        .WithMany("Firemen")
                        .HasForeignKey("FiremanTeamId");

                    b.Navigation("Citizen");

                    b.Navigation("FiremanTeam");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Firemen.FireTruck", "FireTruck")
                        .WithOne("FiremanTeam")
                        .HasForeignKey("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", "TruckId");

                    b.Navigation("FireTruck");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.PoliceAcademy", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Citizen", "Citizen")
                        .WithOne("PoliceAcademy")
                        .HasForeignKey("WebApplication1.EfStuff.Model.PoliceAcademy", "CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.PoliceCallHistory", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Citizen", "Citizen")
                        .WithMany("PoliceCallHistories")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfStuff.Model.Policeman", "Policeman")
                        .WithMany("PoliceCallHistories")
                        .HasForeignKey("PolicemanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");

                    b.Navigation("Policeman");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Policeman", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Citizen", "Citizen")
                        .WithOne("Policeman")
                        .HasForeignKey("WebApplication1.EfStuff.Model.Policeman", "CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Pupil", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.School", "School")
                        .WithMany("Pupils")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.SportSection", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.SportComplex", null)
                        .WithMany("Sections")
                        .HasForeignKey("SportComplexId");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Student", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.University", "University")
                        .WithMany("Students")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Violations", b =>
                {
                    b.HasOne("WebApplication1.EfStuff.Model.Citizen", "Citizen")
                        .WithMany("Violations")
                        .HasForeignKey("CitizenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfStuff.Model.Policeman", "Policeman")
                        .WithMany("Violations")
                        .HasForeignKey("PolicemanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");

                    b.Navigation("Policeman");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Adress", b =>
                {
                    b.Navigation("Citizens");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Citizen", b =>
                {
                    b.Navigation("Fireman");

                    b.Navigation("PoliceAcademy");

                    b.Navigation("PoliceCallHistories");

                    b.Navigation("Policeman");

                    b.Navigation("Violations");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FireTruck", b =>
                {
                    b.Navigation("FiremanTeam");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.Fireman", b =>
                {
                    b.Navigation("FireIncidents");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Firemen.FiremanTeam", b =>
                {
                    b.Navigation("FireIncidents");

                    b.Navigation("Firemen");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.Policeman", b =>
                {
                    b.Navigation("PoliceCallHistories");

                    b.Navigation("Violations");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.School", b =>
                {
                    b.Navigation("Pupils");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.SportComplex", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.TripRoute", b =>
                {
                    b.Navigation("Buses");
                });

            modelBuilder.Entity("WebApplication1.EfStuff.Model.University", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
