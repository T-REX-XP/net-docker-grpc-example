using AutoMapper;
using System.Collections.Generic;

namespace GrpcService1.Profiles
{
    public class MoviesResponseProfile : Profile
    {
        public MoviesResponseProfile()
        {
            CreateMap<IEnumerable<Models.Movie>, MoviesResponse>().ForMember(d => d.Movies, d => d.MapFrom(x => x));
        }
    }
}
