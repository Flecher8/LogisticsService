﻿// <auto-generated />
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
    [Migration("20230324175720_AddSubscriptionStatusesTable")]
    partial class AddSubscriptionStatusesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LogisticsService.Core.DbModels.LogisticCompany", b =>
                {
                    b.Property<int>("LogisticCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogisticCompanyId"));

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrivateCompanyId"));

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RateId"));

                    b.Property<int>("LogisticCompanyId")
                        .HasColumnType("int");

                    b.Property<int>("PriceForKmInDollar")
                        .HasColumnType("int");

                    b.HasKey("RateId");

                    b.HasIndex("LogisticCompanyId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SubscriptionStatus", b =>
                {
                    b.Property<int>("SubscriptionStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionStatusId"));

                    b.Property<string>("SubscriptionStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriptionStatusId");

                    b.ToTable("SubscriptionStatuses");
                });

            modelBuilder.Entity("LogisticsService.Core.DbModels.SubscriptionType", b =>
                {
                    b.Property<int>("SubscriptionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionTypeId"));

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

            modelBuilder.Entity("LogisticsService.Core.DbModels.Rate", b =>
                {
                    b.HasOne("LogisticsService.Core.DbModels.LogisticCompany", "LogisticCompany")
                        .WithMany()
                        .HasForeignKey("LogisticCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticCompany");
                });
#pragma warning restore 612, 618
        }
    }
}
