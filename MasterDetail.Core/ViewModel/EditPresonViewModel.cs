using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;

namespace MasterDetail.Core.ViewModel
{
    public class EditPresonViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public EditPresonViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SavePersonCommand = new RelayCommand(SaveNewPerson);
            NavigateBackCommand = new RelayCommand(() => _navigationService.GoBack());
            Messenger.Default.Register<PropertyChangedMessage<Person>>(this, SetPerson);
        }

        private void SetPerson(PropertyChangedMessage<Person> obj)
        {
            Person = obj.NewValue;
            PersonUpdated = Person.Clone();
        }

        public RelayCommand NavigateBackCommand { get; set; }
        public RelayCommand SavePersonCommand { get; set; }
        public string PageTitle { get; set; }
        public Person Person { get; set; }
        public Person PersonUpdated { get; set; }


        public void Init()
        {
            PageTitle = "Kontakt editieren";
        }

        private void SaveNewPerson()
        {
            //just pass it back to main observer list, saving should be there.
            if (!string.IsNullOrEmpty(Person.FirstName) && !string.IsNullOrEmpty(Person.LastName) && !string.IsNullOrEmpty(Person.Birthday.ToString()) &&
                !string.IsNullOrEmpty(Person.Email))
            {
                RaisePropertyChanged(nameof(Person), Person, PersonUpdated, true);
                NavigateBackCommand.Execute("");
            }
        }
    }
}
