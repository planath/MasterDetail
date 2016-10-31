using System;
using System.Collections.Generic;
using System.Linq;
using MasterDetail.Core.Helper;
using MasterDetail.Core.Model;
using Newtonsoft.Json;

namespace MasterDetail.Core.Repo
{
    public class PeopleRepo : IPeopleRepo
    {
        private readonly ILocalPersistanceHelper _localPersistanceHelper;

        public PeopleRepo(ILocalPersistanceHelper localPersistanceHelper)
        {
            _localPersistanceHelper = localPersistanceHelper;
        }

        public Person FindByName(string query)
        {
            var list = Retrieve();

            var result = from c in list
                         where c.Name.Contains(query) | c.FirstName.Equals(query, StringComparison.OrdinalIgnoreCase) | c.LastName.Equals(query, StringComparison.OrdinalIgnoreCase)
                         select c;

            return result.First();
        }
        public Person FindByEmail(string query)
        {
            var list = Retrieve();

            var result = from c in list
                         where c.Email.Equals(query, StringComparison.OrdinalIgnoreCase)
                         select c;

            return result.First();
        }
        public Person FindById(int query)
        {
            var list = Retrieve();

            var result = from c in list
                         where c.Id == query
                         select c;

            return result.First();
        }
        public List<Person> Retrieve()
        {
            // access device locally
            var jsonString = _localPersistanceHelper.GetData();
            return JsonConvert.DeserializeObject<List<Person>>(jsonString);

            // Access from self created test instances
            // return GetTestInstances();
        }
        public void Post(List<Person> list)
        {
            // save device locally
            var jsonString = JsonConvert.SerializeObject(list);
            _localPersistanceHelper.SaveData(jsonString);
        }

        private List<Person> GetTestInstances()
        {
            return new List<Person>(){
                new Person(1, "Andreas", "Plüss", "andi@qlu.ch", RandomDay()),
                new Person(2, "Rudolf", "Rentiel", "rudddi@qlu.ch", RandomDay()),
                new Person(3, "Michi", "Albrecht", "albani@qlu.ch", RandomDay()),
                new Person(4, "Gerome", "Acker", "cheri@qlu.ch", RandomDay())
            };
        }

        private DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
