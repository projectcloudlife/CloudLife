using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
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
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public LoginViewModel LoginVM{ get => SimpleIoc.Default.GetInstance<LoginViewModel>(); }

    }
}
