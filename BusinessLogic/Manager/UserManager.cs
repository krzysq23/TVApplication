using DataLayer.Context;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public sealed class UserManager
    {
        private Context _ctx;

        public UserManager()
        {
            _ctx = new Context();
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        public int GetUsersIdByUserName(string userName)
        {
            try
            {
                return _ctx.AppUser.Where(w => w.UserName == userName).Select(s => s.Id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        public List<AppUser> GetUsers()
        {
            try
            {
                return _ctx.AppUser.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        public List<AppUser> GetUsers(int userId)
        {
            try
            {
                var list = _ctx.AppUser.ToList();

                var itemToRemove = list.Single(s => s.Id == userId);

                list.Remove(itemToRemove);

                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get users by Ids
        /// </summary>
        /// <returns></returns>
        public List<int> GetFriendsUsersIdByUserId(int userId)
        {
            try
            {
                return _ctx.UserFriend.Where(x => x.UserId == userId).Select(s => s.FriendAppUserId).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get users by Ids
        /// </summary>
        /// <returns></returns>
        public List<AppUser> GetUsersByIds(List<int> usersIds)
        {
            try
            {
                return _ctx.AppUser.Where(x => usersIds.Contains(x.Id)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Insert user to fiend
        /// </summary>
        /// <returns></returns>
        public bool InsertUserToFriends(int userId, int userFriendId)
        {
            try
            {
                var user = _ctx.UserFriend
                        .Where(c => c.UserId == userId && c.FriendAppUserId == userFriendId)
                        .FirstOrDefault();

                if (user == null)
                {
                    UserFriend record = new UserFriend();
                    record.FriendAppUserId = userFriendId;
                    record.UserId = userId;

                    _ctx.UserFriend.Add(record);
                    _ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Firnd delete
        /// </summary>
        /// <returns></returns>
        public bool DeleteFriend(int userId, int userFriendId)
        {
            try
            {
                var record = _ctx.UserFriend.Where(x => x.UserId == userId && x.FriendAppUserId == userFriendId).FirstOrDefault();
                _ctx.UserFriend.Remove(record);
                _ctx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
