using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<PrivateCompany> PrivateCompanies { get; set; }
        public DbSet<LogisticCompany> LogisticCompanies { get; set; }
        public DbSet<LogisticCompaniesAdministrator> LogisticCompaniesAdministrators { get; set; }
        public DbSet<LogisticCompaniesDriver> LogisticCompaniesDrivers { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SmartDevice> SmartDevices { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CancelledOrder> CancelledOrders { get; set; }
        public DbSet<OrderTracker> OrderTrackers { get; set; }
        public DbSet <Transaction> Transactions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
        public DbSet<SystemAdmin> SystemAdmins { get; set; }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=Desktop;Initial Catalog=DBLogisticsService;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
