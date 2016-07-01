using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace BusinessLogic.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetPopularMovies();
        Task<IEnumerable<DataLayer.Entities.Movie>> GetUserFavoritMovies(string userName);
        Task<IEnumerable<CommentModel>> GetMovieComments(string title);
        IEnumerable<DataLayer.Entities.FavouriteMovie> GetFavouriteMovie();
        IEnumerable<DataLayer.Entities.Movie> GetMovies();
        Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(DateTime date);
        bool AddMovieToFavourit(string title, string year, string userName);
        bool DeleteMovie(string title, string userName);
    }
}
