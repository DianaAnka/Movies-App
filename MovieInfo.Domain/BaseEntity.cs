using MovieInfo.Domain.Models;
using System;

namespace MovieInfo.Domain
{
    public class BaseEntity : ISoftDelete
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void SetCreationTime()
        {
            CreationTime = DateTime.Now;
        }
    }
}
