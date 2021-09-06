using AutoMapper;
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
        public override Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request, ServerCallContext context)
        {
            var newMovie = _mapper.Map<Movie>(request.Movie);
            Movie createdMovie= _movieRepository.Create(newMovie);
            return Task.FromResult(new CreateMovieResponse() { Id = createdMovie?.Id });
        }
        public override Task<MovieResponse> GetMovie(MovieRequest request, ServerCallContext context)
        {
            var movieDto = _movieRepository.Get(request.Id);
            var movie = _mapper.Map<MovieGRPC>(movieDto);
            return Task.FromResult(new MovieResponse()
            {
                Movie = movie
            });
        }
    }
}
