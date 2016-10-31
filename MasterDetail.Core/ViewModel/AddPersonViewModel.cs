using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.Service;

namespace MasterDetail.Core.ViewModel
{
    public class AddPersonViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPeopleService _peopleService;

        public AddPersonViewModel(INavigationService navigationService, IPeopleService peopleService)
        {
            _navigationService = navigationService;
            _peopleService = peopleService;
            SavePersonCommand = new RelayCommand(SaveNewPerson);
            NavigateBackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        public RelayCommand NavigateBackCommand { get; set; }

        public RelayCommand SavePersonCommand { get; set; }
        public string PageTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }


        public void Init()
        {
            PageTitle = "Neuen Kontakkt";
        }

        private void SaveNewPerson()
        {
            //just pass it back to main observer list, saving should be there.
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Birthday) &&
                !string.IsNullOrEmpty(Email))
            {
                var person = new Person(FirstName, LastName, Email, new DateTime());
                const string newPerson = "NewPerson";
                RaisePropertyChanged(newPerson, null, person, true);
                NavigateBackCommand.Execute("");
            }
        }
    }
}
