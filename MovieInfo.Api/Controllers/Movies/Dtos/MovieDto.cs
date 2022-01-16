using MovieInfo.Api.Controllers.Movies.Dtos;
using System;
using System.Collections.Generic;

namespace MovieInfo.Api.Controllers.Dtos
{
    public class MovieDto
    {
        public MovieDto(int id, string name, string description, string image, DateTime relaseDate)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            RelaseDate = relaseDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime RelaseDate { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
