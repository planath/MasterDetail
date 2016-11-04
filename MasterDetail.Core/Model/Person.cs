using System;
using System.Linq;
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
        private bool _delete;

        public Person()
        { 
        }

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
            _delete = false;
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
            _delete = false;
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
        public DateTime Birthday { get { return _birthday; } set { _birthday = RandomDay(); } }
        public bool Delete { get { return _delete; } set { _delete = value; } }
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

        protected bool Equals(Person other)
        {
            return _id == other._id && string.Equals(_firstName, other._firstName) && string.Equals(_lastName, other._lastName) && string.Equals(_email, other._email) && _birthday.Equals(other._birthday) && _delete == other._delete;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Person) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _id.GetHashCode();
                hashCode = (hashCode*397) ^ (_firstName != null ? _firstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_lastName != null ? _lastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_email != null ? _email.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _birthday.GetHashCode();
                hashCode = (hashCode*397) ^ _delete.GetHashCode();
                return hashCode;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this == null ||
                    string.IsNullOrEmpty(FirstName) ||
                    string.IsNullOrEmpty(LastName) ||
                    string.IsNullOrEmpty(Birthday.ToString()) ||
                    string.IsNullOrEmpty(Email));
            }
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
