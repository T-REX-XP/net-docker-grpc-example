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
            CreateMap<MoviesResponse, IEnumerable<MovieGRPC>>();
            CreateMap<IEnumerable<MovieGRPC>,MoviesResponse>();
        }
    }
}
