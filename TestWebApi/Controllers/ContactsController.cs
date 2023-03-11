using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(_unitOfWork.GenericRepository<Contact>().GetAll());
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if (_unitOfWork.GenericRepository<Contact>().Get(c => c.Email == contact.Email).Count() != 0)
            {
                return BadRequest("There is another contact with current email!");
            }
            _unitOfWork.GenericRepository<Contact>().Add(contact);
            return Ok();
        }
    }
}
