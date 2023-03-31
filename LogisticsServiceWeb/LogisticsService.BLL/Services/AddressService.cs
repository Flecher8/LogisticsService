using LogisticsService.BLL.Interfaces;
using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using LogisticsService.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.BLL.Services
{
    public class AddressService : IAddressService
    {
        private readonly IDataRepository<Address> _addressRepository;

        private readonly ILogger<AddressService> _logger;

        public AddressService(IDataRepository<Address> addressRepository, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public void CreateAddress(Address address)
        {
            try
            {
                _addressRepository.InsertItem(address);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void DeleteAddress(int addressId)
        {
            try
            {
                _addressRepository.DeleteItem(addressId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public Address? GetAddressnById(int addressId)
        {
            try
            {
                Address? address = _addressRepository.GetItemById(addressId);
                return address;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public List<Address> GetAllAddress()
        {
            var addresses = new List<Address>();
            try
            {
                addresses = _addressRepository.GetAllItems();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return addresses;
        }

        public void UpdateAddress(Address address)
        {
            try
            {
                _addressRepository.UpdateItem(address);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
