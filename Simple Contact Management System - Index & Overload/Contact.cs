using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Contact_Management_System___Index___Overload
{
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Constructor
        public Contact(string name, string phoneNumber, string email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // Overload ToString for easy display
        public override string ToString()
        {
            return $"Name: {Name}, Phone: {PhoneNumber}, Email: {Email}";
        }

        // Overload comparison operators based on name and phone number
        public static bool operator ==(Contact a, Contact b)
        {
            return a.Name == b.Name && a.PhoneNumber == b.PhoneNumber;
        }

        public static bool operator !=(Contact a, Contact b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact)
            {
                return this == (Contact)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, PhoneNumber);
        }
    }

}
