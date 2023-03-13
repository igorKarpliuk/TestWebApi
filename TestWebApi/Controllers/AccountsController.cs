using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Data.ViewModels;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IContactService _contactService;
        public AccountsController(IAccountService accountService, IContactService contactService)
        {
            _accountService = accountService;
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(_accountService.GetAccountsWithContacts());
        }
        [HttpPost]
        public IActionResult AddAccount(AccountAddViewModel account)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data!");
            }
            if (_accountService.GetAccountsByName(account.Name).Count() != 0)
            {
                return BadRequest("There is another account with current name!");
            }
            if (account.Contacts.Count == 0)
            {
                return BadRequest("Can not create account without contact!");
            }
            _contactService.AddOrUpdateContactWhileAddingAccount(account.Contacts);
            _accountService.AddAccount(new Account() { Name = account.Name, Contacts = account.Contacts });
            return Ok();
        }
    }
}
