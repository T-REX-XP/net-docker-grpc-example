using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIService.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieGRPC, Models.Movie>();
            CreateMap<Models.Movie, MovieGRPC>();

            CreateMap<IEnumerable<Models.Movie>, IEnumerable<MovieGRPC>>();
            CreateMap<IEnumerable<MovieGRPC>, IEnumerable<Models.Movie>>();

            CreateMap<CreateMovieRequest, Models.WebRequests.CreateMovieRequest>().ForMember(x => x, x => x.MapFrom(d => d.Movie));
            CreateMap<Models.WebRequests.CreateMovieRequest, CreateMovieRequest>().ForMember(x => x.Movie, x => x.MapFrom(d => d));

            CreateMap<MovieGRPC, Models.WebRequests.CreateMovieRequest>();
            CreateMap<Models.WebRequests.CreateMovieRequest, MovieGRPC>();

        }
    }
}
