using Client.Views;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ClientLocator
    {

        public ClientLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure("LoginPage", typeof(LoginPage));
            nav.Configure("UploadPage", typeof(UploadPage));
            nav.Configure("FileViewerPage", typeof(FileViewerPage));
            SimpleIoc.Default.Register<INavigationService>(() => nav);


            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<UploadViewModel>();
            SimpleIoc.Default.Register<FileViewerViewModel>();
        }

        public LoginViewModel LoginVM{ get => SimpleIoc.Default.GetInstance<LoginViewModel>(); }
        public UploadViewModel UploadVM{ get => SimpleIoc.Default.GetInstance<UploadViewModel>(); }
        public FileViewerViewModel FileViewerVM { get => SimpleIoc.Default.GetInstance<FileViewerViewModel>(); }

    }
}
