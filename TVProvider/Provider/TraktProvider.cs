using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;
using Common.Cache;

namespace TVProvider.Provider
{
    public class TraktProvider : AbstractProvider, ITVData
    {
        private int limitPerPage = 20;
        protected override Uri BaseAddress
        {
            get
            {
                return new Uri("https://api-v2launch.trakt.tv/");
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
                    {"trakt-api-version", "2"},
                    {"trakt-api-key", "2e64662d1006d78d0c4030491b2ad5ae47cd194dfc2b2702a508b3451c91e8ec"}
                };
            }
            set
            { }
        }
        protected override string IdName
        {
            get { return "Trakt"; }
            set { }
        }

        protected override void InitializePopularMoviesUrl()
        {
            _url = "movies/popular?limit=" + limitPerPage;
        }

        protected override void InitializeMoviesCommentsUrl(string id)
        {
            _url = "movies/" + id + "/comments";
        }

        public Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(List<DataLayer.Entities.Movie> movies, DateTime date)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<CommentModel> MapComments(string responseData)
        {
            return JsonConvert.DeserializeObject<IEnumerable<CommentModel>>(responseData);
        }

        protected override IEnumerable<Movie> MapMovies(string responseData)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Movie>>(responseData);
        }

        protected override void UpdateCache(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Object movieIds = null;
                if (MovieIdsCache.MoviesIds.TryGetValue(movie.Title, out movieIds))
                {
                    var oldMovieIds = movieIds as MovieIds;
                    if (oldMovieIds.Trakt == null)
                    {
                        var newMovieIds = new MovieIds
                        {
                            Tmdb = oldMovieIds.Tmdb,

                            Trakt = movie.Ids.Trakt,
                            Imdb = movie.Ids.Imdb,
                            Slug = movie.Ids.Slug
                        };

                        MovieIdsCache.MoviesIds.TryUpdate(movie.Title, newMovieIds, oldMovieIds);
                    }
                }
                else
                {
                    MovieIdsCache.MoviesIds.TryAdd(movie.Title, movie.Ids);

                }
            }
        }
    }
}
