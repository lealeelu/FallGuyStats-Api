﻿// <auto-generated />
using System;
using FallGuyStats.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FallGuyStats.Migrations
{
    [DbContext(typeof(FallGuysContext))]
    [Migration("20200904162026_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            try
            {
                modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

                modelBuilder.Entity("FallGuyStats.Models.EpisodeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int>("Crowns")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EpisodeFinished")
                        .HasColumnType("TEXT");

                    b.Property<int>("Fame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Kudos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Season")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

                modelBuilder.Entity("FallGuyStats.Models.RoundModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Badge")
                        .HasColumnType("TEXT");

                    b.Property<int>("BonusFame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BonusKudos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BonusTier")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EpisodeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Kudos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Qualified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoundType")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rounds");
                });
            }
            catch
            {
                Console.WriteLine("reapplied Initial Migration");
            }
            
#pragma warning restore 612, 618
        }
    }
}
