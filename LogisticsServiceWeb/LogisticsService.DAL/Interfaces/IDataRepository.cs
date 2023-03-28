using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.DAL.Interfaces
{
    public interface IDataRepository<T>
    {
        List<T> GetAllItems();

        List<T> GetFilteredItems(Expression<Func<T, bool>> filter);

        T? GetItemById(int itemId);

        void InsertItem(T item);

        void DeleteItem(int itemId);

        void UpdateItem(T item);

    }
}
