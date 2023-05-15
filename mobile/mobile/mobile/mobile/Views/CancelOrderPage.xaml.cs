using mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelOrderPage : ContentPage
    {
        public CancelOrderViewModel ViewModel { get; set; }

        public CancelOrderPage(int orderId)
        {
            InitializeComponent();
            ViewModel = new CancelOrderViewModel(orderId, Navigation);
            BindingContext = ViewModel;

            ViewModel.DisplayCancelErorr = () =>
            {
                DisplayAlert("Cancel order error", "Can't cancel order", "OK");
            };

            ViewModel.DisplayCancelSuccess = () =>
            {
                DisplayAlert("Success", "", "OK");
            };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.CancelOrder();
        }
    }
}