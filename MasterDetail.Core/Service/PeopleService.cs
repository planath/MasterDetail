using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using MasterDetail.Core.Model;
using MasterDetail.Core.Repo;

namespace MasterDetail.Core.Service
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepo _peopleRepo;

        public PeopleService(IPeopleRepo peopleRepo)
        {
            _peopleRepo = peopleRepo;
        }
        public List<Person> GetAllPeople()
        {
            return _peopleRepo.Retrieve();
        }
        public Person PersonById(int id)
        {
            return _peopleRepo.FindById(id);
        }

        public void SaveAllPeople(List<Person> list)
        {
            _peopleRepo.Post(list);
        }
        /// <summary>
        /// Updates a anlready existing Person if its id is already present in the persistet list or add a new person
        /// </summary>
        /// <param name="person">Person object that needs its values to be persisted after update or a new Person object that shall be added (This Person should not have an ID defined!).</param>
        public void SavePerson(Person person)
        {
            var allPeople = _peopleRepo.Retrieve();
            if (person.Id == null)
            {
                allPeople.Add(person);
            }
            else
            {
                var id = person.Id.Value;
                var personToUpdate = _peopleRepo.FindById(id);

                var index = allPeople.FindIndex(a => a.Id == personToUpdate.Id);
                if (index != -1)
                {
                    allPeople[index] = person;
                }
            }
            _peopleRepo.Post(allPeople);
        }
    }
}
