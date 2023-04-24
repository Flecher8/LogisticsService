﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Core.DbModels
{
    public class LogisticCompaniesDriver
    {
        public int LogisticCompaniesDriverId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public LogisticCompany LogisticCompany { get; set; } = null!;
        public List<Order>? Orders { get; set; } 
    }
}
