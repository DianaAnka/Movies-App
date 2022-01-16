using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieInfo.Api.Controllers.Dtos
{
    public class CreateMovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public DateTime RelaseDate { get; set; }
    }
}
