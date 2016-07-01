using BusinessLogic.Manager;
using Common.Settings;
using DataLayer.Context;
using DataLayer.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Factory;
using TVProvider.Models;
using TVProvider.Provider;

namespace BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        private ICompositeProvider _provider;
        private MovieManager _movieManager;
        private AccountManager _accountManager;

        public MovieService(ICompositeProvider provider)
        {
            _movieManager = new MovieManager();
            _accountManager = new AccountManager();
            _provider = provider;
        }
        
        public Task<IEnumerable<Movie>> GetPopularMovies()
        {
            return _provider.GetPopularMovies();
        }

        public Task<IEnumerable<DataLayer.Entities.Movie>> GetUserFavoritMovies(string userName)
        {
            return _movieManager.GetAsyncFavouriteMovie(userName);
        }

        public Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(DateTime date)
        {
            var movieList = _movieManager.GetMovies();
            return _provider.GetListMovieCommentsByDate(movieList, date);
        }

        public Task<IEnumerable<CommentModel>> GetMovieComments(string title)
        {
            return _provider.GetMovieComments(title);
        }

        public IEnumerable<DataLayer.Entities.FavouriteMovie> GetFavouriteMovie()
        {
            return _movieManager.GetFavouriteMovie();
        }

        public IEnumerable<DataLayer.Entities.Movie> GetMovies()
        {
            return _movieManager.GetMovies();
        }

        public bool AddMovieToFavourit(string title, string year, string userName)
        {
            try
            {
                var user = _accountManager.GetAppUserByUserName(userName);
                int yearInt = Convert.ToInt32(year);

                var movieDb = _movieManager.InsertMovie(title, yearInt);

                if (movieDb != null)
                {
                    var result = _movieManager.InsertFavouritMovie(title, yearInt, user.Id, movieDb.Id);
                }

                return true;
            }
            catch(Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                return false;
            }
            
        }

        public bool DeleteMovie(string title, string userName)
        {
            var user = _accountManager.GetAppUserByUserName(userName);
            var movie = _movieManager.GetMovieByTitle(title);

            var result = _movieManager.DeleteMovie(movie.Id, user.Id);

            return result;
        }
    }
}
