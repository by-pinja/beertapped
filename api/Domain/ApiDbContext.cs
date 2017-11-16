using Microsoft.EntityFrameworkCore;

namespace api.Domain
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<BeerStyleRatingModel> Products { get; protected set; }
    }
}
