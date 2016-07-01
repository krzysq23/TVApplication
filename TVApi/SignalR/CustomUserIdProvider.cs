using BusinessLogic.Manager;
using BusinessLogic.Services;
using DataLayer.Context;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace TVApi.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return ClaimsPrincipal.Current.Identity.Name;
        }
    }
}