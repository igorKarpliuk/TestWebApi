using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Models;

namespace TestWebApi.Services.Interfaces
{
    public interface IContactService
    {
        public IEnumerable<Contact> AllContacts();
        public int GetContactsCountWithEmail(string Email);
        public void AddContact(Contact contact);
        public void AddOrUpdateContactWhileAddingAccount(List<Contact> contacts);
    }
}
