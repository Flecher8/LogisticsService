﻿// <auto-generated />
using System;
using LogisticsService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230326110041_AddLogisticCompaniesAdministratorsTable")]
    partial class AddLogisticCompaniesAdministratorsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesAdministrators", b =>
                {
                    b.Property<int>("LogisticCompaniesAdministratorsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompaniesAdministratorsId"), 1L, 1);

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

                    b.HasKey("LogisticCompaniesAdministratorsId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("LogisticCompaniesAdministrators");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompany", b =>
                {
                    b.Property<int>("LogisticCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompanyId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogisticCompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogisticCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogisticCompanyId");

                    b.ToTable("LogisticCompanies");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.PrivateCompany", b =>
                {
                    b.Property<int>("PrivateCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateCompanyId"), 1L, 1);

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
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

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriptionTypeId");

                    b.ToTable("SubscriptionTypes");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompaniesAdministrators", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany("LogisticCompaniesAdministrators")
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");
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

            modelBuilder.Entity("LogisticsService.Core.DbModels.SmartDevice", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany()
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

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompany", b =>
                {
                    b.Navigation("LogisticCompaniesAdministrators");
                });
#pragma warning restore 612, 618
        }
    }
}
