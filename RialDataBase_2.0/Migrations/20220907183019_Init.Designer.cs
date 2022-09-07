﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RialDataBase_2._0.EntityClasses.BaseConnectClass;

#nullable disable

namespace RialDataBase_2._0.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220907183019_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Cyrillic_General_CI_AS")
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CarName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("VIN");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Car", (string)null);
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.CarCharacteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AirFilter")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("FuelFilter")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Oil")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OilFilter")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PadsFront")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PadsRear")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SalonFilter")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Сandles")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CarId" }, "UQ__CarChara__68A0342F0DF02FB1")
                        .IsUnique()
                        .HasFilter("[CarId] IS NOT NULL");

                    b.ToTable("CarCharacteristics");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Fname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<byte?>("StatusId")
                        .HasColumnType("tinyint")
                        .HasColumnName("StatusID");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex(new[] { "Phone" }, "UQ__Client__5C7E359E1541BBAE")
                        .IsUnique();

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.ClientBankAccout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("CashBack")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPurchaseAmount")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ClientId" }, "UQ__ClientBa__E67E1A25C67D7BCE")
                        .IsUnique()
                        .HasFilter("[ClientId] IS NOT NULL");

                    b.ToTable("ClientBankAccout", (string)null);
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.ClientStatus", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"), 1L, 1);

                    b.Property<string>("Status")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("ClientStatus", (string)null);
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Car", b =>
                {
                    b.HasOne("RialDataBase_2._0.EntityClasses.Objects.Client", "Client")
                        .WithMany("Cars")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Car__ClientId__0E04126B");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.CarCharacteristic", b =>
                {
                    b.HasOne("RialDataBase_2._0.EntityClasses.Objects.Car", "Car")
                        .WithOne("CarCharacteristic")
                        .HasForeignKey("RialDataBase_2._0.EntityClasses.Objects.CarCharacteristic", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__CarCharac__CarId__10E07F16");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Client", b =>
                {
                    b.HasOne("RialDataBase_2._0.EntityClasses.Objects.ClientStatus", "Status")
                        .WithMany("Clients")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Client__StatusID__7EC1CEDB");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.ClientBankAccout", b =>
                {
                    b.HasOne("RialDataBase_2._0.EntityClasses.Objects.Client", "Client")
                        .WithOne("ClientBankAccout")
                        .HasForeignKey("RialDataBase_2._0.EntityClasses.Objects.ClientBankAccout", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ClientBan__Clien__02925FBF");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Car", b =>
                {
                    b.Navigation("CarCharacteristic");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.Client", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("ClientBankAccout");
                });

            modelBuilder.Entity("RialDataBase_2._0.EntityClasses.Objects.ClientStatus", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
