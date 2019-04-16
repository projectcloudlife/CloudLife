
using Client.Command.Attributes;
using Client.Interfaces;
using Common.Enums;
using Common.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        public LoginViewModel(INavigationService navService, IAuthService authService)
        {
            _navigationService = navService;
            _authService = authService;
        }

        IAuthService _authService;
        INavigationService _navigationService;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Notify(nameof(UserName)); _userName = value; }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { Notify(nameof(Password)); _password = value; }
        }

        [CommandExecute]
        async void Login()
        {
            //var info = new AuthInfo { Username = UserName, Password = Password };
            //var response = await _authService.Login(info);
            //if (response.AuthResponse == AuthEnum.Success)
                _navigationService.NavigateTo("FileViewerPage");
            
        }

        [CommandExecute]
        async void Register()
        {
            var info = new AuthInfo { Username = UserName, Password = Password };
            var response = await _authService.Register(info);
        }

        [CommandCanExecute]
        bool CanClickMe()
        {
            if(Password.Length > 5)
            {
                return true;
            }
            return false;
        }

    }
}
