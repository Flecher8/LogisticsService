using mobile.Models.Enums;
using mobile.Models;
using mobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using mobile.Helpers;
using mobile.Views;
using System.Runtime.CompilerServices;

namespace mobile.ViewModels
{
    public class OrderInfoViewModel : PropertyChangedImplementator
    {
        public int OrderId { get; set; }

        private bool _isButtonActive;

        public bool IsButtonActive
        {
            get => _isButtonActive;
            set
            {
                _isButtonActive = value;
                OnPropertyChanged(nameof(IsButtonActive));
            }
        }

        public Command LoadItemCommand { get; set; }

        private string privateCompanyName;
        public string PrivateCompanyName
        {
            get { return privateCompanyName; }
            set
            {
                privateCompanyName = value;
                OnPropertyChanged();
            }
        }

        private string logisticCompanyName;
        public string LogisticCompanyName
        {
            get { return logisticCompanyName; }
            set
            {
                logisticCompanyName = value;
                OnPropertyChanged();
            }
        }

        private int sensorId;
        public int SensorId
        {
            get { return sensorId; }
            set
            {
                sensorId = value;
                OnPropertyChanged();
            }
        }

        private string cargoName;
        public string CargoName
        {
            get { return cargoName; }
            set
            {
                cargoName = value;
                OnPropertyChanged();
            }
        }

        private double cargoWeight;
        public double CargoWeight
        {
            get { return cargoWeight; }
            set
            {
                cargoWeight = value;
                OnPropertyChanged();
            }
        }

        private double cargoLength;
        public double CargoLength
        {
            get { return cargoLength; }
            set
            {
                cargoLength = value;
                OnPropertyChanged();
            }
        }

        private double cargoWidth;
        public double CargoWidth
        {
            get { return cargoWidth; }
            set
            {
                cargoWidth = value;
                OnPropertyChanged();
            }
        }

        private double cargoHeight;
        public double CargoHeight
        {
            get { return cargoHeight; }
            set
            {
                cargoHeight = value;
                OnPropertyChanged();
            }
        }

        private string cargoDescription;
        public string CargoDescription
        {
            get { return cargoDescription; }
            set
            {
                cargoDescription = value;
                OnPropertyChanged();
            }
        }


        private string startDeliveryAddress;
        public string StartDeliveryAddress
        {
            get { return startDeliveryAddress; }
            set
            {
                startDeliveryAddress = value;
                OnPropertyChanged();
            }
        }

        private string endDeliveryAddress;
        public string EndDeliveryAddress
        {
            get { return endDeliveryAddress; }
            set
            {
                endDeliveryAddress = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }


        public OrderInfoViewModel(int orderId, INavigation navigation)
        {
            OrderId = orderId;
            LoadItemCommand = new Command(async () => await ExecuteLoadItemCommand());

            Navigation = navigation;

            IsButtonActive = true;
        }

        async Task ExecuteLoadItemCommand()
        {
            try
            {
                OrdersService ordersService = new OrdersService();
                Order order = await ordersService.GetOrderById(OrderId);

                if (order.OrderStatus == OrderStatus.Cancelled)
                {
                    IsButtonActive = false;
                }

                SetOrderValues(order);

                GeolocationService geolocationService = new GeolocationService();

                StartDeliveryAddress = await geolocationService.GetOrderAddress(order.StartDeliveryAddress);
                EndDeliveryAddress = await geolocationService.GetOrderAddress(order.EndDeliveryAddress);

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                
            }
        }

        private void SetOrderValues(Order order)
        {
            PrivateCompanyName = order.PrivateCompany.CompanyName;
            LogisticCompanyName = order.LogisticCompany.CompanyName;
            SensorId = order.Sensor.SensorId;

            CargoName = order.Cargo.Name;

            CargoWeight = order.Cargo.Weight;

            CargoLength = order.Cargo.Length;
            CargoWidth = order.Cargo.Width;
            CargoHeight = order.Cargo.Height;

            CargoDescription = order.Cargo.Description;

        }

        public async Task CancelOrder()
        {
            
            await Navigation.PushAsync(new CancelOrderPage(OrderId));
        }
    }
}
