using Common.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Models;

namespace TVProvider.Provider
{
    public abstract class AbstractProvider : IDisposable
    {
        private HttpClient _httpClient;
        protected string _url { get; set; }
        private void InitializeHttpClient()
        {
            _httpClient = new HttpClient { BaseAddress = BaseAddress };
            foreach (var header in Headers)
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
        private string GetMovieIdBasedOnTitle(string title, string nameId)
        {
            var movieIds = new Object();
            MovieIdsCache.MoviesIds.TryGetValue(title, out movieIds);

            var castMovieIds = (MovieIds)movieIds;

            var id = castMovieIds.GetType().GetProperty(nameId).GetValue(castMovieIds, null);
            return id != null ? id.ToString() : null;
        }
        protected abstract string IdName { get; set; }
        protected abstract Uri BaseAddress { get; set; }
        protected abstract Dictionary<string, string> Headers { get; set; }
        protected abstract IEnumerable<Movie> MapMovies(string responseData);
        protected abstract IEnumerable<CommentModel> MapComments(string responseData);
        protected abstract void UpdateCache(IEnumerable<Movie> movies);
        protected abstract void InitializePopularMoviesUrl();
        protected abstract void InitializeMoviesCommentsUrl(string id);


        private async Task<IEnumerable<T>> GetData<T>(Func<string, IEnumerable<T>> mapMethod)
        {
            InitializeHttpClient();

            var response = await _httpClient.GetAsync(_url);
            string responseData = await response.Content.ReadAsStringAsync();
            return mapMethod(responseData);
        }

        public async Task<IEnumerable<CommentModel>> GetMovieComments(string title)
        {
            var id = GetMovieIdBasedOnTitle(title, IdName);
            if (id == null)
            {
                return new List<CommentModel>();
            }

            //Action<string> tst = _ => InitializeMoviesCommentsUrl(id);

            InitializeMoviesCommentsUrl(id);

            return await GetData<CommentModel>(MapComments);
        }

        public async Task<IEnumerable<Movie>> GetPopularMovies()
        {
            InitializePopularMoviesUrl();

            var data = await GetData<Movie>(MapMovies);

            UpdateCache(data);

            return data;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
