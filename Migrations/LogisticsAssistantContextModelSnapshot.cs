﻿// <auto-generated />
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

                    b.Property<int>("BreakInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("LorryBrand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxSpeed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lorries");
                });
#pragma warning restore 612, 618
        }
    }
}