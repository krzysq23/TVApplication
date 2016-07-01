using BusinessLogic.Services;
using DataLayer.Context;
using DataLayer.Entities;
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
using System.Web.Http;
using System.Web.Http.Description;
using TVApi.Models;
using TVProvider.Models;
using TVProvider.Provider;

namespace TVApi.Controllers
{
    [RoutePrefix("api/Movies")]
    public class MovieController : ApiController
    {
        private IMovieService _movieService;

        public MovieController(ICompositeProvider provider)
        {
            this._movieService = new MovieService(provider);
        }

        [HttpGet]
        [Route("Popular")]
        public async Task<IEnumerable<TVProvider.Models.Movie>> GetPopularMovies()
        {
            var data = _movieService.GetPopularMovies();
            return await data;
        }

        [HttpGet]
        [Route("Favorit")]
        public async Task<IEnumerable<DataLayer.Entities.Movie>> GetUserFavoritMovies(string userName)
        {
            var data = _movieService.GetUserFavoritMovies(userName);
            return await data;
        }

        [HttpGet]
        [Route("Comments")]
        public async Task<IEnumerable<CommentModel>> GetComments(string title)
        {
            var data = _movieService.GetMovieComments(title);
            return await data;
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddToFavourit(MovieModel movieModel)
        {
            try
            {
                _movieService.AddMovieToFavourit(movieModel.Title, movieModel.Year, movieModel.UserName);

                return Ok();
            }
            catch (Exception ex)
            {
                Common.Log.Log4Net.logger.Error(ex, "", "");
                return Conflict();
            }
        }

        [HttpPost]
        [Route("deleteMovie")]
        public IHttpActionResult DeleteMovie(FavouriteUserMovieModel fumModel)
        {
            try
            {
                _movieService.DeleteMovie(fumModel.title, fumModel.userName);

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
