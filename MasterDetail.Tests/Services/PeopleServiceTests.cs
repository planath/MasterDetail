using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MasterDetail.Core.Helper;
using MasterDetail.Core.Model;
using MasterDetail.Core.Repo;
using MasterDetail.Core.Service;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MasterDetail.Tests
{
    [TestFixture]
    public class PeopleServiceTests
    {
        private PeopleRepo _peopleRepo;
        private PeopleService _peopleService;
        private ILocalPersistanceHelper _fakeLocalPersistanceHelper;
        private string _jsonString;

        public PeopleServiceTests()
        {
            _fakeLocalPersistanceHelper = Mock.Of<ILocalPersistanceHelper>();
            _jsonString = "[{\"Id\":1,\"Name\":\"Andreas Plüss\",\"FirstName\":\"André\",\"LastName\":\"Plüss\",\"Email\":\"andi@qlu.ch\",\"Birthday\":\"2006-10-14T00:00:00\",\"Favourite\":false},{\"Id\":2,\"Name\":\"Rudolf Rentiel\",\"FirstName\":\"Rudolf\",\"LastName\":\"Rentiel\",\"Email\":\"rudddi@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":3,\"Name\":\"Michi Albrecht\",\"FirstName\":\"Michi\",\"LastName\":\"Albrecht\",\"Email\":\"albani@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false},{\"Id\":4,\"Name\":\"Gerome Acker\",\"FirstName\":\"Gerome\",\"LastName\":\"Acker\",\"Email\":\"cheri@qlu.ch\",\"Birthday\":\"2002-12-04T00:00:00\",\"Favourite\":false}]";
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData())
                .Returns(_jsonString);

            _peopleRepo = new PeopleRepo(_fakeLocalPersistanceHelper);
            _peopleService = new PeopleService(_peopleRepo);

        }

        [Test]
        public void GetAllPeople_FirstLoad_ReturnStandardData()
        {
            //act
            var people = _peopleService.GetAllPeople();

            //assert
            Assert.IsTrue(people.Any());
        }

        [Test]
        public void PersonById_FirstLoad_ReturnPersonFromStandardData()
        {
            //arrange
            const int id = 1;

            //act
            var person = _peopleService.PersonById(id);

            //assert
            Assert.AreEqual(person.Id, id);
        }

        [Test]
        public void SaveAllPeople_StandardStringFromTestVar_ReturnNewDefinedString()
        {
            //arrange
            var people = new List<Person>
            {
                new Person(1, "Andi", "Pluss", "andreas@plus.ch", DateTime.Now)
            };

            //act
            _peopleService.SaveAllPeople(people);

            //assert
            var dataString = JsonConvert.SerializeObject(people);
            Mock.Get(_fakeLocalPersistanceHelper).Verify(f => f.SaveData(dataString), Times.Once);
        }

        [Test]
        public void RemovePerson_ListOfDefaultData_ListMinusDeletedPerson()
        {
            //arrange
            var people = _peopleService.GetAllPeople();
            var personToRemove = people.First();

            //act
            people.Remove(personToRemove);
            var dataAfterRemovingPerson = JsonConvert.SerializeObject(people);

            _peopleService.RemovePerson(personToRemove);
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData()).Returns(dataAfterRemovingPerson);

            //assert
            Mock.Get(_fakeLocalPersistanceHelper).Verify(f => f.SaveData(dataAfterRemovingPerson), Times.Once);
            var newlyRetrievedPeople = _peopleService.GetAllPeople();
            Assert.AreEqual(newlyRetrievedPeople.Count, people.Count);

            //compare also content of the two lists:
            //var compareson = Enumerable.SequenceEqual(people.OrderBy(t => t), newlyRetrievedPeople.OrderBy(t => t));
        }

        [Test]
        public void AddPerson_ListOfDefaultData_ListMinusDeletedPerson()
        {
            //arrange
            var people = _peopleService.GetAllPeople();
            var personToAdd = new Person("Andi", "Pluss", "andreas@plus.ch", DateTime.Now);
            var idShouldBeGivenToNewPerson = people.Max(p => p.Id) + 1;

            //act
            personToAdd.Id = idShouldBeGivenToNewPerson;
            people.Add(personToAdd);
            _peopleService.AddPerson(personToAdd);

            var dataAfterAddingPerson = JsonConvert.SerializeObject(people);
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData()).Returns(dataAfterAddingPerson);

            //assert
            Mock.Get(_fakeLocalPersistanceHelper).Verify(f => f.SaveData(dataAfterAddingPerson), Times.Once);
            var newlyRetrievedPeople = _peopleService.GetAllPeople();
            Assert.AreEqual(newlyRetrievedPeople.Count, people.Count);
            Assert.AreEqual(newlyRetrievedPeople.Last().Id, idShouldBeGivenToNewPerson);

            //compare also content of the two lists:
            //var compareson = Enumerable.SequenceEqual(people.OrderBy(t => t), newlyRetrievedPeople.OrderBy(t => t));
        }

        
        [Test]
        public void UpdatePerson_ListOfDefaultData_ListWithUpdatedData()
        {
            //arrange
            var people = _peopleService.GetAllPeople();
            var personToUpdate = people.First();
            personToUpdate.FirstName = "Andrew";

            //act
            var dataAfterUpdatingPerson = JsonConvert.SerializeObject(people);

            _peopleService.UpdatePerson(personToUpdate);
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData()).Returns(dataAfterUpdatingPerson);

            //assert
            Mock.Get(_fakeLocalPersistanceHelper).Verify(f => f.SaveData(dataAfterUpdatingPerson), Times.Once);
            var newlyRetrievedPeople = _peopleService.GetAllPeople();
            Assert.AreEqual(newlyRetrievedPeople.First().FirstName, personToUpdate.FirstName);
        }
    }
}
