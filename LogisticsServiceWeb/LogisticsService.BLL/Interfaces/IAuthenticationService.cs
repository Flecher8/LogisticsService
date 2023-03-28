using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        public string Login(string email, string password, string userType);

        public bool Registration(string email, string password, string companyName, string userType);
    }
}
