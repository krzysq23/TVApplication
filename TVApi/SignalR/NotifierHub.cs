using DataLayer.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using TVApi.Controllers;

namespace TVApi.SignalR
{
    [HubName("observableNotifier")]
    public class NotifierHub : Hub
    {
        private readonly ObservableNotifier _eventNotifier;

        private string UserId
        {
            get
            {
                if (Context != null)
                {
                    return Context.User != null
                        ? ""
                        : string.Empty;
                }

                return string.Empty;
            }
        }

        public NotifierHub() : 
            this(ObservableNotifier.Instance)
        {
        }

        public NotifierHub(ObservableNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public override Task OnConnected()
        {
            var authCookie3 = HttpContext.Current.User.Identity.Name;
            var authCooki4e23 = HttpContext.Current.Request.UserHostName;
            var authCoo = HttpContext.Current.Request.IsAuthenticated;
            var ida = Context.Request.User.Identity.IsAuthenticated;

            _eventNotifier.MapUserConnection(UserId, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _eventNotifier.RemoveConnection(UserId, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            _eventNotifier.MapUserConnection(UserId, Context.ConnectionId);

            return base.OnReconnected();
        }
    }
}