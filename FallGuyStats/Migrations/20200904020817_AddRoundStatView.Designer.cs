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
    [Migration("20200904020817_AddRoundStatView")]
    partial class AddRoundStatView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
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

            modelBuilder.Entity("FallGuyStats.Objects.Models.Views.RoundStatsView", b =>
                {
                    b.Property<string>("RoundType")
                        .HasColumnType("TEXT");

                    b.Property<int>("BronzeCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GoldCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NotQualifiedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QualifiedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SilverCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoundType");

                    b.ToTable("vRoundStats");
                });

            modelBuilder.Entity("FallGuyStats.Objects.Models.Views.SeasonStatsView", b =>
                {
                    b.Property<int>("Season")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CheaterCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CrownCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoundsSinceCrown")
                        .HasColumnType("INTEGER");

                    b.HasKey("Season");

                    b.ToTable("vSeasonStats");
                });

            modelBuilder.Entity("FallGuyStats.Objects.Models.Views.TodayStatsView", b =>
                {
                    b.Property<DateTime>("EpisodeFinishedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CrownCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Season")
                        .HasColumnType("INTEGER");

                    b.HasKey("EpisodeFinishedDate");

                    b.ToTable("vTodayStats");
                });
#pragma warning restore 612, 618
        }
    }
}