using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services.Implementations
{
    public class IncidentService : IIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IncidentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Incident> GetIncidentsWithAccounts()
        {
            return _unitOfWork.GenericRepository<Incident>().GetWithInclude(i => i.Accounts);
        }
        public void AddIncident(Incident incident)
        {
            _unitOfWork.GenericRepository<Incident>().Add(incident);
        }
    }
}
