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
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;
        private readonly IAccountService _accountService;
        private readonly IContactService _contactService;
        public IncidentsController(IIncidentService incidentService, IAccountService accountService, IContactService contactService)
        {
            _incidentService = incidentService;
            _accountService = accountService;
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult GetIncidents()
        {
            return Ok(_incidentService.GetIncidentsWithAccounts());
        }
        [HttpPost]
        public IActionResult AddIncident(IncidentAddViewModel incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if(!_accountService.CheckIfAllAccountsExist(incident.Accounts))
            {
                return NotFound();
            }
            List<Account> accounts = new List<Account>();
            foreach(AccountAddViewModel account in incident.Accounts)
            {
                var existing_account = _accountService.GetAccountsByName(account.Name).FirstOrDefault();
                _contactService.AddOrUpdateContactWhileAddingAccount(account.Contacts);
                existing_account.Contacts = account.Contacts;
                _accountService.UpdateAccountWhileAddingIncident(existing_account);
                accounts.Add(existing_account);
            }
            _incidentService.AddIncident(new Incident() { Accounts = accounts, Description = incident.Description });
            return Ok();
        }
    }
}
