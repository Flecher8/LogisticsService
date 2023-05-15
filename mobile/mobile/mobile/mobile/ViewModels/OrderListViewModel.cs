using mobile.Models;
using mobile.Models.Enums;
using mobile.Services;
using mobile.Views;
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
        private List<Order> Orders { get; set; }
        public ObservableCollection<Order> AcceptedOrders { get; set; }
        public ObservableCollection<Order> InTransitOrders { get; set; }
        public Command LoadItemsCommand { get; set; }

        public INavigation Navigation { get; set; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        public OrderListViewModel(INavigation navigation)
        {
            Orders = new List<Order>();
            AcceptedOrders = new ObservableCollection<Order>();
            InTransitOrders = new ObservableCollection<Order>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            Navigation = navigation;
        }



        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Orders.Clear();
                AcceptedOrders.Clear();
                InTransitOrders.Clear();
                OrdersService ordersService = new OrdersService();
                List<Order> apiItems = await ordersService.GetLogisticCompaniesDriverOrders();

                foreach (var item in apiItems)
                {
                    Orders.Add(item);
                }

                List<Order> acceptedOrders = ordersService.GetOrdersByOrderStatus(Orders, OrderStatus.OrderAccepted);
                SetAcceptedOrders(acceptedOrders);

                List<Order> inTransitOrders = ordersService.GetOrdersByOrderStatus(Orders, OrderStatus.InTransit);
                SetInTransitOrders(inTransitOrders);
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

        private void SetAcceptedOrders(List<Order> acceptedOrders)
        {
            foreach (var item in acceptedOrders)
            {
                AcceptedOrders.Add(item);
            }
        }

        private void SetInTransitOrders(List<Order> inTransitOrders)
        {
            foreach (var item in inTransitOrders)
            {
                InTransitOrders.Add(item);
            }
        }

        Order selectedItem;
        public Order SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
            }
        }

        public async void OnItemSelected(int itemId)
        {
            if (itemId == 0)
                return;
            Console.WriteLine("Item id: " + itemId);
            await Navigation.PushAsync(new OrderInfoPage(itemId));
        }
    }
}
