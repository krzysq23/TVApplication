using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace TVProvider.Provider
{ 
    public interface ITVData
    {
        Task<IEnumerable<Movie>> GetPopularMovies(); 
        Task<IEnumerable<CommentModel>> GetMovieComments(string title);
        Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(List<DataLayer.Entities.Movie> movies, DateTime date);
    }
}
