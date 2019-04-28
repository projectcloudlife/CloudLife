using Client.Command.Attributes;
using Client.Extensions;
using ClientLogic.Interfaces;
using ClientLogic.Models;
using Common.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Client.ViewModels
{
    public class FileViewerViewModel : ViewModel
    {
        public FileViewerViewModel(INavigationService navService, ICloudFileService cloudFileService
            , ILocalFileService localFileService, IAuthService authService, IMessagesService messagesService)
        {
            _navigationService = navService;
            _cloudFileService = cloudFileService;
            _localFileService = localFileService;
            _authService = authService;
            _messagesService = messagesService;
            _cloudFileService.FileMetaDataChanged += OnFileMetaDataChanged;
        }

        private void OnFileMetaDataChanged(FileCommon changedFile)
        {
            var list = FilesList.Select(file =>
            {
                if (file.Id == changedFile.Id)
                {
                    return changedFile;
                }
                return file;
            });
            FilesList = new ObservableCollection<FileCommon>(list);
            Notify(nameof(FilesList));
        }

        INavigationService _navigationService;
        ICloudFileService _cloudFileService;
        ILocalFileService _localFileService;
        IAuthService _authService;
        IMessagesService _messagesService;

        public ObservableCollection<FileCommon> FilesList { get; set; }
        public ObservableCollection<FileCommon> SelectedList { get; set; }


        public async void InitFiles()
        {
            SelectedList = new ObservableCollection<FileCommon>();
            FilesList = new ObservableCollection<FileCommon>(await _cloudFileService.GetFiles());
            Notify(nameof(FilesList));
        }

        public void LogOutCommand()
        {
            _authService.Logout();

            _navigationService.GoBack();
        }

        public void UploadCommand()
        {
            _navigationService.NavigateTo("UploadPage");
        }

        public async void DownloadCommand()
        {

            if (!SelectedList.Any())
            {
                _messagesService.ShowMessage("Select file/s to donwload", "");
                return;
            }

            string path = await _localFileService.SelectFolder();

            if(string.IsNullOrEmpty(path))
            {
                return;
            }

            foreach (var file in SelectedList)
            {
                var downloadedFile = await _cloudFileService.DownloadFile(file);
                var fileClient = downloadedFile.ToClient();
                fileClient.Path = path;
                await _localFileService.SaveFile(fileClient);
            }

        }

        public void ShareCommand()
        {
            foreach (var file in SelectedList)
            {
                file.IsPublic = true;
                _cloudFileService.UpdateFileMetadata(file);
            }
        }

        [CommandExecute]
        void NavCommand(NavigationViewItemInvokedEventArgs args)
        {
            MethodInfo mi = this.GetType().GetMethod($"{args.InvokedItemContainer.Name}Command");
            mi.Invoke(this, null);
        }
    }

}
