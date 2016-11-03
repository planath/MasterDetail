using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Person> GetAllPeopleSortEmailDesc()
        {
            var people = GetAllPeople();
            var result = people.OrderByDescending(person => person.Email);
            return result.ToList();
        }
        public List<Person> GetAllPeopleSortLastFirstName()
        {
            var people = GetAllPeople();
            var result = people.OrderBy(person => person.LastName)
                .ThenBy(person => person.FirstName);
            return result.ToList();
        }
        public List<Person> GetAllPeopleSortFirstLastName()
        {
            var people = GetAllPeople();
            var result = people.OrderBy(person => person.FirstName)
                .ThenBy(person => person.LastName);
            return result.ToList();
        }

        public List<string> GetAllNames()
        {
            var people = GetAllPeople();
            var query = people.Select(p => p.FirstName + ", " + p.LastName);
            return query.ToList();
        }
        public Person PersonById(int id)
        {
            return _peopleRepo.FindById(id);
        }

        public void SaveAllPeople(List<Person> list)
        {
            _peopleRepo.Post(list);
        }

        public void RemovePerson(Person person)
        {
            var allPeople = _peopleRepo.Retrieve();
            var observedPerson = allPeople.FirstOrDefault(i => i.Id == person.Id);
            allPeople.Remove(observedPerson);
            SaveAllPeople(allPeople.ToList());
        }

        public void AddPerson(Person person)
        {
            var allPeople = _peopleRepo.Retrieve();
            var personHighestId = allPeople.Max(p => p.Id);
            var newId = personHighestId + 1;
            person.Id = newId.Value;
            allPeople.Add(person);
            SaveAllPeople(allPeople.ToList());
        }
        public void UpdatePerson(Person person)
        {
            var allPeople = _peopleRepo.Retrieve();
            var observedPerson = allPeople.FirstOrDefault(i => i.Id == person.Id);
            if (observedPerson != null)
            {
                observedPerson.FirstName = person.FirstName;
                observedPerson.LastName = person.LastName;
                observedPerson.Birthday = person.Birthday;
                observedPerson.Email = person.Email;
                observedPerson.Delete = person.Delete;
            }

            SaveAllPeople(allPeople.ToList());
        }
    }
}
