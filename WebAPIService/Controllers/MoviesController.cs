using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIService.Models;

namespace WebAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly GrpcChannel channel;
        private readonly MovieServiceGRPC.MovieServiceGRPCClient client;
        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
            channel = GrpcChannel.ForAddress("https://localhost:49201/");
            client = new MovieServiceGRPC.MovieServiceGRPCClient(channel);
        }

        /// <summary>
        /// Get All Movies
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get top 1 Movie")]
        [HttpGet]
        public Movie Get()
        {
            return new Models.Movie()
            {
                Director = "FFF",
                Id = "",
                Rated = "",
                Released = "",
                Runtime = "",
                Title = "",
                Year = ""
            };
        }

        /// <summary>
        /// Get Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get Movie by Id")]
        [HttpGet("id")]
        public async Task<Movie> GetAsync(string id)
        {
            var reply = client.GetMovie(new MovieRequest { Id = id });
            //TODO: Use Automapper
            return await Task.FromResult(new Movie() { 
                Id = reply.Movie.Id, 
                Title = reply.Movie.Title,
                Director= reply.Movie.Director,
                Rated = reply.Movie.Reted,
                Released = reply.Movie.Released,
                Runtime = reply.Movie.Runtime,
                Year = reply.Movie.Year
            });
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create Movie")]
        [HttpPost]
        public string Create(Models.WebRequests.CreateMovieRequest request)
        {
            return client.CreateMovie(
                    //TODO: Use Automapper
                    new CreateMovieRequest
                    {
                        Movie = new MovieGRPC
                        {
                            Title = request.Title,
                            Director = request.Director,
                            Released = request.Released,
                            Reted = request.Rated,
                            Runtime = request.Runtime,
                            Year = request.Year
                        }
                    }).Id;
        }

        /// <summary>
        /// Update Moovie by Id
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Update Movie by Id")]
        [HttpPost("id")]
        public async Task<Guid> UpdateAsync(Models.Movie movie)
        {
            //TODO: implement create 
            return Guid.NewGuid();
        }
    }
}
