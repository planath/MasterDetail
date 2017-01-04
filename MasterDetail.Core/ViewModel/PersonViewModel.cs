using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.Service;
using MasterDetail.Core.ViewModel.Helper;

namespace MasterDetail.Core.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPeopleService _peopleService;
        private ViewMode _viewMode;

        public PersonViewModel(INavigationService navigationService, IPeopleService peopleService)
        {
            _navigationService = navigationService;
            _peopleService = peopleService;
            EditTogglePersonCommand = new RelayCommand(EditTogglePerson);
            SavePersonCommand = new RelayCommand(SavePerson);
            RemovePersonCommand = new RelayCommand(RemovePerson);
            NavigateBackCommand = new RelayCommand(() => _navigationService.GoBack());
            Messenger.Default.Register<PropertyChangedMessage<Person>>(this, SetPerson);
        }

        #region Properties and Commands
        public RelayCommand EditTogglePersonCommand { get; set; }
        public RelayCommand SavePersonCommand { get; set; }
        public RelayCommand RemovePersonCommand { get; set; }
        public RelayCommand AddPersonCommand { get; set; }
        public RelayCommand NavigateBackCommand { get; set; }
        public Person CurrentPerson { get; set; }
        public string BirthdayString { get { return CurrentPerson.Birthday.ToString(); } set { CurrentPerson.Birthday = DateTime.Now; } }

        public ViewMode CurrentViewMode
        {
            get { return _viewMode; }
            set
            {
                if (!CurrentPerson.IsEmpty && value == ViewMode.Show)
                {
                    SavePerson();
                }
                if (_viewMode != value)
                {
                    RaisePropertyChanged("CurrentViewMode", _viewMode, value, true);
                    _viewMode = value;
                }
            }
        }
        
        #endregion
        public void Init()
        {
            if (CurrentPerson.Id == null)
            {
                CurrentViewMode = ViewMode.Edit;
            }
            else
            {
                CurrentViewMode = ViewMode.Show;
            }
        }

        #region Internal functions
        private void SavePerson()
        {
            RaisePropertyChanged(nameof(CurrentPerson), null, CurrentPerson, true);
        }
        private void RemovePerson()
        {
            CurrentPerson.Delete = true;
            RaisePropertyChanged(nameof(CurrentPerson), null, CurrentPerson, true);
            _navigationService.GoBack();
        }
        private void SetPerson(PropertyChangedMessage<Person> obj)
        {
            var person = obj.NewValue;
            CurrentPerson = person;
        }
        private void EditTogglePerson()
        {
            if (CurrentViewMode == ViewMode.Show)
            {
                CurrentViewMode = ViewMode.Edit;
            }
            else
            {
                CurrentViewMode = ViewMode.Show;
            }
        }
        #endregion
    }
}
