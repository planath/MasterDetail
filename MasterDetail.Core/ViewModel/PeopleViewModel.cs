﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.Service;
using MasterDetail.Core.ViewModel.Helper;

namespace MasterDetail.Core.ViewModel
{
    namespace mvvmlight.Core.ViewModel
    {
        public class PeopleViewModel : ViewModelBase
        {
            private readonly INavigationService _navigationService;
            private readonly IPeopleService _peopleService;

            public PeopleViewModel(INavigationService navigationService, IPeopleService peopleService)
            {
                _navigationService = navigationService;
                _peopleService = peopleService;
                AddPersonCommand = new RelayCommand(AddPerson);

                Messenger.Default.Register<PropertyChangedMessage<Person>>(this, UpdateList);
                Messenger.Default.Register<GoToPageMessage>(this, GoToPerson);
            }

            private void GoToPerson(GoToPageMessage obj)
            {
                var person = People.First(p => p.Id == obj.PersonId);
                SelectedPerson = person;
            }

            public RelayCommand<string> DetailsCommand { get; set; }
            public RelayCommand AddPersonCommand { get; set; }
            public RelayCommand RemovePersonCommand { get; set; }
            public ObservableCollection<Person> People { get; private set; }
            private Person _person;
            public Person SelectedPerson
            {
                get { return _person; }
                set
                {
                    var oldVal = _person;
                    _person = value;
                    _navigationService.NavigateTo("Detail");
                    RaisePropertyChanged("Person", oldVal, _person, true);
                }
            }
            private int _index;
            public int CurrentIndex
            {
                get { return _index; }
                set
                {
                    _index = value;
                    SelectedPerson = People[value];
                }
            }

            //TODO: Simpler?
            public void Init()
            {
                if (People != null)
                {
                    // Prevent memory leak in Android
                    var peopleCopy = _peopleService.GetAllPeople();
                    People = new ObservableCollection<Person>(peopleCopy);
                }

                People = new ObservableCollection<Person>();

                var people = InitPeopleList();
                People.Clear();
                foreach (var person in people)
                    People.Add(person);
            }

            private void UpdateList(PropertyChangedMessage<Person> obj)
            {
                var person = obj.NewValue;
                var observedPerson = People.FirstOrDefault(i => i.Id == person.Id);

                //add person if new to persist
                if (observedPerson == null && !person.IsEmpty)
                {
                    People.Add(person);
                    _peopleService.AddPerson(person);
                }
                //if found and delete flag set
                if (person.Delete)
                {
                    People.Remove(person);
                    _peopleService.RemovePerson(person);
                }
                //if found, do an update
                else if (observedPerson != null)
                {
                    observedPerson.FirstName = person.FirstName;
                    observedPerson.LastName = person.LastName;
                    observedPerson.Birthday = person.Birthday;
                    observedPerson.Email = person.Email;
                    observedPerson.Delete = person.Delete;

                    _peopleService.UpdatePerson(person);
                }
            }
            private IEnumerable<Person> InitPeopleList()
            {
                var people = _peopleService.GetAllPeople();
                return people;
            }

            private void AddPerson()
            {
                SelectedPerson = new Person();
            }
        }
    }
}
