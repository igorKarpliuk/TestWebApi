using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(_contactService.AllContacts());
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if (_contactService.GetContactsCountWithEmail(contact.Email) != 0)
            {
                return BadRequest("There is another contact with current email!");
            }
            _contactService.AddContact(contact);
            return Ok();
        }
    }
}
