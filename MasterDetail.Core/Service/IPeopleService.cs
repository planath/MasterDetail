using System.Collections.Generic;
using MasterDetail.Core.Model;

namespace MasterDetail.Core.Service
{
    public interface IPeopleService
    {
        List<Person> GetAllPeople();
        Person PersonById(int id);
        void SaveAllPeople(List<Person> list);
        void RemovePerson(Person person);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
    }
}