using mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class CancelOrderViewModel : PropertyChangedImplementator
    {
        public int OrderId { get; set; }

        public INavigation Navigation { get; set; }

        public CancelOrderViewModel(int orderId, INavigation navigation)
        {
            OrderId = orderId;
            Navigation = navigation;
        }
    }
}
