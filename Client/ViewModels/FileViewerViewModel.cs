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
            , ILocalFileService localFileService, IAuthService authService)
        {
            _navigationService = navService;
            _cloudFileService = cloudFileService;
            _localFileService = localFileService;
            _authService = authService;
            _cloudFileService.FileMetaDataChanged += OnFileMetaDataChanged;
        }

        private void OnFileMetaDataChanged(FileCommon changedFile)
        {
            var fileToChange = FilesList.First(file => file.Id == changedFile.Id);
            var index = FilesList.IndexOf(fileToChange);
            FilesList[index] = changedFile;
            FilesList.Add(new FileCommon());
            FilesList.RemoveAt(FilesList.Count - 1);
        }

        INavigationService _navigationService;
        ICloudFileService _cloudFileService;
        ILocalFileService _localFileService;
        IAuthService _authService;

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
            var path = await _localFileService.SelectFolder();

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
