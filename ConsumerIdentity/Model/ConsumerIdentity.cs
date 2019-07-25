using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerIdentity.Model
{
    public class ConsumerIdentity
    {
        public Name Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthdate { get; private set; }

        public ConsumerIdentity(Name name, string email, DateTime birthdate)
        {
            Name = name;
            Email = email;
            Birthdate = birthdate;
        }
    }
}
