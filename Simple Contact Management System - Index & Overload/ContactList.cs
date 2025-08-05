using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Contact_Management_System___Index___Overload
{
    public class ContactList : IEnumerable<Contact>
    {
        private List<Contact> contacts = new List<Contact>();

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public bool Remove(Contact contact)
        {
            return contacts.Remove(contact);
        }

        public IEnumerator<Contact> GetEnumerator()
        {
            return contacts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
