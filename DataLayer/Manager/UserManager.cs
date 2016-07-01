using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Manager
{
    public class ApiUserManager : UserManager<ApplicationUser>
    {
        public ApiUserManager(IUserStore<ApplicationUser> store)
        : base(store)
        {
            this.Store = store;
        }

        public static ApiUserManager Create(
        IdentityFactoryOptions<ApiUserManager> options,
        IOwinContext context)
        {
            var appDbContext = context.Get<Context.Context>();
            var appUserManager = new ApiUserManager(new UserStore<ApplicationUser>(appDbContext));

            return appUserManager;
        }

        public virtual async Task<IdentityResult> AddUserToRolesAsync(
            string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<ApplicationUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore
                .GetRolesAsync(user)
                .ConfigureAwait(false);

            // Add user to each role using UserRoleStore
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                await userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
            }
            // Call update once when all roles are added
            return await UpdateAsync(user).ConfigureAwait(false);
        }


        public virtual async Task<IdentityResult> RemoveUserFromRolesAsync(
            string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<ApplicationUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore
                .GetRolesAsync(user)
                .ConfigureAwait(false);

            // Remove user to each role using UserRoleStore
            foreach (var role in roles.Where(userRoles.Contains))
            {
                await userRoleStore
                    .RemoveFromRoleAsync(user, role)
                    .ConfigureAwait(false);
            }
            // Call update once when all roles are removed
            return await UpdateAsync(user).ConfigureAwait(false);
        }
    }
}
