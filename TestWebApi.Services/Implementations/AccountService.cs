using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Core.Interfaces;
using TestWebApi.Core.Models;
using TestWebApi.Data.ViewModels;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services.Implementations
{
    public class AccountService: IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Account> GetAccountsWithContacts()
        {
            return _unitOfWork.GenericRepository<Account>().GetWithInclude(a => a.Contacts);
        }
        public IEnumerable<Account> GetAccountsByName(string Name)
        {
            return _unitOfWork.GenericRepository<Account>().Get(a => a.Name == Name);
        }
        public void AddAccount(Account account)
        {
            _unitOfWork.GenericRepository<Account>().Add(account);
        }
        public bool CheckIfAllAccountsExist(List<AccountAddViewModel> accounts)
        {
            foreach (AccountAddViewModel account in accounts)
            {
                var existing_account = _unitOfWork.GenericRepository<Account>().Get(a => a.Name == account.Name).FirstOrDefault();
                if (existing_account == null)
                {
                    return false;
                }
            }
            return true;
        }
        public void UpdateAccountWhileAddingIncident(Account account)
        {
            _unitOfWork.GenericRepository<Account>().Update(account);
        }
    }
}
