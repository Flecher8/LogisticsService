using mobile.Helpers;
using mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class LoginViewModel : PropertyChangedImplementator
    {
        private AccountService accountService;

        public Action DisplayInvalidLoginErorr = null;

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            accountService = new AccountService();

            LoginCommand = new Command(LoginAsync);
        }

        public async void LoginAsync()
        {
            if (!await accountService.LoginAsync(Email, Password))
            {
                DisplayInvalidLoginErorr();
                return;
            }
            //PlacementService = new PlacementService();
            //App.Current.MainPage = new NavigationPage(new PlacementsPage(await PlacementService.GetUserPlacements()));
        }
    }
}
