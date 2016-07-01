using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Infrastructure
{
    public interface IEventNotifierLogic
    {
        Task<IEnumerable<Notifications>> GetCommentNotification(DateTime date);
    }
}
