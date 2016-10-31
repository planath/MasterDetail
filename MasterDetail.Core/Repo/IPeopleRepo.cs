using System.Collections.Generic;
using MasterDetail.Core.Model;

namespace MasterDetail.Core.Repo
{
    public interface IPeopleRepo
    {
        Person FindByName(string query);
        Person FindByEmail(string query);
        Person FindById(int query);
        List<Person> Retrieve();
        void Post(List<Person> list);
    }
}