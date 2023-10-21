using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.Models
{
    public class LogisticCompaniesDriver
    {
        public int LogisticCompaniesDriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
