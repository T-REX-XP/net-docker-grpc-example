using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1.Models;
using GrpcService1.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public override async Task<MoviesResponse> GetMovies(GetMoviesRequest request, ServerCallContext context)
        {
            var moviesDto = await _movieRepository.GetAsync(request);            
            var result = new MoviesResponse();
            Console.WriteLine("::GRPC--> Before map");
            var movies = _mapper.Map<IEnumerable<MovieGRPC>>(moviesDto);
            result.Movies.AddRange(movies);
            Console.WriteLine("::GRPC--> After add range");
            return result;
        }

        public override async Task<CreateMovieResponse> UpdateMovie(CreateMovieRequest request, ServerCallContext context)
        {
            var moviesDto = _mapper.Map<Movie>(request.Movie);
            await _movieRepository.UpdateAsync(request.Movie.Id, moviesDto);
            return new CreateMovieResponse();
        }

        public override async Task<CreateMovieResponse> RemoveMovie(MovieRequest request, ServerCallContext context)
        {
            await _movieRepository.RemoveAsync(request.Id);
            return new CreateMovieResponse() { Id = "" };
        }

    }
}
