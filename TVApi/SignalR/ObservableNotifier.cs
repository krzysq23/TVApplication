using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using BusinessLogic.Services;
using TVProvider.Provider;
using BusinessLogic.Infrastructure;
using TVApi.Controllers;
using DataLayer.Entities;

namespace TVApi.SignalR
{
    public class ObservableNotifier
    {
        private static readonly Lazy<ObservableNotifier> _instance =
            new Lazy<ObservableNotifier>(() =>
                new ObservableNotifier(GlobalHost.ConnectionManager.GetHubContext<NotifierHub>().Clients));

        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        private readonly IEventNotifierLogic _eventNotifierLogic;

        private ObservableNotifier(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _eventNotifierLogic = new EventNotifierLogic();
        }

        public static ObservableNotifier Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public void SendCommentNotification(Notifications notif)
        {
            Clients.All.addCommentNotification(notif);
        }

        public void SendCommentNotificationToUser(Notifications notif)
        {
            var toId = _connections.GetConnections(notif.UserId.ToString()).ToList();
            //Clients.Clients(toId).addCommentNotif(notif);
        }

        internal void MapUserConnection(string name, string connectionId)
        {
            _connections.Add(name, connectionId);
        }    

        internal void RemoveConnection(string name, string connectionId)
        {
            _connections.Remove(name, connectionId);
        }
    }
}