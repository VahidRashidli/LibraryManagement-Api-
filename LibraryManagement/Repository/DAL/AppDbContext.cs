using DomainModels.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options){}
        public DbSet<Book> Books { get; set; }
    }
}
