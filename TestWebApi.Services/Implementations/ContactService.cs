using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services.Implementations
{
    public class ContactService: IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Contact> AllContacts()
        {
            return _unitOfWork.GenericRepository<Contact>().GetAll();
        }
        public int GetContactsCountWithEmail(string Email)
        {
            return _unitOfWork.GenericRepository<Contact>().Get(c => c.Email == Email).Count();
        }
        public void AddContact(Contact contact)
        {
            _unitOfWork.GenericRepository<Contact>().Add(contact);
        }
        public void AddOrUpdateContactWhileAddingAccount(List<Contact> contacts)
        {
            foreach (Contact contact in contacts)
            {
                var existing_contact = _unitOfWork.GenericRepository<Contact>().Get(c => c.Email == contact.Email).FirstOrDefault();
                if (existing_contact == null)
                {
                    _unitOfWork.GenericRepository<Contact>().Add(contact);
                }
                else
                {
                    _unitOfWork.GenericRepository<Contact>().Update(contact);
                }
            }
        }
    }
}
