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
            CreateMap<IEnumerable<Models.Movie>, MoviesResponse>().ForMember(d => d.Movies, d => d.MapFrom(x => x));
        }
    }
}
