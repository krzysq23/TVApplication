using DataLayer.Entities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Manager
{
    public sealed class MovieManager
    {
        private Context _ctx;

        public MovieManager()
        {
            _ctx = new Context();
        }

        /// <summary>
        /// Get Movie list
        /// </summary>
        /// <returns></returns>
        public List<Movie> GetMovies()
        {
            try
            {
                return _ctx.Movies.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get movie by title
        /// </summary>
        /// <returns></returns>
        public Movie GetMovieByTitle(string title)
        {
            try
            {
                return _ctx.Movies.FirstOrDefault(x => x.Title == title);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get User Movie list ASYNC
        /// </summary>
        /// <returns>List<Movie></returns>
        public async Task<IEnumerable<Movie>> GetAsyncFavouriteMovie(string userName)
        {
            try
            {
                var user = _ctx.AppUser.FirstOrDefault(x => x.UserName == userName);
                var movieIds = _ctx.FavouriteMovies.Where(x => x.AppUserId == user.Id).Select(s => s.MovieId).ToList();

                var movies = await Task.Factory.StartNew(() => _ctx.Movies.Where(x => movieIds.Contains(x.Id)).ToList());

                return movies;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get User Movie list 
        /// </summary>
        /// <returns>List<Movie></returns>
        public List<Movie> GetFavouriteMovie(string userName)
        {
            try
            {
                var user = _ctx.AppUser.FirstOrDefault(x => x.UserName == userName);
                var movieIds = _ctx.FavouriteMovies.Where(x => x.AppUserId == user.Id).Select(s => s.MovieId).ToList();

                return _ctx.Movies.Where(x => movieIds.Contains(x.Id)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Favourite Movie By MovieIds
        /// </summary>
        /// <returns>List<Movie></returns>
        public List<FavouriteMovie> GetFavouriteMovieByMovieIds(List<int> movieIds)
        {
            try
            {
                return _ctx.FavouriteMovies.Where(x => movieIds.Contains(x.MovieId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Movie list
        /// </summary>
        /// <returns></returns>
        public List<FavouriteMovie> GetFavouriteMovie()
        {
            try
            {
                return _ctx.FavouriteMovies.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Movie insert
        /// </summary>
        /// <returns></returns>
        public Movie InsertMovie(string title, int year)
        {
            try
            {
                var movieDb = _ctx.Movies
                        .Where(c => c.Title == title)
                        .FirstOrDefault();

                if (movieDb == null)
                {
                    Movie record = new Movie();
                    record.Title = title;
                    record.Year = year;

                    _ctx.Movies.Add(record);
                    _ctx.SaveChanges();

                    return record;
                }
                else
                {
                    return movieDb;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Favourit Movie insert
        /// </summary>
        /// <returns></returns>
        public bool InsertFavouritMovie(string title, int year, int appUserId, int movieId)
        {
            try
            {
                var favouritMovie = _ctx.FavouriteMovies
                    .Where(c => c.MovieId == movieId && c.AppUserId == appUserId)
                    .FirstOrDefault();

                if (favouritMovie == null)
                {
                    FavouriteMovie record = new FavouriteMovie();
                    record.AppUserId = appUserId;
                    record.MovieId = movieId;

                    _ctx.FavouriteMovies.Add(record);
                    _ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Movie delete
        /// </summary>
        /// <returns></returns>
        public bool DeleteMovie(int movieId, int appUserId)
        {
            try
            {
                var favouriteMovie = _ctx.FavouriteMovies.Where(x => x.AppUserId == appUserId && x.MovieId == movieId).FirstOrDefault();
                _ctx.FavouriteMovies.Remove(favouriteMovie);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
