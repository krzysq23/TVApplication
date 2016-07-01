using BusinessLogic.Services;
using DataLayer.Context;
using DataLayer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using TVApi.Models;
using TVProvider.Models;

namespace TVApi.Controllers
{
    [RoutePrefix("api/Notifications")]
    public class NotificationsController : ApiController
    {
        private INotificationsService _notificationsService;

        public NotificationsController()
        {
            this._notificationsService = new NotificationsService();
        }


        [HttpGet]
        [Route("Get")]
        public IEnumerable<Notifications> GetUserNotifications(string userName)
        {
            var data = _notificationsService.GetNotifications(userName);
            return data;
        }
    }
}
