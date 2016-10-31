using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MasterDetail.Core.Model
{
    public class Person : IComparable
    {
        private int? _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;
        private bool _favourite;

        [JsonConstructor]
        public Person(int id, string firstName, string lastName, string email, DateTime birthday)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (firstName == null) throw new ArgumentNullException(nameof(firstName));
            if (lastName == null) throw new ArgumentNullException(nameof(lastName));
            if (email == null) throw new ArgumentNullException(nameof(email));

            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthday = birthday;
            _favourite = false;
        }

        // Constructor with id = null should only be used to creat new person from within app
        public Person(string firstName, string lastName, string email, DateTime birthday)
        {
            if (firstName == null) throw new ArgumentNullException(nameof(firstName));
            if (lastName == null) throw new ArgumentNullException(nameof(lastName));
            if (email == null) throw new ArgumentNullException(nameof(email));
            
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthday = birthday;
            _favourite = false;
        }

        public string Name { get { return _firstName + " " + _lastName; }
            set
            {
                if (value.Length > 0)
                {
                    string[] names = value.Split(null);
                    LastName = names.Last();
                    if (names.Length > 1)
                    {
                        var firstNames = names.Take(names.Length - 1);
                        FirstName = String.Join(" ", firstNames);
                    }
                }
            }
        }
        public int? Id { get { return _id; } set { _id = value; } }
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public DateTime Birthday { get { return _birthday; } set { _birthday = value; } }
        public bool Favourite { get { return _favourite; } set { _favourite = value; } }
        public int CompareTo(object obj)
        {
            Person person = obj as Person;
            if (person == null)
                throw new ArgumentException("person is not an Person object.");

            var isId = Id.ToString();
            var searchId = person.Id.ToString();
            
            return isId.CompareTo(searchId);
        }

        public Person Clone()
        {
            return (Person)this.MemberwiseClone();
        }
    }
}
