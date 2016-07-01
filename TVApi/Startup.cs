using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using TVApi.Providers;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using TVApi.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using DataLayer.Context;
using DataLayer.Manager;
using DataLayer.Entities;
using Microsoft.AspNet.SignalR;
using TVApi.SignalR;
using Microsoft.Owin.Security;
using BusinessLogic.Manager;
using System.Web.Routing;
using System.Web.Helpers;
using System.Security.Claims;

[assembly: OwinStartup(typeof(TVApi.Startup))]

namespace TVApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new CustomUserIdProvider());

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJavaScriptProxies = true
                };
                map.RunSignalR(hubConfiguration);
            });
            
            app.UseWebApi(GlobalConfiguration.Configuration);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }
    }   
}
