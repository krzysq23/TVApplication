using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TVApi.Providers;
using TVApi.Models;
using Microsoft.Owin.Security.Facebook;
using System.Web.Cors;
using System.Configuration;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Manager;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using DataLayer.Entities;

namespace TVApi
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            app.CreatePerOwinContext(() => new Context());
            app.CreatePerOwinContext<ApiUserManager>(ApiUserManager.Create);

            app.Use(async (Context, next) =>
            {
                await next.Invoke();
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Login"),
                AuthenticationMode = AuthenticationMode.Active,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApiUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(5),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.Use(async (Context, next) =>
            {
                await next.Invoke();
            });

            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "673983477795-4ms4miof8rsr9gcgl4hl0p816jop567k.apps.googleusercontent.com",
                ClientSecret = "AclgIY-tAIIGKrsIuigs9CWC",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "876185702508270",
                AppSecret = "ffc9e813798827eef83a8fb2908fba52",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            RegisterCors(app);
        }

        private void RegisterCors(IAppBuilder app)
        {
            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            // Try and load allowed origins from web.config
            // If none are specified we'll allow all origins

            //var origins = ConfigurationManager.AppSettings[Constants.CorsOriginsSettingKey];

            //if (origins != null)
            //{
            //    foreach (var origin in origins.Split(';'))
            //    {
            //        corsPolicy.Origins.Add(origin);
            //    }
            //}
            //else
            //{
                corsPolicy.AllowAnyOrigin = true;
            //}

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };

            app.UseCors(corsOptions);
        }
    }
}
