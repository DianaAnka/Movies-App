namespace MovieInfo.Domain.Models
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
