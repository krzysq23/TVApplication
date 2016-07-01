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
    public class UserService : IUserService
    {
        private UserManager _userManager;
        private AccountManager _accountManager;

        public UserService()
        {
            _userManager = new UserManager();
            _accountManager  = new AccountManager();
        }

        public IEnumerable<AppUser> GetUserList()
        {
            return _userManager.GetUsers();
        }

        public AppUser GetUserNyUserName(string userName)
        {
            return _accountManager.GetAppUserByUserName(userName);
        }

        public IEnumerable<AppUser> GetUserList(string userName)
        {
            var appUser = _accountManager.GetAppUserByUserName(userName);

            return _userManager.GetUsers(appUser.Id);
        }

        public IEnumerable<AppUser> GetUserFriends(string userName)
        {
            var appUser = _accountManager.GetAppUserByUserName(userName);

            var friendsUserId = _userManager.GetFriendsUsersIdByUserId(appUser.Id);

            return _userManager.GetUsersByIds(friendsUserId);
        }
        
        public bool AddUserToFirends(string userName, string userFriendId)
        {
            var userFriendIdInt = Convert.ToInt32(userFriendId);

            var user = _accountManager.GetAppUserByUserName(userName);

            return _userManager.InsertUserToFriends(user.Id, userFriendIdInt);
        }

        public bool DeleteFriend(string userName, string userFriendId)
        {
            var userFriendIdInt = Convert.ToInt32(userFriendId);

            var user = _accountManager.GetAppUserByUserName(userName);

            return _userManager.DeleteFriend(user.Id, userFriendIdInt);
        }
    }
}
