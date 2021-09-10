using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIService.Models;
using WebAPIService.Models.WebRequests;

namespace WebAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly GrpcChannel channel;
        private readonly MovieServiceGRPC.MovieServiceGRPCClient client;
        private readonly IMapper _mapper;
        public MoviesController(ILogger<MoviesController> logger, IGRPCSettings settings, IMapper mapper)
        {
            _logger = logger;
            channel = GrpcChannel.ForAddress(settings.Address);
            client = new MovieServiceGRPC.MovieServiceGRPCClient(channel);
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Movies
        /// </summary>
        /// <param name="skip">Skip elements</param>
        /// <param name="take">Take elements</param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get Movies")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMoviesAsync([FromQuery] int skip = 0, [FromQuery] int take = 2)
        {
            var movies = await client.GetMoviesAsync(new GetMoviesRequest { Skip = skip, Take = take });
            var result = _mapper.Map<IEnumerable<Movie>>(movies);
            return result;
        }

        /// <summary>
        /// Get Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Get Movie by Id")]
        [HttpGet("{id}")]
        public async Task<Movie> GetAsync(string id)
        {
            var reply = client.GetMovie(new MovieRequest { Id = id });
            Console.WriteLine("---WebApi: id===" + id);
            return await Task.FromResult(_mapper.Map<Movie>(reply.Movie));
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Create Movie")]
        [HttpPost]
        public async Task<string> CreateAsync(Models.WebRequests.CreateMovieRequest request)
        {
            var data = await client.CreateMovieAsync(_mapper.Map<CreateMovieRequest>(request));
            return data.Id;
        }

        /// <summary>
        /// Update Moovie by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Update Movie by Id")]
        [HttpPut("{id}")]
        public async Task UpdateAsync(string id, Models.WebRequests.CreateMovieRequest request)
        {
            var record = _mapper.Map<MovieGRPC>(request);
            record.Id = id;
            await client.UpdateMovieAsync(new CreateMovieRequest { Movie = record });

        }

        /// <summary>
        /// Delete Movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await client.RemoveMovieAsync(new MovieRequest { Id = id });
        }
    }
}
