﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data;

#nullable disable

namespace Server.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20220225170110_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("Shared.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCreated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StopTime")
                        .HasColumnType("TEXT");

                    b.HasKey("BookingId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            Cost = 88.0,
                            IsComplete = true,
                            IsCreated = false,
                            StartTime = "25/02/2022 05:01"
                        },
                        new
                        {
                            BookingId = 2,
                            Cost = 98.0,
                            IsComplete = true,
                            IsCreated = false,
                            StartTime = "25/02/2022 05:01"
                        });
                });

            modelBuilder.Entity("Shared.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("PricePerUnit")
                        .HasColumnType("REAL");

                    b.Property<double>("Range")
                        .HasColumnType("REAL");

                    b.Property<double>("RangeLeft")
                        .HasColumnType("REAL");

                    b.Property<bool>("isLocked")
                        .HasColumnType("INTEGER");

                    b.HasKey("CarId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            CarId = 1,
                            Active = false,
                            CategoryId = 1,
                            Make = "Tesla",
                            Model = "Model X",
                            PricePerUnit = 7.0,
                            Range = 250.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        },
                        new
                        {
                            CarId = 2,
                            Active = false,
                            CategoryId = 2,
                            Make = "Tesla",
                            Model = "Model S",
                            PricePerUnit = 7.0,
                            Range = 200.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        },
                        new
                        {
                            CarId = 3,
                            Active = false,
                            CategoryId = 3,
                            Make = "Porsche",
                            Model = "Taycan",
                            PricePerUnit = 7.0,
                            Range = 270.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        },
                        new
                        {
                            CarId = 4,
                            Active = false,
                            CategoryId = 1,
                            Make = "Nissan",
                            Model = "Leaf",
                            PricePerUnit = 7.0,
                            Range = 150.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        },
                        new
                        {
                            CarId = 5,
                            Active = false,
                            CategoryId = 2,
                            Make = "Honda",
                            Model = "Up!",
                            PricePerUnit = 7.0,
                            Range = 220.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        },
                        new
                        {
                            CarId = 6,
                            Active = false,
                            CategoryId = 3,
                            Make = "Toyota",
                            Model = "GT",
                            PricePerUnit = 7.0,
                            Range = 200.0,
                            RangeLeft = 100.0,
                            isLocked = true
                        });
                });

            modelBuilder.Entity("Shared.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Description 1",
                            Name = "Category 1"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Description 2",
                            Name = "Category 2"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Description 3",
                            Name = "Category 3"
                        });
                });

            modelBuilder.Entity("Shared.Models.License", b =>
                {
                    b.Property<int>("LicenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("YearsHeld")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("dob")
                        .HasColumnType("TEXT");

                    b.HasKey("LicenseId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("Shared.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EmailAddress = "Email: 1",
                            Password = "Description 1"
                        },
                        new
                        {
                            UserId = 2,
                            EmailAddress = "Email: 2",
                            Password = "Description 2"
                        },
                        new
                        {
                            UserId = 3,
                            EmailAddress = "Email: 3",
                            Password = "Description 3"
                        });
                });

            modelBuilder.Entity("Shared.Models.Car", b =>
                {
                    b.HasOne("Shared.Models.Category", "Category")
                        .WithMany("Cars")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Shared.Models.Category", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}