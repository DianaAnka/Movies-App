using System;
using System.Collections.Generic;
using System.Text;

namespace MovieInfo.Domain.Models
{
    public class Movie : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime RelaseDate { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();


        private Movie(string name, string description, string image, DateTime relaseDate)
        {
            Name = name;
            Description = description;
            Image = image;
            RelaseDate = relaseDate;
            CreationTime = DateTime.Now;
        }

        public static Movie Create(string name, string description, string image, DateTime relaseDate)
        {

            if (String.IsNullOrWhiteSpace(name)) throw new NullReferenceException(" Name is null");
            return new Movie(name, description, image, relaseDate);
        }

        public void Update(string name, string description, string image, DateTime relaseDate)
        {
            Name = name;
            Description = description;
            Image = image;
            RelaseDate = relaseDate;
        }
    }
}
