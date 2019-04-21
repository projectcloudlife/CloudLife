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
    public class FileViewerViewModel:ViewModel
    {
        public FileViewerViewModel(INavigationService navService, ICloudFileService cloudFileService
            ,ILocalFileService localFileService, IAuthService authService)
        {
            _navigationService = navService;
            _cloudFileService = cloudFileService;
            _localFileService = localFileService;
            _authService = authService;
            InitFiles();
            
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
            //FilesList = new ObservableCollection<FileCommon>(await _cloudFileService.GetFiles(true));

            FilesList = new ObservableCollection<FileCommon>
            {
                new FileCommon{Name="fff.jpg",
                SizeInBytes=2343,
                IsPublic=true,
                UploadDate = DateTime.UtcNow

                },
                new FileCommon{Name="dddddd.exe",
                SizeInBytes=2343,
                IsPublic=true,
                UploadDate = DateTime.UtcNow

        },
                new FileCommon{Name="fff.jpg",
                SizeInBytes=2343,
                IsPublic=false,
                UploadDate = DateTime.UtcNow
                },
                new FileCommon{Name="dddddd.exe",
                SizeInBytes=2343,
                IsPublic=false,
                UploadDate = DateTime.UtcNow
                }
            };
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

        public async  void DownloadCommand()
        {
            var path = await _localFileService.SelectFolder();

            SelectedList.AsParallel().ForAll(async (file) =>
            {
                var downloadedFile = await _cloudFileService.DownloadFile(file);
                var fileClient = downloadedFile.ToClient();
                fileClient.Path = path;            
                await _localFileService.SaveFile(fileClient);
            });
        }

        public void ShareCommand()
        {
            SelectedList.AsParallel().ForAll(async (file) =>
            {
                file.IsPublic = true;
                await _cloudFileService.UpdateFileMetadata(file);
            });
        }

        [CommandExecute]
        void NavCommand(NavigationViewItemInvokedEventArgs args)
        {
            MethodInfo mi = this.GetType().GetMethod($"{args.InvokedItemContainer.Name}Command");
            mi.Invoke(this, null);
        }
    }

}
