using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.Service;

namespace MasterDetail.Core.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPeopleService _peopleService;

        public PersonViewModel(INavigationService navigationService, IPeopleService peopleService)
        {
            _navigationService = navigationService;
            _peopleService = peopleService;
            SavePersonCommand = new RelayCommand(SaveChanges);
            RemovePersonCommand = new RelayCommand(RemovePerson);
            NavigateBackCommand = new RelayCommand(() => _navigationService.GoBack());
            Messenger.Default.Register<PropertyChangedMessage<Person>>(this, SetPerson);
        }

        private void SetPerson(PropertyChangedMessage<Person> obj)
        {
            CurrentPerson = obj.NewValue;
        }

        public RelayCommand SavePersonCommand { get; set; }
        public RelayCommand RemovePersonCommand { get; set; }
        public RelayCommand NavigateBackCommand { get; set; }


        public Person CurrentPerson { get; set; }
        public string BirthdayString { get { return CurrentPerson.Birthday.ToString(); } set { CurrentPerson.Birthday = DateTime.Parse(value);} }

        public void Init()
        {
            Person person;
            // Id should be set from PropertyChangedMessage
            // if not, fallback to first person
            if (CurrentPerson == null)
            {
                // TODO: Rather setup an empty constructor for Person and
                // show a dialogbox informing, that person was not found
                person = _peopleService.GetAllPeople().First();
            }
        }

        private void SaveChanges()
        {
            RaisePropertyChanged(nameof(CurrentPerson), null, CurrentPerson, true);
            _navigationService.GoBack();
        }

        private void RemovePerson()
        {
            CurrentPerson.Delete = true;
            RaisePropertyChanged(nameof(CurrentPerson), null, CurrentPerson, true);
            _navigationService.GoBack();
        }
    }
}
