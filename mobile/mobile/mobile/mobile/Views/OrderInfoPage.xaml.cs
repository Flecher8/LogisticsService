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
    public partial class OrderInfoPage : ContentPage
    {
        public OrderInfoViewModel ViewModel { get; set; }
        public OrderInfoPage(int orderId)
        {
            InitializeComponent();
            ViewModel = new OrderInfoViewModel(orderId);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadItemCommand.Execute(null);
            base.OnAppearing();
        }
    }
}