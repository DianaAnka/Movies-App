using AutoMapper;
using MovieInfo.Api.Controllers.Dtos;
using MovieInfo.Api.Controllers.Movies.Dtos;
using MovieInfo.Domain.Models;

namespace MovieInfo.Api.Controllers.Movies
{
    public class MoviesAutoMapperProfile : Profile
    {
        public MoviesAutoMapperProfile()
        {
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
