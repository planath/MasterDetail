using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            _jsonString = JsonConvert.SerializeObject(GetTestInstances());
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
            var personToAdd = new Person("Maria", "Pluss", "andreas@plus.ch", DateTime.Now);
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


        [Test]
        public void GetAllPeopleSortEmailDesc_AskingForAllPeople_ReturnsListProperlySorted()
        {
            //act
            var list = _peopleService.GetAllPeopleSortEmailDesc();

            //assert
            Assert.AreEqual("rudddi@qlu.ch", list.First().Email);
        }
        [Test]
        public void GetAllPeopleSortLastFirstName_AskingForAllPeople_ReturnsListProperlySorted()
        {
            //act
            var list = _peopleService.GetAllPeopleSortLastFirstName();

            //assert
            Assert.AreEqual("Gerome", list.First().FirstName);
        }
        [Test]
        public void GetAllPeopleSortFirstLastName_AskingForAllPeople_ReturnsListProperlySorted()
        {
            //act
            var list = _peopleService.GetAllPeopleSortFirstLastName();

            //assert
            Assert.AreEqual("Kuster", list.First().LastName);
        }

        [Test]
        public void GetAllNames_ListOfStringsReturnedEachContainingFirstAndLastNameConcatedWithCommaInbetween()
        {
            //act
            var listOfPeople = _peopleService.GetAllPeople();
            var listOfNames = _peopleService.GetAllNames();

            //assert
            for (int i = 1; i <= listOfNames.Count(); i++)
            {
                var person = listOfPeople.FirstOrDefault();
                var personName = listOfNames.FirstOrDefault();
                Assert.IsTrue(personName.Contains(person.FirstName));
                Assert.IsTrue(personName.Contains(person.LastName));
                Assert.IsTrue(personName.Contains(", "));

                // Analyze
                TestContext.WriteLine(personName);

                listOfPeople.Remove(person);
                listOfNames.Remove(personName);
            }
        }
        

        private List<Person> GetTestInstances()
        {
            return new List<Person>(){
                new Person(1, "Andreas", "Plüss", "andi@qlu.ch", RandomDay()),
                new Person(2, "Rudolf", "Rentiel", "rudddi@qlu.ch", RandomDay()),
                new Person(3, "Michi", "Acker", "albani@qlu.ch", RandomDay()),
                new Person(4, "Andrew", "Fuller", "purzel@qlu.ch", RandomDay()),
                new Person(5, "Gerome", "Acker", "cheri@qlu.ch", RandomDay()),
                new Person(6, "Andrew", "Albrecht", "purzel@qlu.ch", RandomDay()),
                new Person(7, "Andreas", "Kuster", "andi@qlu.ch", RandomDay())
            };
        }
        private DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1992, 1, 1);
            int range = (new DateTime(1999, 12, 12) - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
