using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IEncryptionService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hashedPassword);
    }
}
