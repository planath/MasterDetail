/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MasterDetail"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using MasterDetail.Core.Repo;
using MasterDetail.Core.Service;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace MasterDetail.Core.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static ViewModelLocator _locator;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            // Register Services
            SimpleIoc.Default.Register<IPeopleService, PeopleService>();
            SimpleIoc.Default.Register<IPeopleRepo, PeopleRepo>();

            // Register VMs
            SimpleIoc.Default.Register<PeopleViewModel>();
            SimpleIoc.Default.Register<PersonViewModel>();

            // Create Instance as Messenger retrevial needs to be registered
            var instanciate1 = DetailVm;
        }
        
        public PeopleViewModel MainVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PeopleViewModel>();
            }
        }
        public PersonViewModel DetailVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PersonViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}