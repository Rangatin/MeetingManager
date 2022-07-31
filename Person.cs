using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   FirstName == person.FirstName &&
                   LastName == person.LastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}
