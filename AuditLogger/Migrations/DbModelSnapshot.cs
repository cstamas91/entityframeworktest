﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwoWayRelation.Data;

namespace AuditLogger.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwoWayRelation.Data.Models.AuditLogRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attribute")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RegistrationId")
                        .HasColumnType("int");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("AuditLogRecord");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.PaymentInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PaymentInfo");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.Preferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("NSFWContent")
                        .HasColumnType("bit");

                    b.Property<bool>("RecieveLocalNewsAlerts")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.RegionPreference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PreferencesId")
                        .HasColumnType("int");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PreferencesId");

                    b.ToTable("RegionPreferences");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentInfoId")
                        .HasColumnType("int");

                    b.Property<int?>("PreferencesId")
                        .HasColumnType("int");

                    b.Property<int?>("SettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentInfoId")
                        .IsUnique()
                        .HasFilter("[PaymentInfoId] IS NOT NULL");

                    b.HasIndex("PreferencesId");

                    b.HasIndex("SettingsId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("HideMenu")
                        .HasColumnType("bit");

                    b.Property<int>("Theme")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.AuditLogRecord", b =>
                {
                    b.HasOne("TwoWayRelation.Data.Models.Registration", null)
                        .WithMany("AuditLogRecords")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.RegionPreference", b =>
                {
                    b.HasOne("TwoWayRelation.Data.Models.Preferences", "Preferences")
                        .WithMany("RegionPreferences")
                        .HasForeignKey("PreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TwoWayRelation.Data.Models.Registration", b =>
                {
                    b.HasOne("TwoWayRelation.Data.Models.PaymentInfo", "PaymentInfo")
                        .WithOne("Registration")
                        .HasForeignKey("TwoWayRelation.Data.Models.Registration", "PaymentInfoId");

                    b.HasOne("TwoWayRelation.Data.Models.Preferences", "Preferences")
                        .WithMany()
                        .HasForeignKey("PreferencesId");

                    b.HasOne("TwoWayRelation.Data.Models.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");
                });
#pragma warning restore 612, 618
        }
    }
}
