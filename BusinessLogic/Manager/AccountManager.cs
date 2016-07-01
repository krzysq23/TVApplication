using DataLayer.Context;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public sealed class AccountManager
    {
        private Context _ctx;

        public AccountManager()
        {
            _ctx = new Context();
        }

        public AppUser GetAppUserByUserName(string userName)
        {
            try
            {
                return _ctx.AppUser.FirstOrDefault(x => x.UserName == userName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AppUser GetAppUserByUserId(string id)
        {
            try
            {
                return _ctx.AppUser.FirstOrDefault(x => x.UserId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AppUserInsert(string id, string userName)
        {
            try
            {
                AppUser record = new AppUser();
                record.UserId = id;
                record.UserName = userName;
                _ctx.AppUser.Add(record);
                _ctx.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public void AppUserUpdate(AppUser record)
        {
            try
            {
                _ctx.AppUser.Attach(record);
                _ctx.Entry(record).State = EntityState.Modified;
                _ctx.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}
