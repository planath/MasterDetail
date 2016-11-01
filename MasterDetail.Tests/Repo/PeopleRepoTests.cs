using System;
using System.Collections.Generic;
using MasterDetail.Core.Helper;
using MasterDetail.Core.Model;
using MasterDetail.Core.Repo;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MasterDetail.Tests.Repo
{
    [TestFixture]
    class PeopleRepoTests
    {
        private ILocalPersistanceHelper _fakeLocalPersistanceHelper;
        private PeopleRepo _repo;
        private string _jsonString;

        [SetUp]
        public void SetUp()
        {
            _jsonString = JsonConvert.SerializeObject(GetTestInstances());
            _fakeLocalPersistanceHelper = Mock.Of<ILocalPersistanceHelper>();
            Mock.Get(_fakeLocalPersistanceHelper).Setup(o => o.GetData())
                .Returns(_jsonString);
            _repo = new PeopleRepo(_fakeLocalPersistanceHelper);

        }

        [Test]
        public void FindFirstByName_GiveLastName_FindsFirstPersonContainingThatName()
        {
            var query = "Plüs";
            var person = _repo.FindFirstByName(query);

            Assert.IsTrue(person.LastName.Contains(query));
        }

        [Test]
        public void FindSecondByName_GiveFirstName_FindsTheSecondPersonContainingThatName()
        {
            var query = "Andr";
            var person = _repo.FindSecondByName(query);

            Assert.IsTrue(person.FirstName.Equals("André"));
        }
        
        private List<Person> GetTestInstances()
        {
            return new List<Person>(){
                new Person(1, "Andreas", "Plüss", "andi@qlu.ch", RandomDay()),
                new Person(2, "Rudolf", "Rentiel", "rudddi@qlu.ch", RandomDay()),
                new Person(3, "Michi", "Albrecht", "albani@qlu.ch", RandomDay()),
                new Person(4, "André", "Fuller", "purzel@qlu.ch", RandomDay()),
                new Person(5, "Gerome", "Acker", "cheri@qlu.ch", RandomDay())
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
