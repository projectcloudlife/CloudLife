using Client.Command.Attributes;
using Client.Interfaces;
using Client.Models;
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
        public FileViewerViewModel(INavigationService navService, ICloudFileService cloudFileService)
        {
            _navigationService = navService;
            _cloudFileService = cloudFileService;
            InitFiles();
        }

        INavigationService _navigationService;
        ICloudFileService _cloudFileService;


        public ObservableCollection<FileCommon> FilesList { get; set; }
        public ObservableCollection<FileCommon> SelectedList { get; set; }


            public async void InitFiles()
        {
            //FilesList = new List<FileCommon>(await _cloudFileService.GetFiles(true));
            FilesList = new ObservableCollection<FileCommon>
            {
                new FileCommon{Name="fff.jpg",
                SizeInBytes=2343,
                IsPublic=true
                },
                new FileCommon{Name="dddddd.exe",
                SizeInBytes=2343,
                IsPublic=true
                }
            };
        }
        [CommandExecute]
        public void LogOutCommand()
        {
            //logout
            _navigationService.GoBack();
        }

        
        public void UploadCommand()
        {
            _navigationService.NavigateTo("UploadPage");
        }

        [CommandExecute]
        void NavCommand(NavigationViewItemInvokedEventArgs args)
        {
            MethodInfo mi = this.GetType().GetMethod($"{args.InvokedItemContainer.Name}Command");
            var context = (ObservableCollection<FileCommon>)args.InvokedItemContainer.DataContext;
            mi.Invoke(this, null);

        }
    }

}
