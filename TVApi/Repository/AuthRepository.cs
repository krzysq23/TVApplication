using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TVApi.Models;
using DataLayer.Entities;
using DataLayer.Manager;
using DataLayer.Context;
using BusinessLogic.Manager;
using BusinessLogic.Services;

namespace TVApi.Repository
{
    public class AuthRepository : IDisposable
    {
        private Context _ctx;

        private ApiUserManager _userManager;
        private AccountManager _accountManager;

        public AuthRepository()
        {
            _ctx = new Context();
            _userManager = new ApiUserManager(new UserStore<ApplicationUser>(_ctx));
            _accountManager = new AccountManager();
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {

            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            //created AppUser
            if (result.Succeeded)
            {
                try
                {
                    var findUser = await _userManager.FindAsync(user.UserName, userModel.Password);
                    _accountManager.AppUserInsert(user.Id, user.UserName);
                }
                catch(Exception ex)
                {
                    Common.Log.Log4Net.logger.Error(ex, "", "");
                }
                
            }

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        //public async Task SignInAsync(IdentityUser user)
        //{
        //    ApplicationUser user = await _repo.FindAsync(new UserLoginInfo(user.Provider, user.id));
        //}

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}