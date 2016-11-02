using System;
using System.Collections.Generic;
using MasterDetail.Core.Helper;
using MasterDetail.Core.Model;
using Newtonsoft.Json;

namespace MasterDetail.iOS
{
    internal class LocalPersistanceHelper : ILocalPersistanceHelper
    {
        public void SaveData(string dataJson)
        {
            Console.WriteLine("no persisting yet");
        }

        public string GetData()
        {
            return JsonConvert.SerializeObject(GetTestInstances());
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