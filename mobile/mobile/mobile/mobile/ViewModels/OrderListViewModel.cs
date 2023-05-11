using mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class OrderListViewModel
    {
        public ObservableCollection<Order> AcceptedOrders { get; set; }
        public Command LoadItemsCommand { get; set; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        public OrderListViewModel()
        {
            AcceptedOrders = new ObservableCollection<Order>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                // TODO: здесь отправляем GET-запрос на сервер и получаем список элементов

                AcceptedOrders.Clear();
                var apiItems = await ApiService.GetItems();
                //foreach (var item in apiItems)
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        Order selectedItem;
        public Order SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // TODO: здесь переходим на страницу детального просмотра выбранного элемента

            // Пример перехода на страницу детального просмотра выбранного элемента
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
