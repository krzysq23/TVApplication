using BusinessLogic.Manager;
using Common.Settings;
using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Factory;
using TVProvider.Models;
using TVProvider.Provider;

namespace BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private ApiUserManager _provider;
        private AccountManager _accountManager;

        public AccountService()
        {
            _accountManager = new AccountManager();
        }

        public AccountService(ApiUserManager provider)
        {
            _provider = provider;
        }

        public AppUser GetAppUser(string userName)
        {
            return _accountManager.GetAppUserByUserName(userName);
        }

        public bool UpdateAppUser(string firstName, string lastName, string userName)
        {
            try
            {
                var appUser = _accountManager.GetAppUserByUserName(userName); 

                appUser.FirstName = firstName;
                appUser.LastName = lastName;

                _accountManager.AppUserUpdate(appUser);

                return true;
            }
            catch(Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                return false;
            }    
        }
    }
}
