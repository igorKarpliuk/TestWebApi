using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Data.ViewModels;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(_unitOfWork.GenericRepository<Account>().GetWithInclude(a => a.Contacts));
        }
        [HttpPost]
        public IActionResult AddAccount(AccountAddViewModel account)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data!");
            }
            if (_unitOfWork.GenericRepository<Account>().Get(a => a.Name == account.Name).Count() != 0)
            {
                return BadRequest("There is another account with current name!");
            }
            if (account.Contacts.Count == 0)
            {
                return BadRequest("Can not create account without contact!");
            }
            foreach (Contact contact in account.Contacts)
            {
                var existing_contact = _unitOfWork.GenericRepository<Contact>().Get(c => c.Email == contact.Email).FirstOrDefault();
                if(existing_contact == null)
                {
                    _unitOfWork.GenericRepository<Contact>().Add(contact);
                }
                else
                {
                    _unitOfWork.GenericRepository<Contact>().Update(contact);
                }
            }
            _unitOfWork.GenericRepository<Account>().Add(new Account() { Name = account.Name, Contacts = account.Contacts });
            return Ok();
        }
    }
}
