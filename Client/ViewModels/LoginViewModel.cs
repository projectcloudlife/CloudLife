
using Client.Command.Attributes;
using ClientLogic.Interfaces;
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
        public LoginViewModel(INavigationService navService, IAuthService authService
            , IMessagesService messagesService)
        {
            _navigationService = navService;
            _authService = authService;
            _messagesService = messagesService;
        }

        IAuthService _authService;
        INavigationService _navigationService;
        IMessagesService _messagesService;

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
            var info = new AuthInfo { Username = UserName, Password = Password };
            var response = await _authService.Login(info);

            switch (response.AuthResponse)
            {
                case AuthEnum.Success:
                    _navigationService.NavigateTo("FileViewerPage");
                    break;
                case AuthEnum.BadUsername:
                    _messagesService.ShowMessage("Login Faild!", "Username does not exist");
                    break;
                case AuthEnum.BadPassword:
                    _messagesService.ShowMessage("Login Faild", "Password incorrect");
                    break;
                default:
                    break;
            }
        }

        [CommandExecute]
        async void Register()
        {
            var info = new AuthInfo { Username = UserName, Password = Password };
            var response = await _authService.Register(info);
            switch (response)
            {
                case AuthEnum.Success:
                    _messagesService.ShowMessage("Registration Status"
                        , "You Have Been Successfully Registered");
                    break;
                case AuthEnum.BadUsername:
                    _messagesService.ShowMessage("Registration Status"
                    , "Username already exist.");
                    break;
                case AuthEnum.BadPassword:
                    _messagesService.ShowMessage("Registration Status"
                  , "Invalid password");
                    break;
                default:
                    break;
            }
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
