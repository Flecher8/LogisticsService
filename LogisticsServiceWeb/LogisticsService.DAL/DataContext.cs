using LogisticsService.Core.DbModels;
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
        public DbSet<PrivateCompany> PrivateCompanies { get; set; } = null!;
        public DbSet<LogisticCompany> LogisticCompanies { get; set; } = null!;
        public DbSet<Rate> Rates { get; set; } = null!;
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; } = null!;
        public DbSet<SubscriptionStatus> SubscriptionStatuses { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<SmartDevice> SmartDevices { get; set; } = null!;


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
