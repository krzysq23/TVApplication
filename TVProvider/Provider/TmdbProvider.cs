using Common.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace TVProvider.Provider
{
    public class TmdbProvider : AbstractProvider, ITVData
    {
        private string apiKey = "c58faded168e42fc3333816fb0a01eb9";
        protected override Uri BaseAddress
        {
            get
            {
                return new Uri("http://api.themoviedb.org/3/");
            }
            set
            { }
        }
        protected override Dictionary<string, string> Headers
        {
            get
            {
                return new Dictionary<string, string>
                {
                };
            }
            set
            { }
        }
        protected override string IdName
        {
            get { return "Tmdb"; }
            set { }
        }

        protected override void InitializePopularMoviesUrl()
        {
            _url = "movie/popular?api_key=" + apiKey;
        }

        protected override void InitializeMoviesCommentsUrl(string id)
        {
            _url = "movie/" + id + "/reviews?api_key=" + apiKey;
        }

        public Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(List<DataLayer.Entities.Movie> movies, DateTime date)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Movie> MapMovies(string responseData)
        {
            return JsonConvert.DeserializeObject<MovieListResult>(responseData).Results;
        }

        protected override IEnumerable<CommentModel> MapComments(string responseData)
        {
            return JsonConvert.DeserializeObject<CommentListResult>(responseData).Results;
        }

        protected override void UpdateCache(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Object movieIds = null;
                if (MovieIdsCache.MoviesIds.TryGetValue(movie.Title, out movieIds))
                {
                }
                else
                {
                    MovieIdsCache.MoviesIds.TryAdd(movie.Title, movie.Ids);
                }
            }
        }
    }
}
