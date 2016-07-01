using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace BusinessLogic.Services
{
    public interface IAccountService
    {
        AppUser GetAppUser(string userName);
        bool UpdateAppUser(string firstName, string lastName, string userName);
    }
}
