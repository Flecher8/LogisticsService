﻿// <auto-generated />
using System;
using LogisticsService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LogisticsService.Core.DbModels.CancelledOrder", b =>
                {
                    b.Property<int>("CancelledOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CancelledOrderId"), 1L, 1);

                    b.Property<int>("CancelledBy")
                        .HasColumnType("int");

                    b.Property<int>("CancelledById")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CancelledOrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("CancelledOrders");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Cargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CargoId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("CargoId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesAdministrator", b =>
                {
                    b.Property<int>("LogisticCompaniesAdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompaniesAdministratorId"), 1L, 1);

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.HasKey("LogisticCompaniesAdministratorId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("LogisticCompaniesAdministrators");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesDriver", b =>
                {
                    b.Property<int>("LogisticCompaniesDriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompaniesDriverId"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.HasKey("LogisticCompaniesDriverId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("LogisticCompaniesDrivers");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompany", b =>
                {
                    b.Property<int>("LogisticCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompanyId"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogisticCompanyId");

                    b.ToTable("LogisticCompanies");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int?>("CargoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryEndAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryStartAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EstimatedDeliveryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LogisticCompaniesDriverId")
                        .HasColumnType("int");

                    b.Property<int?>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<double>("PathLength")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("PrivateCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("SensorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDeliveryDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CargoId");

                    b.HasIndex("LogisticCompaniesDriverId");

                    b.HasIndex("LogisticCompanyId");

                    b.HasIndex("PrivateCompanyId");

                    b.HasIndex("SensorId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.OrderTracker", b =>
                {
                    b.Property<int>("OrderTrackerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderTrackerId"), 1L, 1);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderTrackerId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderTrackers");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.PrivateCompany", b =>
                {
                    b.Property<int>("PrivateCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateCompanyId"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrivateCompanyId");

                    b.ToTable("PrivateCompanies");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Rate", b =>
                {
                    b.Property<int>("RateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RateId"), 1L, 1);

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("PriceForKmInDollar")
                        .HasColumnType("int");

                    b.HasKey("RateId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Sensor", b =>
                {
                    b.Property<int>("SensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SensorId"), 1L, 1);

                    b.Property<int>("SmartDeviceId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("SensorId");

                    b.HasIndex("SmartDeviceId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SmartDevice", b =>
                {
                    b.Property<int>("SmartDeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SmartDeviceId"), 1L, 1);

                    b.Property<int?>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSensors")
                        .HasColumnType("int");

                    b.HasKey("SmartDeviceId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("SmartDevices");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"), 1L, 1);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionStatus")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionTypeId")
                        .HasColumnType("int");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("LogisticCompanyId");

                    b.HasIndex("SubscriptionTypeId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SubscriptionType", b =>
                {
                    b.Property<int>("SubscriptionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionTypeId"), 1L, 1);

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SubscriptionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriptionTypeId");

                    b.ToTable("SubscriptionTypes");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SystemAdmin", b =>
                {
                    b.Property<int>("SystemAdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SystemAdminId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SystemAdminId");

                    b.ToTable("SystemAdmins");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<double>("CommissionPercent")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("EarnedAmount")
                        .HasColumnType("float");

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PrivateCompanyId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("LogisticCompanyId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PrivateCompanyId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.CancelledOrder", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesAdministrator", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("LogisticCompaniesAdministrators")
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesDriver", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("LogisticCompaniesDrivers")
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Order", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId");

                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompaniesDriver", "LogisticCompaniesDriver")
                        .WithMany("Orders")
                        .HasForeignKey("LogisticCompaniesDriverId");

                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("Orders")
                        .HasForeignKey("LogisticCompanyId");

                    b.HasOne("LogisticsService.Core.DbModels.PrivateCompany", "PrivateCompany")
                        .WithMany("Orders")
                        .HasForeignKey("PrivateCompanyId");

                    b.HasOne("LogisticsService.Core.DbModels.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId");

                    b.Navigation("Cargo");

                    b.Navigation("LogisticCompaniesDriver");

                    b.Navigation("LogisticCompany");

                    b.Navigation("PrivateCompany");

                    b.Navigation("Sensor");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.OrderTracker", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Rate", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany()
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Sensor", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.SmartDevice", "SmartDevice")
                        .WithMany("Sensors")
                        .HasForeignKey("SmartDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SmartDevice");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SmartDevice", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("SmartDevices")
                        .HasForeignKey("LogisticCompanyId");

                    b.Navigation("LogisticCompany");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Subscription", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany()
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsService.Core.DbModels.SubscriptionType", "SubscriptionType")
                        .WithMany()
                        .HasForeignKey("SubscriptionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");

                    b.Navigation("SubscriptionType");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.Transaction", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("Transactions")
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsService.Core.DbModels.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticsService.Core.DbModels.PrivateCompany", "PrivateCompany")
                        .WithMany("Transactions")
                        .HasForeignKey("PrivateCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");

                    b.Navigation("Order");

                    b.Navigation("PrivateCompany");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesDriver", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompany", b =>
                {
                    b.Navigation("LogisticCompaniesAdministrators");

                    b.Navigation("LogisticCompaniesDrivers");

                    b.Navigation("Orders");

                    b.Navigation("SmartDevices");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.PrivateCompany", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SmartDevice", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
