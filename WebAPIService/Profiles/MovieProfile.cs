using AutoMapper;
using System.Collections.Generic;
using WebAPIService.Models;

namespace WebAPIService.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieGRPC, Models.Movie>();
            CreateMap<Models.Movie, MovieGRPC>();

            CreateMap<IEnumerable<Models.Movie>, List<MovieGRPC>>();

            CreateMap<IEnumerable<Movie>, MoviesResponse>().ForMember(x => x.Movies, x => x.MapFrom(d => d));

            CreateMap<Models.WebRequests.CreateMovieRequest, CreateMovieRequest>().ForMember(x => x.Movie, x => x.MapFrom(d => d));

            CreateMap<MovieGRPC, Models.WebRequests.CreateMovieRequest>();
            CreateMap<Models.WebRequests.CreateMovieRequest, MovieGRPC>();

        }
    }
}
