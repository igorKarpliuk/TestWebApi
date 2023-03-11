using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TestWebApi.Core.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Contact> Contacts { get; set; }
        public Account()
        {
            Contacts = new List<Contact>();
        }
    }
}
