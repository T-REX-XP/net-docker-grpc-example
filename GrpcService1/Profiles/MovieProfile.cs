using AutoMapper;
using GrpcService1.Models;

namespace GrpcService1.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieGRPC>();
            CreateMap<MovieGRPC, Movie>();
        }
    }
}
