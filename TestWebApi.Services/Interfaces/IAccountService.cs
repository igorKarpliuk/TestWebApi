using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Models;
using TestWebApi.Data.ViewModels;

namespace TestWebApi.Services.Interfaces
{
    public interface IAccountService
    {
        public IEnumerable<Account> GetAccountsWithContacts();
        public IEnumerable<Account> GetAccountsByName(string Name);
        public void AddAccount(Account account);
        public bool CheckIfAllAccountsExist(List<AccountAddViewModel> accounts);
        public void UpdateAccountWhileAddingIncident(Account account);
    }
}
