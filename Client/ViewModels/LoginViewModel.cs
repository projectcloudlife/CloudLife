
using Client.Command.Attributes;
using Client.Interfaces;
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
        public LoginViewModel(INavigationService navService)
        {
            _navigationService = navService;
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
        void Login(object param)
        {
            new AuthInfo { Username = UserName, Password = Password };
            _navigationService.NavigateTo("FileViewerPage");
        }

        [CommandExecute]
        void Register(object param)
        {
            new AuthInfo { Username = UserName, Password = Password };
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
