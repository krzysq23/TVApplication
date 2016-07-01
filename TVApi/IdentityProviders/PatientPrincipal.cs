using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace TVApi.Providers
{
    public class PatientPrincipal : ClaimsPrincipal, IPatientPrincipal
    {
        public PatientPrincipal(ApplicationUser user)
        {
            Identity = new GenericIdentity(user.UserName);
        }

        public PatientPrincipal() { }

        public new bool IsInRole(string role)
        {
            if (!string.IsNullOrWhiteSpace(role))
                return Role.ToString().Equals(role);

            return false;
        }

        public new IIdentity Identity { get; private set; }
        public WindowsBuiltInRole Role { get; set; }
        public string UserName { get; set; }
    }
}