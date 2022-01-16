using MovieInfo.Domain.Models;

namespace MovieInfo.Domain.Extenstions
{
    public static class BaseEntityExtentions
    {
        public static void SoftDelete(this ISoftDelete entity)
        {
            entity.IsDeleted = true;
        }
    }
}
