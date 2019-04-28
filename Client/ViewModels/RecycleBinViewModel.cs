using Client.Command.Attributes;
using ClientLogic.Interfaces;
using ClientLogic.Models;
using Common.Models;
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
    public class RecycleBinViewModel:ViewModel
    {
        INavigationService _navigationService;
        ICloudFileService _cloudFileService;
        ILocalFileService _localFileService;
        IMessagesService _messagesService;

        public ObservableCollection<FileCommon> RecycleBin { get; set; }
        public ObservableCollection<FileCommon> SelectedList { get; set; }


        public RecycleBinViewModel(INavigationService navigationService, ILocalFileService localFileService,
            ICloudFileService cloudFileService, IMessagesService messagesService)
        {
            _navigationService = navigationService;
            _localFileService = localFileService;
            _cloudFileService = cloudFileService;
            _messagesService = messagesService;
        }

        public async void InitFiles()
        {
            SelectedList = new ObservableCollection<FileCommon>();
            RecycleBin = new ObservableCollection<FileCommon>((await _cloudFileService.GetFiles())
                .Where(file => file.InRecycleBin == true));
            Notify(nameof(RecycleBin));
        }

        [CommandExecute]
        void BackCommand()
        {
            _navigationService.GoBack();
        }

        public void RecoverCommand()
        {

            if(SelectedList.Any() == false)
            {
                return;
            }

            foreach (var file in SelectedList)
            {
                file.InRecycleBin = false;
                _cloudFileService.UpdateFileMetadata(file);
            }

            UpdateRecycleBinAndCleanSelected();
        }

        public void DeleteCommand()
        {
            if(SelectedList.Any() == false)
            {
                return;
            }

            foreach (var file in SelectedList)
            {
                _cloudFileService.DeleteFile(file);
            }

            UpdateRecycleBinAndCleanSelected();
        }

        void UpdateRecycleBinAndCleanSelected()
        {
            var newRecycleBin = RecycleBin.Where(file => SelectedList.Any(sfile => sfile.Id == file.Id) == false);
            RecycleBin = new ObservableCollection<FileCommon>(newRecycleBin);
            Notify(nameof(RecycleBin));
            SelectedList.Clear();
        }

        [CommandExecute]
        void NavCommand(NavigationViewItemInvokedEventArgs args)
        {
            MethodInfo mi = this.GetType().GetMethod($"{args.InvokedItemContainer.Name}Command");
            mi.Invoke(this, null);
        }
    }
}
