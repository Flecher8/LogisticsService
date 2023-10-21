using LogisticsService.Core.DbModels;
using LogisticsService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Repositories
{
    public class AddressRepository : IDataRepository<Address>
    {
        private readonly DataContext context;

        public AddressRepository(DataContext context)
        {
            this.context = context;
        }

        public void DeleteItem(int itemId)
        {
            Address? address = context.Addresses.Find(itemId);

            if (address is not null)
            {
                context.Addresses.Remove(address);
                context.SaveChanges();
            }
        }

        public List<Address> GetAllItems()
        {
            return context.Addresses.ToList();
        }

        public List<Address> GetFilteredItems(Expression<Func<Address, bool>> filter)
        {
            return context.Addresses
               .Where(filter)
               .ToList();
        }

        public Address? GetItemById(int itemId)
        {
            return context.Addresses
                .FirstOrDefault(s => s.AddressId == itemId);
        }

        public int InsertItem(Address item)
        {
            context.Addresses.Add(item);
            context.SaveChanges();
            int createdItemId = item.AddressId;
            return createdItemId;
        }

        public void UpdateItem(Address item)
        {
            Address? dbAddress = context.Addresses.Find(item.AddressId);

            if (dbAddress is not null)
            {
                dbAddress.AddressName = item.AddressName;
                dbAddress.Latitude = item.Latitude;
                dbAddress.Longitute = item.Longitute;
                
                context.SaveChanges();
            }
        }
    }
}
