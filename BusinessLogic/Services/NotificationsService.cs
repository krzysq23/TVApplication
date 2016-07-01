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
    public class NotificationsService : INotificationsService
    {
        private NotificationsManager _notificationsManager;

        public NotificationsService()
        {
            _notificationsManager = new NotificationsManager();
        }

        public IEnumerable<Notifications> GetNotifications(string userName)
        {
            return _notificationsManager.GetMovieComment(userName);
        }

        public bool InsertNotification(Notifications notification)
        {
            return _notificationsManager.InsertNotification(notification);
        }
    }
}
