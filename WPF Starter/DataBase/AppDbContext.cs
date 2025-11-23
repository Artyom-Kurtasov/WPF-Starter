using Microsoft.EntityFrameworkCore;
using WPF_Starter.Models;

namespace WPF_Starter.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<People> Person { get; set; }
    }
}
