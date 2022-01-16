using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieInfo.Domain.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }


        private Comment(string text, int movieId, string userId)
        {
            Text = text;
            MovieId = movieId;
            UserId = userId;
        }

        public static Comment Create(string text, int movieId, string userId)
        {
            return new Comment(text, movieId, userId);
        }


    }
}
