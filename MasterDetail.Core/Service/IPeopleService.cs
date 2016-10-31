using System.Collections.Generic;
using MasterDetail.Core.Model;
using MasterDetail.Core.Repo;

namespace MasterDetail.Core.Service
{
    public interface IPeopleService
    {
        List<Person> GetAllPeople();
        Person PersonById(int id);
        void SaveAllPeople(List<Person> list);
        void SavePerson(Person person);
    }
}