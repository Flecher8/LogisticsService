using LogisticsService.Core.DbModels;
using LogisticsService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IUserService
    {
        public bool IsEmailAlreadyRegistered(string email);

        public UserType GetUserTypeByEmail(string email);

        public string GetUserHashedPassword(string email, UserType userType);
    }
}
