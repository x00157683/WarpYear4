﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data;

#nullable disable

namespace Server.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Shared.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

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
                            StartTime = "26/03/2022 08:59"
                        },
                        new
                        {
                            BookingId = 2,
                            Cost = 98.0,
                            IsComplete = true,
                            IsCreated = false,
                            StartTime = "26/03/2022 08:59"
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
