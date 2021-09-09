using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1.Models;
using GrpcService1.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class MovieGRPCService : MovieServiceGRPC.MovieServiceGRPCBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMapper _mapper;
        private readonly MovieRepository _movieRepository;
        public MovieGRPCService(ILogger<GreeterService> logger, MovieRepository movieRepository, IMapper mapper)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public override async Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request, ServerCallContext context)
        {
            var newMovie = _mapper.Map<Movie>(request.Movie);
            var createdMovie = await _movieRepository.CreateAsync(newMovie);
            return await Task.FromResult(new CreateMovieResponse() { Id = createdMovie?.Id });
        }
        public override async Task<MovieResponse> GetMovie(MovieRequest request, ServerCallContext context)
        {
            var movieDto = await _movieRepository.GetAsync(request.Id);
            var movie = _mapper.Map<MovieGRPC>(movieDto);
            return new MovieResponse()
            {
                Movie = movie
            };
        }
        public override async Task<MoviesResponse> GetMovies(Empty request, ServerCallContext context)
        {
            var result = await _movieRepository.GetAsync();
            var converted = _mapper.Map<MoviesResponse>(result);
            return converted;
        }
    }
}
