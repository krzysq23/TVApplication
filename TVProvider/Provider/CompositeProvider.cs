using DataLayer.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TVProvider.Factory;
using TVProvider.Helpers;
using TVProvider.Models;

namespace TVProvider.Provider
{
    public interface ICompositeProvider : ITVData
    {
    }

    public class CompositeProvider : ICompositeProvider
    {
        private IList<ITVData> _providers;
        private TmdbProvider _tmdbProvider;
        private TraktProvider _traktProvider;

        public CompositeProvider()
        {
            try
            {
                _providers = new List<ITVData>();
                _tmdbProvider = new TmdbProvider();
                _traktProvider = new TraktProvider();
                _providers.Add(_tmdbProvider);
                _providers.Add(_traktProvider);
            }
            catch(Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex,"","");
            }
        }

        public CompositeProvider(IList<ITVData> providers)
        {
            _providers = providers;
        }

        public async Task<IEnumerable<Movie>> GetPopularMovies()
        {
            var movies = new List<Movie>();

            foreach (var provider in _providers)
            {
                var popularMovies = await provider.GetPopularMovies();
                movies.AddRange(popularMovies);
            }
            return movies;
        }

        public async Task<IEnumerable<CommentModel>> GetMovieComments(string title)
        {
            var comments = new List<CommentModel>();
            foreach (var provider in _providers)
            {
                var commentsFromProvider = await provider.GetMovieComments(title).ConfigureAwait(false);
                comments.AddRange(commentsFromProvider);
            }
            return comments;
        }

        public async Task<IEnumerable<CommentNotifModel>> GetListMovieCommentsByDate(List<DataLayer.Entities.Movie> movies, DateTime date)
        {

            var moviesComments = new List<CommentNotifModel>();

            foreach (var provider in _providers)
            {
                await provider.GetPopularMovies();
            }

            try
            {
                foreach (var movie in movies)
                {
                    var comment = await GetMovieComments(movie.Title);

                    var commentsFromProviderUp = comment.Where(w => w.Created_At >= date);

                    if (commentsFromProviderUp.Any())
                    {
                        var records = BuildCommentModels(movie, commentsFromProviderUp);
                        moviesComments.AddRange(records);
                    }                  
                }
                return moviesComments;
            }
            catch(Exception)
            {
                // TO DO
                throw;
            }           
        }

        public List<CommentNotifModel> BuildCommentModels(DataLayer.Entities.Movie movie, IEnumerable<CommentModel> commentModel)
        {
            List<CommentNotifModel> newCommentModelList = new List<CommentNotifModel>();

            foreach (var comm in commentModel)
            {
                CommentNotifModel newCommentModel = new CommentNotifModel();
                newCommentModel.MovieId = movie.Id;
                newCommentModel.MovieTitle = movie.Title;
                newCommentModel.Comment = comm.Comment;
                newCommentModel.Created_At = comm.Created_At;
                newCommentModelList.Add(newCommentModel);
            }

            return newCommentModelList;
        }
    }
}
