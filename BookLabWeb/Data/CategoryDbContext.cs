using BookLabWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLabWeb.Data
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
