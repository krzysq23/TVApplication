using DataLayer.Entities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public sealed class NotificationsManager
    {
        private Context _ctx;

        public NotificationsManager()
        {
            _ctx = new Context();
        }

        /// <summary>
        /// Get Movie Comment Notification
        /// </summary>
        /// <returns></returns>
        public List<Notifications> GetMovieComment(string userName)
        {
            try
            {
                var user = _ctx.AppUser.FirstOrDefault(x => x.UserName == userName);
                var records = _ctx.Notifications.Where(x => x.UserId == user.Id).ToList();

                return records;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Insert Notification
        /// </summary>
        /// <returns></returns>
        public bool InsertNotification(Notifications record)
        {
            try
            {
                if (record != null)
                {
                    _ctx.Notifications.Add(record);
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
        /// Insert Notifications
        /// </summary>
        /// <returns></returns>
        public bool InsertNotifications(List<Notifications> records)
        {
            try
            {
                _ctx.Notifications.AddRange(records);
                _ctx.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                throw;
            }

        }
    }
}
