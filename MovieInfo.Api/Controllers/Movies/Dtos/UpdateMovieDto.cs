using MovieInfo.Api.Controllers.Dtos;

namespace MovieInfo.Api.Controllers.Movies.Dtos
{
    public class UpdateMovieDto : CreateMovieDto
    {
        public int Id { get; set; }
    }
}
