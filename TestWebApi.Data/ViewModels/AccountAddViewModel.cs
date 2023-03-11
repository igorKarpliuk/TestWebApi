using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Models;

namespace TestWebApi.Data.ViewModels
{
    public class AccountAddViewModel
    {
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
