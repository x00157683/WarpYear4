﻿// <auto-generated />
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

            modelBuilder.Entity("Shared.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Range")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Make = "Tesla",
                            Model = "Model X",
                            Range = 0.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Make = "Tesla",
                            Model = "Model S",
                            Range = 0.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Make = "Porsche",
                            Model = "Taycan",
                            Range = 0.0
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Make = "Nissan",
                            Model = "Leaf",
                            Range = 0.0
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Make = "Honda",
                            Model = "Up!",
                            Range = 0.0
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            Make = "Toyota",
                            Model = "GT",
                            Range = 0.0
                        });
                });

            modelBuilder.Entity("Shared.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Description = "Description 1",
                            Name = "Category 1"
                        },
                        new
                        {
                            CategoryID = 2,
                            Description = "Description 2",
                            Name = "Category 2"
                        },
                        new
                        {
                            CategoryID = 3,
                            Description = "Description 3",
                            Name = "Category 3"
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
