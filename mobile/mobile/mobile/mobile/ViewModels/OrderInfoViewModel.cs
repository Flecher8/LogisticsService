using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class OrderInfoViewModel
    {
        public int OrderId { get; set; }

        public Command LoadItemCommand { get; set; }

        public OrderInfoViewModel(int orderId)
        {
            OrderId = orderId;
            LoadItemCommand = new Command(async () => await ExecuteLoadItemCommand());
        }

        async Task ExecuteLoadItemCommand()
        {
            Console.WriteLine("ExecuteLoadItemCommand");
            Console.WriteLine(OrderId.ToString());
        }


    }
}
