using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Helper;
using MasterDetail.Core.ViewModel;
using MasterDetail.Helpers;
using MasterDetail.Resources.layout;

namespace MasterDetail
{
    public static class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    // Initialize the MVVM Light DispatcherHelper.
                    // This needs to be called on the UI thread.
                    DispatcherHelper.Initialize();

                    // Configure and register the MVVM Light NavigationService
                    // for going to Detail Page and back
                    var navigationService = CreateNavigationService();
                    SimpleIoc.Default.Register<INavigationService>(() => navigationService);

                    // Configure further dependancies
                    SimpleIoc.Default.Register<ILocalPersistanceHelper, LocalPersistanceHelper>();

                    _locator = new ViewModelLocator();
                }

                return _locator;
            }
        }
        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Detail", typeof(DetailActivity));

            return navigationService;
        }
    }
}