using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1.Profiles
{
    public class MoviesResponseProfile : Profile
    {
        public MoviesResponseProfile()
        {
            CreateMap<MoviesResponse, IEnumerable<MovieGRPC>>().ForMember(d => d, m => m.MapFrom(x => x.Movies));
            CreateMap<IEnumerable<MovieGRPC>, MoviesResponse>().ForMember(d => d.Movies, m => m.MapFrom(x => x));
        }
    }
}
