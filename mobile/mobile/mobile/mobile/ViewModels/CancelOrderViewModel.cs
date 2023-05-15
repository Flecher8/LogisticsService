using mobile.Helpers;
using mobile.Models;
using mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class CancelOrderViewModel : PropertyChangedImplementator
    {
        public int OrderId { get; set; }

        public INavigation Navigation { get; set; }

        public Action DisplayCancelErorr = null;

        public Action DisplayCancelSuccess = null;

        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public CancelOrderViewModel(int orderId, INavigation navigation)
        {
            OrderId = orderId;
            Navigation = navigation;
        }

        public async Task CancelOrder()
        {
            CancelledOrder cancelledOrder = new CancelledOrder();
            cancelledOrder.OrderId = OrderId;
            cancelledOrder.Reason = Reason;
            cancelledOrder.Description = Description;

            OrdersService ordersService = new OrdersService();
            bool status = await ordersService.CancelOrder(cancelledOrder);

            if(status)
            {
                DisplayCancelSuccess();
                await Navigation.PopAsync();
                await Navigation.PopAsync();
                return;
            }

            DisplayCancelErorr();
        }

    }
}
