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
    public class CargoRepository : IDataRepository<Cargo>
    {
        private readonly DataContext context;

        public CargoRepository(DataContext context)
        {
            this.context = context;
        }

        public Cargo? GetItemById(int itemId)
        {
            return context.Cargos
                .FirstOrDefault(s => s.CargoId == itemId);
        }

        public List<Cargo> GetFilteredItems(Expression<Func<Cargo, bool>> filter)
        {
            return context.Cargos
                .Where(filter)
                .ToList();
        }

        public void InsertItem(Cargo item)
        {
            context.Cargos.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(Cargo item)
        {
            Cargo? dbCargo = context.Cargos.Find(item.CargoId);

            if (dbCargo is not null)
            {
                dbCargo.Weight = item.Weight;
                dbCargo.Length = item.Length;
                dbCargo.Width = item.Width;
                dbCargo.Height = item.Height;
                dbCargo.Description = item.Description;

                context.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            Cargo? cargo = context.Cargos.Find(itemId);

            if (cargo is not null)
            {
                context.Cargos.Remove(cargo);
                context.SaveChanges();
            }
        }

        public List<Cargo> GetAllItems()
        {
            return context.Cargos.ToList();
        }
    }
}
