﻿// <auto-generated />
using System;
using LogisticsAssistant.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogisticsAssistant.Migrations
{
    [DbContext(typeof(LogisticsAssistantContext))]
    partial class LogisticsAssistantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LogisticsAssistant.Models.Lorries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("BreakAfterRideInHours")
                        .HasColumnType("float");

                    b.Property<int>("BreakInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("LorryBrand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MaxSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lorries");
                });

            modelBuilder.Entity("LogisticsAssistant.Models.ScheduledTrips", b =>
                {
                    b.Property<int>("ScheduledTripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduledTripId"), 1L, 1);

                    b.Property<DateTime>("CreationTripDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfArrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfDepartue")
                        .HasColumnType("datetime2");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<int>("LorryId")
                        .HasColumnType("int");

                    b.Property<string>("TripDescription")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.HasKey("ScheduledTripId");

                    b.HasIndex("LorryId");

                    b.ToTable("ScheduledTrips");
                });

            modelBuilder.Entity("LogisticsAssistant.Models.ScheduledTrips", b =>
                {
                    b.HasOne("LogisticsAssistant.Models.Lorries", "Lorry")
                        .WithMany()
                        .HasForeignKey("LorryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lorry");
                });
#pragma warning restore 612, 618
        }
    }
}
