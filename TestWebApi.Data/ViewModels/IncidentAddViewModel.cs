using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Data.ViewModels
{
    public class IncidentAddViewModel
    {
        public List<AccountAddViewModel> Accounts { get; set; }
        public string Description { get; set; }
    }
}
