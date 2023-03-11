using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Data.ViewModels;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public IncidentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetIncidents()
        {
            return Ok(_unitOfWork.GenericRepository<Incident>().GetWithInclude(i => i.Accounts));
        }
        [HttpPost]
        public IActionResult AddIncident(IncidentAddViewModel incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            foreach (AccountAddViewModel account in incident.Accounts)
            {
                var existing_account = _unitOfWork.GenericRepository<Account>().Get(a => a.Name == account.Name).FirstOrDefault();
                if (existing_account == null)
                {
                    return NotFound();
                }
            }
            List<Account> accounts = new List<Account>();
            foreach(AccountAddViewModel account in incident.Accounts)
            {
                var existing_account = _unitOfWork.GenericRepository<Account>().Get(a => a.Name == account.Name).FirstOrDefault();
                foreach(Contact contact in account.Contacts)
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
                existing_account.Contacts = account.Contacts;
                _unitOfWork.GenericRepository<Account>().Update(existing_account);
                accounts.Add(existing_account);
            }
            _unitOfWork.GenericRepository<Incident>().Add(new Incident() { Accounts = accounts, Description = incident.Description });
            return Ok();
        }
    }
}
