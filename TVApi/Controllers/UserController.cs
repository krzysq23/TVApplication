using BusinessLogic.Manager;
using BusinessLogic.Services;
using DataLayer.Context;
using DataLayer.Entities;
using DataLayer.Manager;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TVApi.Models;
using TVProvider.Models;

namespace TVApi.Controllers
{
    [RoutePrefix("api/Users")]
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController()
        {
            this._userService = new UserService();
        }

        [HttpGet]
        [Route("List")]
        public IEnumerable<AppUser>  GetUserList()
        {
            var data = _userService.GetUserList();
            return data;
        }

        [HttpGet]
        [Route("List")]
        public IEnumerable<AppUser> GetUserList(string userName)
        {
            var id = HttpContext.Current.User.Identity.Name;
            var data = _userService.GetUserList(userName);
            return data;
        }

        [HttpGet]
        [Route("UserFriends")]
        public IEnumerable<AppUser> GetUserFriends(string userName)
        {
            var data = _userService.GetUserFriends(userName);
            return data;
        }

        [HttpPost]
        [Route("AddToFirends")]
        public IHttpActionResult AddToFirends(UserFriendModel userFriendModel)
        {
            try
            {
                var data = _userService.AddUserToFirends(userFriendModel.UserName, userFriendModel.UserFriendId);

                return Ok();
            }
            catch (Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                return Conflict();
            }
        }

        [HttpPost]
        [Route("DeleteFriend")]
        public IHttpActionResult DeleteFriend(UserFriendModel userFriendModel)
        {
            try
            {
                var data = _userService.DeleteFriend(userFriendModel.UserName, userFriendModel.UserFriendId);

                return Ok();
            }
            catch (Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                return Conflict();
            }
        }
    }
}
