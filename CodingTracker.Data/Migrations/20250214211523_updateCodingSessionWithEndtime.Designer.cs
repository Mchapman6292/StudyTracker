﻿// <auto-generated />
using System;
using CodingTracker.Data.DbContextService.CodingTrackerDbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodingTracker.Data.Migrations
{
    [DbContext(typeof(CodingTrackerDbContext))]
    [Migration("20250214211523_updateCodingSessionWithEndtime")]
    partial class updateCodingSessionWithEndtime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CodingTracker.Common.Entities.CodingSessionEntities.CodingSessionEntity", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SessionId"));

                    b.Property<string>("DurationHHMM")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("DurationSeconds")
                        .HasColumnType("double precision");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GoalMinutes")
                        .HasColumnType("integer");

                    b.Property<bool>("GoalReached")
                        .HasColumnType("boolean");

                    b.Property<bool>("GoalSet")
                        .HasColumnType("boolean");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("SessionId");

                    b.ToTable("CodingSessions");
                });

            modelBuilder.Entity("CodingTracker.Common.Entities.UserCredentialEntities.UserCredentialEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("UserCredentials");
                });
#pragma warning restore 612, 618
        }
    }
}
