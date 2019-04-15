using Client.Command.Attributes;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class FileViewerViewModel:ViewModel
    {
        public FileViewerViewModel(INavigationService navService)
        {
            _navigationService = navService;
        }

        INavigationService _navigationService;

        [CommandExecute]
        void GoBack()
        {
            _navigationService.GoBack();
        }

        [CommandExecute]
        void UploadNavigate()
        {
            _navigationService.NavigateTo("UploadPage");
        }

        [CommandExecute]
        void TestClick()
        {

        }
    }

}
