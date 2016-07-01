using BusinessLogic.Manager;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;
using TVProvider.Provider;

namespace BusinessLogic.Infrastructure
{
    public class EventNotifierLogic : IEventNotifierLogic
    {
        private MovieManager _movieManager;
        private AccountManager _accountManager;
        private NotificationsManager _notificationsManager;
        private ICompositeProvider _provider;

        public EventNotifierLogic()
        {
            _movieManager = new MovieManager();
            _accountManager = new AccountManager();
            _notificationsManager = new NotificationsManager();
            _provider = new CompositeProvider();
        }

        public async Task<IEnumerable<Notifications>> GetCommentNotification(DateTime date)
        {
            var notifList = new List<Notifications>();
            var movielist = _movieManager.GetMovies();
            var commentsList = await _provider.GetListMovieCommentsByDate(movielist, date.AddDays(-10)).ConfigureAwait(false);
            var favouritrUsers = _movieManager.GetFavouriteMovieByMovieIds(commentsList.Select(s => s.MovieId).ToList());

            foreach(var item in commentsList)
            {
                var newNotifList = InsertNotifications(item, favouritrUsers.Where(w => w.MovieId == item.MovieId).ToList());
                notifList.AddRange(newNotifList);       
            }

            if(notifList != null)
            {
                _notificationsManager.InsertNotifications(notifList);
            }

            return notifList;
        }

        public static List<Notifications> InsertNotifications(CommentNotifModel comment, List<FavouriteMovie> userList)
        {
            List<Notifications> newNotifList = new List<Notifications>();

            foreach(var user in userList)
            {
                Notifications record = new Notifications();
                record.UserId = user.AppUserId;
                record.Description = comment.MovieTitle + " new comment";
                record.DateCreated = comment.Created_At;
                record.Read = false;
                newNotifList.Add(record);
            }

            return newNotifList;
        }
    }
}
