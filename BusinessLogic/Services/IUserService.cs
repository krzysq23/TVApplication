using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetUserList();
        AppUser GetUserNyUserName(string userName);
        IEnumerable<AppUser> GetUserList(string userName);
        IEnumerable<AppUser> GetUserFriends(string userName);
        bool AddUserToFirends(string userName, string userFriendId);
        bool DeleteFriend(string userName, string userFriendId);

    }
}
