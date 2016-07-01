using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Manager
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(IRoleStore<IdentityRole, string> roleStore)
        : base(roleStore)
    {
        }

        public static RoleManager Create(
        IdentityFactoryOptions<RoleManager> options,
        IOwinContext context)
        {
            var manager = new RoleManager(
                new RoleStore<IdentityRole>(
                    context.Get<Context.Context>()));

            return manager;
        }
    }
}
