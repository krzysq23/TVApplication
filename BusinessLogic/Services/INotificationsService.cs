using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace BusinessLogic.Services
{
    public interface INotificationsService
    {
        IEnumerable<Notifications> GetNotifications(string userName); 
        bool InsertNotification(Notifications notification);
    }
}
