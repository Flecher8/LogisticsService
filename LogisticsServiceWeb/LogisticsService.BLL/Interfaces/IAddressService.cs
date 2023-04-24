using LogisticsService.Core.DbModels;
using LogisticsService.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Interfaces
{
    public interface IAddressService
    {
        Address? GetAddressById(int addressId);

        List<Address> GetAllAddresses();

        void CreateAddress(Address address);

        void UpdateAddress(Address address);

        void DeleteAddress(int addressId);
    }
}
