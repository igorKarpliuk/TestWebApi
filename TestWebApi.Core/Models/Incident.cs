using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Core.Models
{
    [PrimaryKey(nameof(IncidentName))]
    public class Incident
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IncidentName { get; set; }
        public string Description { get; set; }
        public virtual IList<Account> Accounts { get; set; }
        public Incident()
        {
            Accounts = new List<Account>();
        }
    }
}
