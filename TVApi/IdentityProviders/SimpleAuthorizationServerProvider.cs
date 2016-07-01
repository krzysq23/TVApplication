using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Manager;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using TVApi.Repository;
using TVApi.SignalR;

namespace TVApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var userManager = context.OwinContext.GetUserManager<ApiUserManager>();
            var authManager = context.OwinContext.Authentication;

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var signInHelper = new SignInHelper(userManager, authManager);
            await signInHelper.SignInAsync(user, false, false);

            FormsAuthentication.SetAuthCookie(user.UserName, true);

            PatientPrincipal newUser = new PatientPrincipal(user);
            newUser.UserName = user.UserName;
            HttpContext.Current.User = newUser;        

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager);

            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));


            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }
    }
}