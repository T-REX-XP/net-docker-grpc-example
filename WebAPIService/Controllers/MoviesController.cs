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

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get All Movies
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get top 1 Movie")]
        [HttpGet]
        public async Task<Movie> GetAsync()
        {
            return new Models.Movie()
            {
                Director = "",
                Id = new Guid(),
                Rated = "",
                Released = DateTime.Now,
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
        public async Task<Movie> GetAsync(Guid id)
        {
            return new Movie()
            {
                Director = "",
                Id = new Guid(),
                Rated = "",
                Released = DateTime.Now,
                Runtime = "",
                Title = "",
                Year = ""
            };
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create Movie")]
        [HttpPost]
        public async Task<Guid> CreateAsync(Models.Movie movie)
        {
            //TODO: implement create 
            return Guid.NewGuid();
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
