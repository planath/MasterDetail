using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.Service;

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
                RemovePersonCommand = new RelayCommand(RemovePerson);

                Messenger.Default.Register<PropertyChangedMessage<Person>>(this, UpdateList);
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
                    RaisePropertyChanged(nameof(SelectedPerson), oldVal, _person, true);
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

                //add person if not in list
                if (observedPerson == null)
                {
                    person.Id = People.Count + 1;
                    People.Add(person);
                    _peopleService.SaveAllPeople(People.ToList());
                }
                //if found and delete flag set
                else if (person.Delete)
                {
                    People.Remove(observedPerson);
                    _peopleService.SaveAllPeople(People.ToList());
                }
                //if found, do an update
                else
                {
                    observedPerson.FirstName = person.FirstName;
                    observedPerson.LastName = person.LastName;
                    observedPerson.Birthday = person.Birthday;
                    observedPerson.Email = person.Email;
                    observedPerson.Delete = person.Delete;
                    _peopleService.SaveAllPeople(People.ToList());
                }
            }
            private IEnumerable<Person> InitPeopleList()
            {
                var people = _peopleService.GetAllPeople();
                return people;
            }

            private void RemovePerson()
            {
                if (!People.Any()) return;
                People.Remove(People.Last());
            }

            private void AddPerson()
            {
                _navigationService.NavigateTo("Add");
            }
        }
    }
}
