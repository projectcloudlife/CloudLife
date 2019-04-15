using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

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
