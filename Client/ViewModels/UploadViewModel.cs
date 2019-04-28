using Client.Command.Attributes;
using Client.Extensions;
using ClientLogic.Interfaces;
using ClientLogic.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Client.ViewModels
{
    public class UploadViewModel:ViewModel
    {
        public UploadViewModel(INavigationService navigationService, ILocalFileService localFileService,
            ICloudFileService cloudFileService, IMessagesService messagesService)
        {
            _navigationService = navigationService;
            _localFileService = localFileService;
            _cloudFileService = cloudFileService;
            _messagesService = messagesService;
        }

        INavigationService _navigationService;
        ICloudFileService _cloudFileService;
        ILocalFileService _localFileService;
        IMessagesService _messagesService;

        public ObservableCollection<FileClient> UploadFilesList { get; set; }


        public async void BrowseCommand()
        {
            var files = await _localFileService.SelectFiles();
            UploadFilesList = new ObservableCollection<FileClient>(files);
            Notify(nameof(UploadFilesList));
        }

        public async void UploadCommand()
        {
            if (UploadFilesList == null || UploadFilesList.Count == 0)
            {
                _messagesService.ShowMessage("Error", "Please select files to upload.");
            }
            else
            {
                foreach (var file in UploadFilesList)
                {
                    var fileToUpload = await _localFileService.GetFileWithData(file);
                    var res = await _cloudFileService.UploadFile(fileToUpload);
                }

                UploadFilesList.Clear();
                _navigationService.GoBack();
            }           
        }

        [CommandExecute]
        void BackCommand()
        {
            _navigationService.GoBack();
        }

        [CommandExecute]
        void NavCommand(NavigationViewItemInvokedEventArgs args)
        {
            MethodInfo mi = this.GetType().GetMethod($"{args.InvokedItemContainer.Name}Command");
            mi.Invoke(this, null);
        }
    }
}
