using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TVApi.Providers
{
    public interface IPatientPrincipal : IPrincipal
    {

        WindowsBuiltInRole Role { get; set; }
        string UserName { get; set; }
    }
}