using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Models;

namespace TestWebApi.Services.Interfaces
{
    public interface IIncidentService
    {
        public IEnumerable<Incident> GetIncidentsWithAccounts();
        public void AddIncident(Incident incident);
    }
}
