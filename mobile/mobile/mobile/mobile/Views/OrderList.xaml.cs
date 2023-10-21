using mobile.Models;
using mobile.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderList : ContentPage
    {
        public OrderListViewModel ViewModel { get; set; }
        public OrderList()
        {
            InitializeComponent();
            ViewModel = new OrderListViewModel(Navigation);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {

            ViewModel.LoadItemsCommand.Execute(null);
            base.OnAppearing();
        }


        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var order = e.SelectedItem as Order;
            ViewModel.OnItemSelected(order.OrderId);
        }
    }
}
